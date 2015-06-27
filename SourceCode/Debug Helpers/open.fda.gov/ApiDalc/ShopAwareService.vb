#Region " Imports "

Imports Newtonsoft.Json.Linq
Imports ApiDalc.DataObjects
Imports ApiDalc.Enumerations
Imports System.ComponentModel

#End Region

Public Class ShopAwareService
    Public Property OpenFdaApiHits As Integer

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
    ''' This Gets a count of Issues. The count is based on Classifications (Class I, Class II, Class III) and drug event
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
    ''' Get search results data
    ''' </summary>
    ''' <param name="keyWord"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSearchResult(ByVal keyWord As String, ByVal state As String) As SearchResult

        Const maxResultSetSize As Integer = 100

        Dim searchResultLocal As New SearchResult With {.Keyword = keyWord}
        Dim mapList As New Dictionary(Of String, SearchResultMapData)
        Dim graphList As New Dictionary(Of String, List(Of ReportData))

        graphList.Add("class1", New List(Of ReportData))
        graphList.Add("class2", New List(Of ReportData))
        graphList.Add("class3", New List(Of ReportData))

        Dim tmp As List(Of ResultRecall) = GetRecallInfo(keyWord, state, maxResultSetSize)

        For Each itm As ResultRecall In tmp

            ProcessResultRecordForData(itm, mapList)

            ' ------------------------------------------------------------
            'TODO convert itm (ResultRecall) to SearchResultItem
            ' ------------------------------------------------------------

            'Dim newItemDate As DateTime = ConvertDateStringToDate(itm.Recall_Initiation_Date, "yyyyMMdd")
            'Dim tmpReportDate As DateTime = ConvertDateStringToDate(itm.Report_Date, "yyyyMMdd")
            Dim newItemDate As DateTime = DateTime.ParseExact(itm.Recall_Initiation_Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim tmpReportDate As DateTime = DateTime.ParseExact(itm.Report_Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

            Dim tmpSearchResultItem As New SearchResultItem With {.City = itm.City,
                                                                  .DateStarted = newItemDate.ToShortDateString(),
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
                                                                  .ReportDate = tmpReportDate.ToShortDateString(),
                                                                  .CodeInfo = itm.Code_info,
                                                                  .Voluntary = itm.Voluntary_Mandated}

            Dim itmDate As DateTime = Nothing
            Select Case itm.Classification

                Case "Class I"

                    searchResultLocal.ClassI.Add(tmpSearchResultItem)

                Case "Class II"

                    searchResultLocal.ClassII.Add(tmpSearchResultItem)

                Case "Class III"

                    searchResultLocal.ClassIII.Add(tmpSearchResultItem)

            End Select

        Next

        searchResultLocal.MapObjects = ConvertDictionaryMapObjectsToSearchResult(mapList)

        LimitResultsOfSearchResultItems(searchResultLocal.ClassI, maxResultSetSize)
        LimitResultsOfSearchResultItems(searchResultLocal.ClassII, maxResultSetSize)
        LimitResultsOfSearchResultItems(searchResultLocal.ClassIII, maxResultSetSize)

        Return searchResultLocal

    End Function

    ''' <summary>
    ''' Get report data item by region
    ''' </summary>
    ''' <param name="keyword"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReportDataItemByRegion(ByVal keyword As String, ByVal state As String) As ReportData

        _fda = New OpenFda(_restClient)

        Return _fda.GetReportDataRecallReasonByReportDate(keyword, state)

    End Function

    ''' <summary>
    ''' Get States
    ''' </summary>
    ''' <returns>List of states</returns>
    ''' <remarks></remarks>
    Public Function GetStates() As List(Of StateData)

        _fda = New OpenFda()

        Dim results As List(Of StateData)

        results = _fda.GetStates()

        Return results

    End Function

    Public Function GetFDAResult(ByVal keyWord As String, ByVal state As String) As FDAResult

        Const maxResultSetSize As Integer = 100

        Dim searchResultLocal As New FDAResult With {.Keyword = keyWord}

        Dim mapList As New Dictionary(Of String, SearchResultMapData)
        
        Dim graphData As New ReportData

        Dim tmp As List(Of ResultRecall) = GetRecallInfo(keyWord, state, maxResultSetSize)

        Dim values As New FDAResult

        For Each itm As ResultRecall In tmp

            ProcessResultRecordForData(itm, mapList)

            ' ------------------------------------------------------------
            'TODO convert itm (ResultRecall) to SearchResultItem
            ' ------------------------------------------------------------

            'Dim newItemDate As DateTime = ConvertDateStringToDate(itm.Recall_Initiation_Date, "yyyyMMdd")
            'Dim tmpReportDate As DateTime = ConvertDateStringToDate(itm.Report_Date, "yyyyMMdd")
            Dim newItemDate As DateTime = DateTime.ParseExact(itm.Recall_Initiation_Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim tmpReportDate As DateTime = DateTime.ParseExact(itm.Report_Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

            Dim tmpSearchResultItem As New SearchResultItem With {.City = itm.City,
                                                                  .DateStarted = newItemDate.ToShortDateString(),
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
                                                                  .ReportDate = tmpReportDate.ToShortDateString(),
                                                                  .CodeInfo = itm.Code_info,
                                                                  .Classification = itm.Classification,
                                                                  .Voluntary = itm.Voluntary_Mandated}

            searchResultLocal.Results.Add(tmpSearchResultItem)

            Dim dateForReport As String = tmpReportDate.ToString("MMyyyy")
            Dim found As Boolean = False

            Select Case itm.Classification

                Case "Class I"

                    For i As Integer = 0 To graphData.Labels.Count - 1

                        If graphData.Labels(i) = dateForReport Then

                            found = True
                            graphData.Data1(i) += 1

                        End If

                    Next

                    If Not found Then

                        graphData.Labels.Add(dateForReport)
                        graphData.Data1.Add(1)
                        graphData.Data2.Add(0)
                        graphData.Data3.Add(0)

                    End If

                Case "Class II"

                    For i As Integer = 0 To graphData.Labels.Count - 1

                        If graphData.Labels(i) = dateForReport Then

                            found = True
                            graphData.Data2(i) += 1

                        End If

                    Next

                    If Not found Then

                        graphData.Labels.Add(dateForReport)
                        graphData.Data1.Add(0)
                        graphData.Data2.Add(1)
                        graphData.Data3.Add(0)

                    End If

                Case "Class III"

                    For i As Integer = 0 To graphData.Labels.Count - 1

                        If graphData.Labels(i) = dateForReport Then

                            found = True
                            graphData.Data3(i) += 1

                        End If

                    Next

                    If Not found Then

                        graphData.Labels.Add(dateForReport)
                        graphData.Data1.Add(0)
                        graphData.Data2.Add(0)
                        graphData.Data3.Add(1)

                    End If

            End Select

        Next

        searchResultLocal.MapObjects = ConvertDictionaryMapObjectsToSearchResult(mapList)
        searchResultLocal.GraphObjects = graphData

        ' Lets Get the Events And Mix them In.
        Dim drugee As New OpenFda
        Dim drugs As List(Of SearchResultDrugEvent)

        'Get Drug Events
        drugs = drugee.GetDrugEventsByDrugName(keyWord)
        searchResultLocal.Results.AddRange(drugs)

        'Get Device Events
        drugs = drugee.GetDeviceEventByDescription(keyWord)
        searchResultLocal.Results.AddRange(drugs)

        Dim tmpLinqResults = (From el In searchResultLocal.Results Select el Order By CDate(el.DateStarted) Descending).ToList()

        If tmpLinqResults.Count > maxResultSetSize Then
            tmpLinqResults.RemoveRange(maxResultSetSize, tmpLinqResults.Count - maxResultSetSize)
        End If

        searchResultLocal.Results = tmpLinqResults

        Return searchResultLocal

    End Function

    Public Function GetFeatureCollection() As List(Of FeatureObject)


        Return Nothing
    End Function

#End Region

#Region " Private Methods "

    Private Function GetRecallInfoCounts(keyWord As String, state As String) As SearchSummary

        _fda = New OpenFda(_restClient)

        'TODO: Need to query Drug/Events

        Dim searchSummaryForKeyword As New SearchSummary With {.Keyword = keyWord}
        Dim filterType As FdaFilterTypes
        filterType = FdaFilterTypes.RecallReason

        Dim endPointList As New List(Of OpenFdaApiEndPoints)({OpenFdaApiEndPoints.FoodRecall, OpenFdaApiEndPoints.DrugRecall, OpenFdaApiEndPoints.DeviceRecall})

        Const maxresultsize As Integer = 0

        Dim filterList As New List(Of String)
        filterList.Add(keyWord)

        'Dim recallResultList As List(Of ResultRecall)

        For Each endPoint In endPointList

            'recallResultList = New List(Of ResultRecall)
            Dim endpointSearchSummary As SearchSummary = ExecuteSearchCounts(endPoint, filterType, filterList, maxresultsize, state, "classification")

            If endpointSearchSummary IsNot Nothing Then

                searchSummaryForKeyword.ClassICount += endpointSearchSummary.ClassICount
                searchSummaryForKeyword.ClassIICount += endpointSearchSummary.ClassIICount
                searchSummaryForKeyword.ClassIIICount += endpointSearchSummary.ClassIIICount

                ' searchSummaryForKeyword.EventCount += endpointSearchSummary.EventCount

            End If

            searchSummaryForKeyword.EventCount = _fda.GetDrugEventsByDrugNameCount(keyWord)

        Next

        Return searchSummaryForKeyword

    End Function

    Private Function GetRecallInfo(ByVal keyWord As String, state As String, resultSize As Integer) As List(Of ResultRecall)

        _fda = New OpenFda(_restClient)

        OpenFdaApiHits = 0
        resultSize = 100

        Dim apiUrl As String = String.Empty
        Dim searchResults As String
        Dim resultList As New List(Of ResultRecall)

        Dim endPointList As New List(Of OpenFdaApiEndPoints)({OpenFdaApiEndPoints.FoodRecall, OpenFdaApiEndPoints.DrugRecall, OpenFdaApiEndPoints.DeviceRecall})

        '' Dim classificationList As New List(Of String)({"Class I", "Class II", "Class III"})

        For Each endPointType In endPointList

            ''  For Each cc In classificationList

            Dim filterList As New List(Of String)({state})

            'Limit first query to a 1 year window

            Dim beginDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1))
            Dim endDate As String = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(-1))

            _fda.ResetSearch()
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, filterList, FilterCompairType.And)
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.RecallReason, New List(Of String)({keyWord}), FilterCompairType.And)
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)
            '' _fda.AddSearchFilter(endPointType, "classification", cc, FilterCompairType.And)

            apiUrl = _fda.BuildUrl(endPointType, resultSize)

            searchResults = _fda.Execute(apiUrl)
            OpenFdaApiHits += 1

            Dim dataSetSize As Integer = _fda.GetMetaResults().Total()

            ' If there was not data in the 1 yr window the get all results.
            ' Check a 2 yr window for results.
            If dataSetSize = 0 Then


                'beginDate = String.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1))
                endDate = String.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(-2))

                _fda.ResetSearch()
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, filterList, FilterCompairType.And)
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.RecallReason, New List(Of String)({keyWord}), FilterCompairType.And)
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Date, New List(Of String)({beginDate, endDate}), FilterCompairType.And)
                ''  _fda.AddSearchFilter(endPointType, "classification", cc, FilterCompairType.And)

                apiUrl = _fda.BuildUrl(endPointType, resultSize)

                searchResults = _fda.Execute(apiUrl)
                OpenFdaApiHits += 1

                dataSetSize = _fda.GetMetaResults().Total()

            End If

            ' If there was not data in the 2 yr window the get all results.
            If dataSetSize = 0 Then

                _fda.ResetSearch()
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, filterList, FilterCompairType.And)
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.RecallReason, New List(Of String)({keyWord}), FilterCompairType.And)
                ''  _fda.AddSearchFilter(endPointType, "classification", cc, FilterCompairType.And)

                apiUrl = _fda.BuildUrl(endPointType, resultSize)

                searchResults = _fda.Execute(apiUrl)
                OpenFdaApiHits += 1

                dataSetSize = _fda.GetMetaResults().Total()

            End If

            ' if total records int the Search request exceeds the max of 100 records per request
            ' then page through the data
            ' LIMIT the number of page request to a MAX of 5
            Dim pageLimit As Integer = CInt(Decimal.Ceiling(dataSetSize / 100))
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

            ''Next

        Next

        Return resultList

    End Function

    Private Function GetRecallInfo(ByVal keyWordList As List(Of String), ByVal maxresultsize As Integer) As List(Of RecallSearchResultData)

        _fda = New OpenFda(_restClient)

        Dim results As New List(Of RecallSearchResultData)

        Dim filterType As FdaFilterTypes
        filterType = FdaFilterTypes.RecallReason

        Dim resultCount As Integer
        Dim recallResultList As New List(Of ResultRecall)

        For Each kwGroup In keyWordList

            Dim filterList As New List(Of String)
            Dim kwGroupArray As String() = kwGroup.Split(",")

            For Each itm In kwGroupArray
                filterList.Add(itm)
            Next

            Dim endPointList As New List(Of OpenFdaApiEndPoints)({OpenFdaApiEndPoints.FoodRecall, OpenFdaApiEndPoints.DrugRecall, OpenFdaApiEndPoints.DeviceRecall})

            For Each endPoint In endPointList

                recallResultList = New List(Of ResultRecall)
                resultCount = ExecuteSearch(endPoint, filterType, filterList, maxresultsize, recallResultList)

                For Each itm As ResultRecall In recallResultList

                    Dim itmClassification As Classification

                    Select Case itm.Classification
                        Case "Class I"
                            itmClassification = Classification.Class_I

                        Case "Class II"
                            itmClassification = Classification.Class_II

                        Case "Class III"
                            itmClassification = Classification.Class_III

                    End Select

                    'itm.KeyWord = kwGroup

                    Dim recallData As New RecallSearchResultData With {.KeyWord = itm.KeyWord,
                                                                       .Type = itm.Product_Type,
                                                                       .Count = resultCount,
                                                                       .Classification = String.Format("{0}  -  {1}", itm.Classification, GetEnumDescription(itmClassification)),
                                                                       .ProductDescription = itm.Product_Description,
                                                                       .ReasonForRecall = itm.Reason_For_Recall}

                    RecallData_AddPropertyInfo(recallData, itm)
                    results.Add(recallData)

                Next

            Next

        Next

        Return results

    End Function

    Private Function ExecuteSearch(endPointType As OpenFdaApiEndPoints, filterType As FdaFilterTypes, filterList As List(Of String), ByVal maxresultsize As Integer, ByRef recallResultList As List(Of ResultRecall)) As Integer

        Dim apiUrl As String = String.Empty
        Dim searchResults As String
        Dim srMetaData As MetaResults
        Dim tmpRecallResultList As New List(Of ResultRecall)

        _fda.AddSearchFilter(endPointType, filterType, filterList)
        apiUrl = _fda.BuildUrl(endPointType, maxresultsize)
        searchResults = _fda.Execute(apiUrl)

        srMetaData = MetaResults.CnvJsonData(searchResults)

        If srMetaData.Total > 0 Then

            tmpRecallResultList = ResultRecall.CnvJsonDataToList(searchResults)

            If tmpRecallResultList.Count > 0 Then

                For Each itm As ResultRecall In tmpRecallResultList

                    itm.KeyWord = filterList(0)
                    recallResultList.Add(itm)

                Next

            End If

        End If

        Return srMetaData.Total

    End Function

    Private Function ExecuteSearchCounts(endPointType As OpenFdaApiEndPoints, filterType As FdaFilterTypes, filterList As List(Of String), ByVal maxresultsize As Integer, ByVal state As String, ByVal cntField As String) As SearchSummary

        Dim apiUrl As String = String.Empty
        Dim tmpRecallResultList As New List(Of ResultRecall)

        Dim searchSummary As New SearchSummary With {.Keyword = filterList(0)}

        _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, New List(Of String)({state}), FilterCompairType.And)
        _fda.AddSearchFilter(endPointType, filterType, filterList, FilterCompairType.And)

        apiUrl = _fda.BuildUrl(endPointType, maxresultsize)
        apiUrl += String.Format("&count={0}.exact", cntField.ToLower)

        Dim searchResults As String = _fda.Execute(apiUrl)

        If Not String.IsNullOrEmpty(searchResults) Then

            Dim jo As JObject = JObject.Parse(searchResults)
            Dim countResults As JArray = jo("results")

            Dim termCountFound As Boolean = False
            Dim termCount As Integer

            For Each itm In countResults

                termCount = itm("count")

                Select Case itm("term")
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

    Private Sub RecallData_AddPropertyInfo(ByRef recallData As RecallSearchResultData, ByVal itm As ResultRecall)

        If itm.Distribution_Pattern.ToLower.Contains("nationwide") Then
            recallData.IsNationWide = True
        End If

        If Not String.IsNullOrEmpty(itm.State) Then
            recallData.Regions.Add(itm.State)
        End If

        Dim items As Array

        items = System.Enum.GetValues(GetType(States))

        Dim tmpState As States

        For Each item As String In items

            tmpState = DirectCast([Enum].Parse(GetType(States), item), States)

            If itm.Distribution_Pattern.Contains(tmpState.ToString) OrElse
                itm.Distribution_Pattern.Contains(GetEnumDescription(tmpState)) OrElse
                recallData.IsNationWide Then

                recallData.Regions.Add(tmpState.ToString)

            End If

        Next

    End Sub

    Private Sub ProcessResultRecordForData(data As ResultRecall, list As Dictionary(Of String, SearchResultMapData))

        Dim check As String = data.Distribution_Pattern
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
                Dim coordPair As List(Of String) = stateCoords.Value.ToString.Split(";").ToList

                If check.Contains(state) Or nationwide Then

                    Dim listCheck As SearchResultMapData = Nothing

                    If list.ContainsKey(state) Then
                        listCheck = list(state)
                    Else

                        listCheck = New SearchResultMapData With {.State = state, .Latitude = coordPair(0), .Longitude = coordPair(1)}
                        list.Add(state, listCheck)

                    End If

                    Dim tooltip As String = String.Concat(data.Product_Type, " {0}")

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

    Private Sub AddSearchResultItemToClassificication(searchResultList As List(Of SearchResultItem), tmpSearchResultItem As SearchResultItem, maxResultSetSize As Integer)

        Dim itmDate As DateTime
        Dim newItemDate As DateTime

        DateTime.TryParse(tmpSearchResultItem.DateStarted, newItemDate)

        If searchResultList.Count = 0 Then
            searchResultList.Add(tmpSearchResultItem)
        Else

            Dim itemAdded = False

            For ndx As Integer = 0 To searchResultList.Count - 1

                DateTime.TryParse(searchResultList(ndx).DateStarted, itmDate)

                If newItemDate > itmDate Then

                    searchResultList.Insert(ndx, tmpSearchResultItem)
                    itemAdded = True
                    Exit For

                End If

            Next

            If Not itemAdded AndAlso searchResultList.Count < maxResultSetSize Then
                searchResultList.Add(tmpSearchResultItem)
            End If

        End If

    End Sub

    Private Sub LimitResultsOfSearchResultItems(searchResultItemList As List(Of SearchResultItem), maxResultSetSize As Integer)

        'If searchResultLocal.ClassI.Count > maxResultSetSize Then
        Dim tmpLinqResults = (From el In searchResultItemList Select el Order By CDate(el.DateStarted) Descending).ToList()

        If tmpLinqResults.Count > maxResultSetSize Then
            tmpLinqResults.RemoveRange(maxResultSetSize, tmpLinqResults.Count - maxResultSetSize)
        End If

        searchResultItemList.Clear()
        searchResultItemList.AddRange(tmpLinqResults)

    End Sub


#End Region


End Class
