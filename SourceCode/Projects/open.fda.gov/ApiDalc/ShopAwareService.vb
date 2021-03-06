﻿#Region " Imports "

Imports Newtonsoft.Json.Linq
Imports ApiDalc.DataObjects
Imports ApiDalc.Enumerations
Imports System.ComponentModel

#End Region

Public Class ShopAwareService
    Public Property OpenFdaApiHits As Integer

    Const MaxResultSetSize = 100

#Region " Member Variables "

    Private _fda As OpenFda
    Private _restClient As IRestClient

#End Region

#Region " Constructors "

    Public Sub New()
        _restClient = New RestClient
    End Sub

    Public Sub New(restClient As IRestClient)
        _restClient = restClient
    End Sub

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Gets a count of issues. Issues are enforcements (Recalls) of food, drug and device (identified by classification) and events of drug and devices
    ''' </summary>
    ''' <param name="keyWord"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSearchSummary(ByVal keyWord As String, ByVal state As String) As SearchSummary

        Dim results As SearchSummary = Nothing

        results = GetRecallInfoCounts(keyWord, state)

        Return results

    End Function

    ''' <summary>
    ''' Gets a detailed list of issues. Issues are enforcements (Recalls) of food, drug and device (identified by classification) and events of drug and devices
    ''' </summary>
    ''' <param name="keyWord"></param>
    ''' <param name="state"></param>
    ''' <returns>FDA Result object</returns>
    ''' <remarks></remarks>
    Public Function GetFDAResult(ByVal keyWord As String, ByVal state As String) As FDAResult

        Dim dataYearsBack As Integer = 1

        Dim searchResultLocal As New FDAResult With {.Keyword = keyWord}

        Dim mapList As New Dictionary(Of String, SearchResultMapData)
        Dim graphData As New ReportData

        Dim tmp As List(Of ResultRecall) = GetRecallInfo(keyWord, state)
        Dim values As New FDAResult

        Dim dateInit As Date = Date.Now.AddYears(-dataYearsBack)

        ' Initialize graph data
        For i As Integer = 1 To 12 * dataYearsBack

            dateInit = dateInit.AddMonths(1)

            graphData.Labels.Add(dateInit.ToString("MMM-yyyy"))
            graphData.Data1.Add(0)
            graphData.Data2.Add(0)
            graphData.Data3.Add(0)
            graphData.DataE.Add(0)

        Next

        ' Reconstruct recall data as searchresultitem object
        For Each itm As ResultRecall In tmp ' should be in order by newest date

            Dim newItemDate As DateTime = DateTime.ParseExact(itm.Recall_Initiation_Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim tmpReportDate As DateTime = DateTime.ParseExact(itm.Report_Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

            Dim tmpSearchResultItem As New SearchResultItem With {.City = itm.City,
                                                                  .DateStarted = newItemDate.ToString("ddMMMyyyy"),
                                                                  .Content = String.Format("{0} {1}", itm.Reason_For_Recall, itm.Code_info),
                                                                  .DistributionPattern = itm.Distribution_Pattern,
                                                                  .ProductDescription = itm.Product_Description,
                                                                  .State = itm.State,
                                                                  .Status = itm.Status,
                                                                  .Country = itm.Country,
                                                                  .RecallNumber = itm.Recall_Number,
                                                                  .ProductQuantity = itm.Product_Quantity,
                                                                  .EventId = itm.Event_Id,
                                                                  .RecallingFirm = itm.Recalling_Firm,
                                                                  .ReportDate = tmpReportDate.ToString("ddMMMyyyy"),
                                                                  .CodeInfo = itm.Code_info,
                                                                  .Classification = itm.Classification,
                                                                  .Voluntary = itm.Voluntary_Mandated}

            searchResultLocal.Results.Add(tmpSearchResultItem)

        Next

        ' Lets Get the Events And Mix them In.
        Dim drugee As New OpenFda
        Dim drugs As List(Of SearchResultDrugEvent)

        'Get Drug Events
        drugs = drugee.GetDrugEventsByDrugName(keyWord)
        searchResultLocal.Results.AddRange(drugs)

        'Get Device Events
        drugs = drugee.GetDeviceEventByDescription(keyWord)
        searchResultLocal.Results.AddRange(drugs)

        ''Sort for most recient at the top of the list

        ' searchResultLocal.Results cannot be sorted because 
        searchResultLocal.Results = (From el In searchResultLocal.Results Select el Order By CDate(el.SortDate) Descending).ToList()

        ' Now that the data is combined and sorted, Process data for maps and graphs
        For Each itm As Object In searchResultLocal.Results

            Dim classification As String = String.Empty
            Dim reportDate As String = String.Empty

            ' Map Processing first
            Select Case itm.GetType.ToString

                Case "ApiDalc.DataObjects.SearchResultItem"
                    ProcessResultRecordForRecall(CType(itm, SearchResultItem), mapList)
                    classification = CType(itm, SearchResultItemBase).Classification
                    reportDate = CType(itm, SearchResultItemBase).ReportDate

                Case "ApiDalc.DataObjects.SearchResultDrugEvent"
                    ProcessResultRecordForDrugEvent(CType(itm, SearchResultDrugEvent), mapList)
                    classification = CType(itm, SearchResultItemBase).Classification
                    reportDate = CType(itm, SearchResultItemBase).ReportDate

            End Select

            ' Now process graph data
            Dim tmpReportDate As DateTime = DateTime.ParseExact(reportDate, "ddMMMyyyy", System.Globalization.CultureInfo.InvariantCulture)

            Dim dateForReport As String = tmpReportDate.ToString("MMM-yyyy")
            Dim found As Boolean = False

            Select Case classification

                Case "Class I"

                    For i As Integer = 0 To graphData.Labels.Count - 1

                        If graphData.Labels(i) = dateForReport Then

                            found = True
                            graphData.Data1(i) += 1

                            Exit For

                        End If

                    Next

                    If Not found Then

                        graphData.Labels.Insert(0, dateForReport)
                        graphData.Data1.Insert(0, 1)
                        graphData.Data2.Insert(0, 0)
                        graphData.Data3.Insert(0, 0)
                        graphData.DataE.Insert(0, 0)

                    End If

                Case "Class II"

                    For i As Integer = 0 To graphData.Labels.Count - 1

                        If graphData.Labels(i) = dateForReport Then

                            found = True
                            graphData.Data2(i) += 1

                        End If

                    Next

                    If Not found Then

                        graphData.Labels.Insert(0, dateForReport)
                        graphData.Data1.Insert(0, 0)
                        graphData.Data2.Insert(0, 1)
                        graphData.Data3.Insert(0, 0)
                        graphData.DataE.Insert(0, 0)

                    End If

                Case "Class III"

                    For i As Integer = 0 To graphData.Labels.Count - 1

                        If graphData.Labels(i) = dateForReport Then

                            found = True
                            graphData.Data3(i) += 1

                        End If

                    Next

                    If Not found Then

                        graphData.Labels.Insert(0, dateForReport)
                        graphData.Data1.Insert(0, 0)
                        graphData.Data2.Insert(0, 0)
                        graphData.Data3.Insert(0, 1)
                        graphData.DataE.Insert(0, 0)

                    End If

                Case "Device Event", "Drug Event", "Event"

                    For i As Integer = 0 To graphData.Labels.Count - 1

                        If graphData.Labels(i) = dateForReport Then

                            found = True
                            graphData.DataE(i) += 1

                        End If

                    Next

                    If Not found Then

                        graphData.Labels.Insert(0, dateForReport)
                        graphData.Data1.Insert(0, 0)
                        graphData.Data2.Insert(0, 0)
                        graphData.Data3.Insert(0, 0)
                        graphData.DataE.Insert(0, 1)

                    End If

            End Select

        Next

        searchResultLocal.MapObjects = ConvertDictionaryMapObjectsToSearchResult(mapList)
        searchResultLocal.GraphObjects = graphData

        Return searchResultLocal

    End Function

#End Region

#Region " Private Methods "

    Private Function GetRecallInfoCounts(keyWord As String, state As String) As SearchSummary

        _fda = New OpenFda(_restClient)

        Dim searchSummaryForKeyword As New SearchSummary With {.Keyword = keyWord, .State = state}
        Dim filterType As FdaFilterTypes
        filterType = FdaFilterTypes.RecallReason

        Dim endPointList As New List(Of OpenFdaApiEndPoints)({OpenFdaApiEndPoints.FoodRecall, OpenFdaApiEndPoints.DrugRecall, OpenFdaApiEndPoints.DeviceRecall})

        Const maxresultsize As Integer = 0

        Dim filterList As New List(Of String)
        filterList.Add(keyWord)

        For Each endPoint In endPointList

            Dim endpointSearchSummary As SearchSummary = ExecuteSearchCounts(endPoint, filterType, filterList, maxresultsize, state, "classification")

            If endpointSearchSummary IsNot Nothing Then

                searchSummaryForKeyword.ClassICount += endpointSearchSummary.ClassICount
                searchSummaryForKeyword.ClassIICount += endpointSearchSummary.ClassIICount
                searchSummaryForKeyword.ClassIIICount += endpointSearchSummary.ClassIIICount

            End If

        Next

        searchSummaryForKeyword.EventCount = 0
        searchSummaryForKeyword.EventCount += _fda.GetDrugEventsByDrugNameCount(keyWord)
        searchSummaryForKeyword.EventCount += _fda.GetDeviceEventsByDescriptionCount(keyWord)

        Return searchSummaryForKeyword

    End Function

    Private Function GetRecallInfo(ByVal keyWord As String, state As String) As List(Of ResultRecall)

        _fda = New OpenFda(_restClient)

        OpenFdaApiHits = 0

        Dim apiUrl As String = String.Empty
        Dim searchResults As String
        Dim resultList As New List(Of ResultRecall)

        Dim endPointList As New List(Of OpenFdaApiEndPoints)({OpenFdaApiEndPoints.FoodRecall, OpenFdaApiEndPoints.DrugRecall, OpenFdaApiEndPoints.DeviceRecall})

        For Each endPointType In endPointList

            Dim filterList As New List(Of String)({state})

            'Limit first query to a 1 year window
            Dim beginDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1))
            Dim endDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(-1))

            _fda.ResetSearch()
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, filterList, FilterCompairType.And)
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.RecallReason, New List(Of String)({keyWord}), FilterCompairType.And)
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)

            apiUrl = _fda.BuildUrl(endPointType, MaxResultSetSize)

            searchResults = _fda.Execute(apiUrl)
            OpenFdaApiHits += 1

            Dim dataSetSize As Integer = _fda.GetMetaResults().Total()

            ' If there was not data in the 1 yr window the get all results.
            ' Check a 2 yr window for results.
            If dataSetSize = 0 Then

                endDate = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(-2))

                _fda.ResetSearch()
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, filterList, FilterCompairType.And)
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.RecallReason, New List(Of String)({keyWord}), FilterCompairType.And)
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)

                apiUrl = _fda.BuildUrl(endPointType, MaxResultSetSize)

                searchResults = _fda.Execute(apiUrl)
                OpenFdaApiHits += 1

                dataSetSize = _fda.GetMetaResults().Total()

            End If

            ' If there was not data in the 2 yr window the get all results.
            If dataSetSize = 0 Then

                _fda.ResetSearch()
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, filterList, FilterCompairType.And)
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.RecallReason, New List(Of String)({keyWord}), FilterCompairType.And)

                apiUrl = _fda.BuildUrl(endPointType, MaxResultSetSize)

                searchResults = _fda.Execute(apiUrl)
                OpenFdaApiHits += 1

                dataSetSize = _fda.GetMetaResults().Total()

            End If

            ' if total records int the Search request exceeds the max of 100 records per request
            ' then page through the data
            ' LIMIT the number of page request to a MAX of 5
            Dim pageLimit As Integer = CInt(Decimal.Ceiling(CDec(dataSetSize / 100)))
            If pageLimit > 5 Then
                pageLimit = 5
            End If

            Dim skipValue As Integer = 0
            If dataSetSize > 0 Then

                Do
                    pageLimit -= 1

                    If Not String.IsNullOrEmpty(searchResults) Then

                        Dim result As List(Of ResultRecall) = ResultRecall.CnvJsonDataToList(searchResults)
                        resultList.AddRange(result)

                    End If

                    If pageLimit > 0 Then

                        skipValue += 100
                        Dim newApiUrl As String = apiUrl.Replace("&limit=100", String.Format("&limit=100&skip={0}", skipValue))
                        searchResults = _fda.Execute(apiUrl)
                        OpenFdaApiHits += 1

                    End If

                Loop Until pageLimit = 0

            End If
        Next

        Dim sortedResultList As List(Of ResultRecall) = (From el In resultList Select el Order By el.Recall_Initiation_Date Descending).ToList()

        Return sortedResultList

        'Return resultList

    End Function

    Private Function ExecuteSearchCounts(endPointType As OpenFdaApiEndPoints, filterType As FdaFilterTypes, filterList As List(Of String), ByVal maxresultsize As Integer, ByVal state As String, ByVal cntField As String) As SearchSummary

        Dim apiUrl As String = String.Empty
        Dim tmpRecallResultList As New List(Of ResultRecall)

        Dim searchSummary As New SearchSummary With {.Keyword = filterList(0),
                                                     .State = state
                                                    }
        Dim searchResults As String

        'Limit first query to a 1 year window
        Dim beginDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1))
        Dim endDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(-1))

        _fda.ResetSearch()
        _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, New List(Of String)({state}), FilterCompairType.And)
        _fda.AddSearchFilter(endPointType, filterType, filterList, FilterCompairType.And)
        _fda.AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)

        _fda.AddCountField(String.Format("{0}.exact", cntField.ToLower))
        apiUrl = _fda.BuildUrl(endPointType)

        searchResults = _fda.Execute(apiUrl)

        ' If there was not data in the 1 yr window the get all results.
        ' Check a 2 yr window for results.
        If String.IsNullOrEmpty(searchResults) Then

            endDate = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(-2))

            _fda.ResetSearch()
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, New List(Of String)({state}), FilterCompairType.And)
            _fda.AddSearchFilter(endPointType, filterType, filterList, FilterCompairType.And)
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)

            _fda.AddCountField(String.Format("{0}.exact", cntField.ToLower))
            apiUrl = _fda.BuildUrl(endPointType)

            searchResults = _fda.Execute(apiUrl)

        End If

        ' If there was not data in the 2 yr window the get all results.
        If String.IsNullOrEmpty(searchResults) Then

            _fda.ResetSearch()
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, New List(Of String)({state}), FilterCompairType.And)
            _fda.AddSearchFilter(endPointType, filterType, filterList, FilterCompairType.And)

            _fda.AddCountField(String.Format("{0}.exact", cntField.ToLower))
            apiUrl = _fda.BuildUrl(endPointType)

            searchResults = _fda.Execute(apiUrl)

        End If

        If Not String.IsNullOrEmpty(searchResults) Then

            Dim jo As JObject = JObject.Parse(searchResults)
            Dim countResults As JArray = CType(jo("results"), JArray)

            Dim termCountFound As Boolean = False
            Dim termCount As Integer

            For Each itm In countResults

                termCount = CInt(itm("count"))

                Dim termClassification As String = CStr(itm("term"))

                Select Case termClassification

                    Case "Class I"

                        searchSummary.ClassICount = termCount
                        termCountFound = True

                    Case "Class II"

                        searchSummary.ClassIICount = termCount
                        termCountFound = True

                    Case "Class III"
                        searchSummary.ClassIIICount = termCount
                        termCountFound = True

                End Select

            Next

            If Not termCountFound Then
                searchSummary = Nothing
            End If

        End If

        Return searchSummary

    End Function

    Private Sub ProcessResultRecordForRecall(data As SearchResultItem, list As Dictionary(Of String, SearchResultMapData))

        Dim check As String = data.DistributionPattern
        Dim states As List(Of String) = System.Enum.GetNames(GetType(States)).ToList
        Dim nationwide As Boolean = False

        Try

            If check.ToLower.Contains("nationwide") Then
                nationwide = True
            End If

            For Each state As String In states

                Dim stEnum As Reflection.FieldInfo = GetType(States).GetField(state)
                Dim stateName As DescriptionAttribute = DirectCast(stEnum.GetCustomAttributes(GetType(DescriptionAttribute), False)(0), DescriptionAttribute)
                Dim stateCoords As DefaultValueAttribute = DirectCast(stEnum.GetCustomAttributes(GetType(DefaultValueAttribute), False)(0), DefaultValueAttribute)
                Dim coordPair As List(Of String) = stateCoords.Value.ToString.Split(CChar(";")).ToList

                If check.Contains(state) Or check.ToUpper.Contains(stateName.Description.ToUpper) Or nationwide Then

                    Dim listCheck As SearchResultMapData = Nothing

                    If list.ContainsKey(state) Then

                        listCheck = list(state)

                    Else

                        listCheck = New SearchResultMapData With {.State = state, .Latitude = coordPair(0), .Longitude = coordPair(1)}

                        list.Add(state, listCheck)

                    End If

                    Dim tooltip As String = String.Concat(data.Classification, " {0}")

                    Select Case data.Classification.ToLower

                        Case "class i"

                            tooltip = String.Format(tooltip, " Class-1")
                            listCheck.IconSet = (listCheck.IconSet Or IconSet.Class1)

                        Case "class ii"

                            tooltip = String.Format(tooltip, " Class-2")
                            listCheck.IconSet = (listCheck.IconSet Or IconSet.Class2)

                        Case "class iii"

                            tooltip = String.Format(tooltip, " Class-3")
                            listCheck.IconSet = (listCheck.IconSet Or IconSet.Class3)

                    End Select

                    If Not listCheck.Tooltip.Contains(tooltip) Then

                        If listCheck.Tooltip.Length > 0 Then
                            listCheck.Tooltip += ", "
                        End If

                        listCheck.Tooltip += tooltip

                    End If

                    ' Get the Rank
                    Dim rank As Integer = 9
                    Select Case data.Rank.ToLower
                        Case "classi" : rank = 1
                        Case "classii" : rank = 2
                        Case "classiii" : rank = 3
                        Case "events" : rank = 4
                        Case "device event" : rank = 5
                        Case Else : rank = 5

                    End Select

                    If listCheck.Rank > rank Then listCheck.Rank = rank

                    list(state) = listCheck

                End If

            Next

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub ProcessResultRecordForDrugEvent(data As SearchResultDrugEvent, list As Dictionary(Of String, SearchResultMapData))

        Dim check As String = "nationwide" ' add state field data.
        Dim states As List(Of String) = System.Enum.GetNames(GetType(States)).ToList
        Dim nationwide As Boolean = False

        Try

            If check.ToLower.Contains("nationwide") Then
                nationwide = True
            End If

            For Each state As String In states

                Dim stEnum As Reflection.FieldInfo = GetType(States).GetField(state)
                Dim stateName As DescriptionAttribute = DirectCast(stEnum.GetCustomAttributes(GetType(DescriptionAttribute), False)(0), DescriptionAttribute)
                Dim stateCoords As DefaultValueAttribute = DirectCast(stEnum.GetCustomAttributes(GetType(DefaultValueAttribute), False)(0), DefaultValueAttribute)
                Dim coordPair As List(Of String) = stateCoords.Value.ToString.Split(CChar(";")).ToList

                If check.Contains(state) Or check.ToUpper.Contains(stateName.Description.ToUpper) Or nationwide Then

                    Dim listCheck As SearchResultMapData = Nothing

                    If list.ContainsKey(state) Then
                        listCheck = list(state)
                    Else

                        listCheck = New SearchResultMapData With {.State = state, .Latitude = coordPair(0), .Longitude = coordPair(1)}
                        list.Add(state, listCheck)

                    End If

                    Dim tooltip As String = String.Concat(data.Rank)

                    listCheck.IconSet = (listCheck.IconSet Or IconSet.Event)

                    If Not listCheck.Tooltip.Contains(tooltip) Then

                        If listCheck.Tooltip.Length > 0 Then
                            listCheck.Tooltip += ", "
                        End If

                        listCheck.Tooltip += tooltip

                    End If

                    ' Get the Rank
                    Dim rank As Integer = 9
                    Select Case data.Rank.ToLower
                        Case "classi" : rank = 1
                        Case "classii" : rank = 2
                        Case "classiii" : rank = 3
                        Case "events" : rank = 4
                        Case "device event" : rank = 5
                        Case Else : rank = 5
                    End Select

                    If listCheck.Rank > rank Then listCheck.Rank = rank

                    list(state) = listCheck

                    list(state) = listCheck

                End If

            Next

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Function ConvertDictionaryMapObjectsToSearchResult(mapList As Dictionary(Of String, SearchResultMapData)) As List(Of SearchResultMapData)

        Dim result As New List(Of SearchResultMapData)

        For Each mapData As KeyValuePair(Of String, SearchResultMapData) In mapList
            result.Add(mapData.Value)
        Next

        Return result

    End Function

#End Region

End Class
