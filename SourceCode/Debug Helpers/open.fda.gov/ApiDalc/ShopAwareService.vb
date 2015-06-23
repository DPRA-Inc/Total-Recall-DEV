#Region " Imports "

Imports Newtonsoft.Json.Linq
Imports ApiDalc.DataObjects
Imports ApiDalc.Enumerations
Imports System.ComponentModel

#End Region

Public Class ShopAwareService

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

    Public Function GetSearchResult(ByVal keyWord As String, ByVal state As String) As SearchResult

        Const maxResultSetSize As Integer = 3

        Dim searchResultLocal As New SearchResult With {.Keyword = keyWord}
        Dim mapList As New Dictionary(Of String, SearchResultMapData)

        'Dim searchSummaryLocal = GetRecallInfoCounts(keyWord, state)

        Dim tmp As List(Of ResultRecall) = GetRecallInfo(keyWord, state, maxResultSetSize)

        For Each itm As ResultRecall In tmp

            ProcessResultRecordForMapData(itm, mapList)

            ' ------------------------------------------------------------
            'TODO convert itm (ResultRecall) to SearchResultItem
            ' ------------------------------------------------------------

            Dim tmpDate As DateTime = DateTime.ParseExact(itm.Recall_Initiation_Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim tmpSearchResultItem As New SearchResultItem With {.City = itm.City,
                                                                  .DateStarted = tmpDate.ToShortDateString(),
                                                                  .Content = String.Format("{0} {1}", itm.Reason_For_Recall, itm.Code_info),
                                                                  .DistributionPattern = itm.Distribution_Pattern,
                                                                  .ProductDescription = itm.Product_Description,
                                                                  .State = itm.State,
                                                                  .Status = itm.Status,
                                                                  .Voluntary = itm.Voluntary_Mandated}


            Dim itmDate As DateTime = Nothing
            Select Case itm.Classification

                Case "Class I"

                    If searchResultLocal.ClassI.Count < maxResultSetSize Then
                        'searchResultLocal.ClassI.Add(itm)
                        If searchResultLocal.ClassI.Count < maxResultSetSize Then

                            If searchResultLocal.ClassI.Count = 0 Then
                                searchResultLocal.ClassI.Add(tmpSearchResultItem)
                            Else

                                For ndx As Integer = 0 To searchResultLocal.ClassI.Count - 1
                                    DateTime.TryParse(searchResultLocal.ClassI(ndx).DateStarted, itmDate)
                                    'itmDate = DateTime.ParseExact(searchResultLocal.ClassI(ndx).DateStarted, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

                                    If tmpDate > itmDate Then

                                        searchResultLocal.ClassI.Insert(ndx, tmpSearchResultItem)
                                        Exit For

                                    End If

                                Next

                            End If
                            ' searchResultLocal.ClassI.Add(tmpSearchResultItem)

                        End If

                    End If

                Case "Class II"

                    If searchResultLocal.ClassII.Count < maxResultSetSize Then
                        'searchResultLocal.ClassII.Add(itm)
                        If searchResultLocal.ClassII.Count < maxResultSetSize Then

                            If searchResultLocal.ClassII.Count = 0 Then
                                searchResultLocal.ClassII.Add(tmpSearchResultItem)
                            Else

                                For ndx As Integer = 0 To searchResultLocal.ClassII.Count - 1
                                    DateTime.TryParse(searchResultLocal.ClassII(ndx).DateStarted, itmDate)
                                    'itmDate = DateTime.ParseExact(searchResultLocal.ClassII(ndx).DateStarted, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

                                    If tmpDate > itmDate Then

                                        searchResultLocal.ClassII.Insert(ndx, tmpSearchResultItem)
                                        Exit For

                                    End If

                                Next

                            End If
                            ' searchResultLocal.ClassII.Add(tmpSearchResultItem)

                        End If

                    End If

                Case "Class III"

                    If searchResultLocal.ClassIII.Count < maxResultSetSize Then
                        'searchResultLocal.ClassIII.Add(itm)
                        If searchResultLocal.ClassIII.Count < maxResultSetSize Then

                            If searchResultLocal.ClassIII.Count = 0 Then
                                searchResultLocal.ClassIII.Add(tmpSearchResultItem)
                            Else

                                For ndx As Integer = 0 To searchResultLocal.ClassIII.Count - 1
                                    DateTime.TryParse(searchResultLocal.ClassIII(ndx).DateStarted, itmDate)
                                    'itmDate = DateTime.ParseExact(searchResultLocal.ClassIII(ndx).DateStarted, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

                                    If tmpDate > itmDate Then

                                        searchResultLocal.ClassIII.Insert(ndx, tmpSearchResultItem)
                                        Exit For

                                    End If

                                Next

                            End If
                            ' searchResultLocal.ClassIII.Add(tmpSearchResultItem)

                        End If

                    End If

            End Select

        Next

        searchResultLocal.MapObjects = ConvertDictionaryMapObjectsToSearchResult(mapList)

        Return searchResultLocal

    End Function

    ''' <summary>
    ''' This gets the Top recall result for the KeyWord
    ''' </summary>
    ''' <param name="keyWordList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecallsSummary(ByVal keyWordList As List(Of String)) As List(Of RecallSearchResultData)

        Dim results As List(Of RecallSearchResultData)

        results = GetRecallInfo(keyWordList, 0)

        Return results

    End Function

    ''' <summary>
    ''' Returns the 
    ''' </summary>
    ''' <param name="keyWord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecallsDetail(ByVal keyWord As String) As List(Of RecallSearchResultData)

        Dim results As List(Of RecallSearchResultData)
        Dim keyWordList As New List(Of String)

        keyWordList.Add(keyWord)

        results = GetRecallInfo(keyWordList, 100)

        Return results

    End Function

#End Region

#Region " Private Methods "

    Private Function GetRecallInfoCounts(keyWord As String, state As String) As SearchSummary

        'TODO: Need to query Drug/Events

        _fda = New OpenFda(_restClient)

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

        Next

        Return searchSummaryForKeyword

    End Function

    Private Function GetRecallInfo(ByVal keyWord As String, state As String, resultSize As Integer) As List(Of ResultRecall)


        _fda = New OpenFda(_restClient)
        Dim apiUrl As String = String.Empty
        Dim searchResults As String
        Dim resultList As New List(Of ResultRecall)

        Dim endPointList As New List(Of OpenFdaApiEndPoints)({OpenFdaApiEndPoints.FoodRecall, OpenFdaApiEndPoints.DrugRecall, OpenFdaApiEndPoints.DeviceRecall})

        Dim classificationList As New List(Of String)({"Class I", "Class II", "Class III"})

        For Each endPointType In endPointList

            For Each cc In classificationList

                Dim filterList As New List(Of String)({state})

                _fda.ResetSearch()

                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, filterList, FilterCompairType.And)
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.RecallReason, New List(Of String)({keyWord}), FilterCompairType.And)
                _fda.AddSearchFilter(endPointType, "classification", cc, FilterCompairType.And)

                apiUrl = _fda.BuildUrl(endPointType, resultSize)

                searchResults = _fda.Execute(apiUrl)

                If Not String.IsNullOrEmpty(searchResults) Then

                    Dim result As List(Of ResultRecall) = ResultRecall.CnvJsonDataToList(searchResults)

                    For Each tmpItm In result

                        'If resultList.Count < resultSize Then
                        resultList.Add(tmpItm)
                        'Else
                        '    Exit For
                        'End If

                    Next

                End If

            Next

        Next


        Return resultList


        'tmpRecallResultList = ResultRecall.CnvJsonDataToList(searchResults)


    End Function

    Private Function GetRecallInfo(ByVal keyWordList As List(Of String), ByVal maxresultsize As Integer) As List(Of RecallSearchResultData)

        Dim results As New List(Of RecallSearchResultData)

        _fda = New OpenFda(_restClient)

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

                    itm.KeyWord = kwGroup

                    Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
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

        'Dim fda As New OpenFDA
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
                    recallResultList.Add(itm)
                Next

            End If

        End If

        'Return tmpRecallResultList.Count > 0
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
        'Dim item As String

        For Each item In items

            tmpState = DirectCast([Enum].Parse(GetType(States), item), States)

            If itm.Distribution_Pattern.Contains(tmpState.ToString) OrElse
                itm.Distribution_Pattern.Contains(GetEnumDescription(tmpState)) OrElse
                recallData.IsNationWide Then

                recallData.Regions.Add(tmpState.ToString)

            End If

        Next

    End Sub

    Private Sub ProcessResultRecordForMapData(data As ResultRecall, ByRef list As Dictionary(Of String, SearchResultMapData))

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

#End Region

End Class
