Imports Newtonsoft.Json.Linq

Public Class ShopAwareService

#Region " Private Members "

    Private _fda As OpenFDA

#End Region

#Region " Public Methods "


    Public Function GetItemCountByRegion(ByVal keyWord As String, ByVal state As String) As SearchSummary

        Dim results As SearchSummary = Nothing

        results = GetRecallInfoCounts(keyWord, state)

        Return results

    End Function


    Public Function GetRecallsSummary(ByVal keyWordList As List(Of String)) As List(Of RecallSearchResultData)

        Dim results As List(Of RecallSearchResultData)

        results = GetRecallInfo(keyWordList, 0)

        Return results

    End Function

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

        _fda = New OpenFDA

        Dim searchSummaryForKeyword As New SearchSummary With {.Keyword = keyWord}
        Dim filterType As FDAFilterTypes
        Dim endPointList As New List(Of OpenFDAApiEndPoints)({OpenFDAApiEndPoints.FoodRecall, OpenFDAApiEndPoints.DrugRecall})
        Const maxresultsize As Integer = 0

        Dim filterList As New List(Of String)
        filterList.Add(keyWord)

        'Dim recallResultList As List(Of ResultRecall)

        For Each endPoint In endPointList

            'recallResultList = New List(Of ResultRecall)
            Dim endpointSearchSummary As SearchSummary = ExecuteSearchCounts(endPoint, filterType, filterList, maxresultsize, "classification")

            If endpointSearchSummary IsNot Nothing Then
                
                searchSummaryForKeyword.ClassICount += endpointSearchSummary.ClassICount
                searchSummaryForKeyword.ClassIICount += endpointSearchSummary.ClassIICount
                searchSummaryForKeyword.ClassIIICount += endpointSearchSummary.ClassIIICount

                ' searchSummaryForKeyword.EventCount += endpointSearchSummary.EventCount

            End If

        Next

        Return searchSummaryForKeyword

    End Function

    Private Function GetRecallInfo(ByVal keyWordList As List(Of String), ByVal maxresultsize As Integer) As List(Of RecallSearchResultData)

        Dim results As New List(Of RecallSearchResultData)

        _fda = New OpenFDA

        Dim filterType As FDAFilterTypes = FDAFilterTypes.RecallReason

        Dim resultCount As Integer
        Dim RecallResultList As List(Of ResultRecall)

        For Each kwGroup In keyWordList

            Dim filterList As New List(Of String)
            Dim xx As String() = kwGroup.Split(",")
            For Each itm In xx
                filterList.Add(itm)
            Next

            Dim endPointList As New List(Of OpenFDAApiEndPoints)({OpenFDAApiEndPoints.FoodRecall, OpenFDAApiEndPoints.DrugRecall})

            For Each endPoint In endPointList

                RecallResultList = New List(Of ResultRecall)
                resultCount = ExecuteSearch(endPoint, filterType, filterList, maxresultsize, RecallResultList)

                For Each itm As ResultRecall In RecallResultList

                    Dim itmClassification As EnumClassification

                    Select Case itm.Classification
                        Case "Class I"
                            itmClassification = EnumClassification.Class_I

                        Case "Class II"
                            itmClassification = EnumClassification.Class_II

                        Case "Class III"
                            itmClassification = EnumClassification.Class_III

                    End Select

                    itm.KeyWord = kwGroup

                    Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
                                                                       .Type = itm.Product_Type,
                                                                       .Count = resultCount,
                                                                       .Classification = String.Format("{0}  -  {1}", itm.Classification, GetEnumDescription(itmClassification)),
                                                                       .Description_1 = itm.Product_Description,
                                                                       .Description_2 = itm.Reason_For_Recall}

                    RecallData_AddPropertyInfo(recallData, itm)

                    results.Add(recallData)

                Next

            Next

            'RecallResultList = New List(Of ResultRecall)
            'resultCount = ExecuteSearch(OpenFDAApiEndPoints.FoodRecall, filterType, filterList, maxresultsize, RecallResultList)

            'For Each itm As ResultRecall In RecallResultList

            '    itm.KeyWord = kwGroup
            '    Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
            '                                                       .Type = itm.product_type,
            '                                                       .Count = resultCount,
            '                                                       .Description_1 = itm.product_description,
            '                                                       .Description_2 = itm.reason_for_recall}

            '    recallData_AddPropertyInfo(recallData, itm)

            '    results.Add(recallData)

            'Next

            'RecallResultList = New List(Of ResultRecall)
            'resultCount = ExecuteSearch(OpenFDAApiEndPoints.DrugRecall, filterType, filterList, maxresultsize, RecallResultList)

            'For Each itm As ResultRecall In RecallResultList

            '    itm.KeyWord = kwGroup
            '    Dim recallData As New RecallSearchResultData With {.KeyWord = kwGroup,
            '                                                       .Type = itm.product_type,
            '                                                       .Count = resultCount,
            '                                                       .Description_1 = itm.product_description,
            '                                                       .Description_2 = itm.reason_for_recall}

            '    recallData_AddPropertyInfo(recallData, itm)

            '    results.Add(recallData)

            'Next

        Next

        Return results

    End Function

    Private Function ExecuteSearch(endPointType As OpenFDAApiEndPoints, filterType As FDAFilterTypes, filterList As List(Of String), ByVal maxresultsize As Integer, ByRef RecallResultList As List(Of ResultRecall)) As Integer

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
                    RecallResultList.Add(itm)
                Next

            End If

        End If

        'Return tmpRecallResultList.Count > 0
        Return srMetaData.Total

    End Function

    Private Function ExecuteSearchCounts(endPointType As OpenFDAApiEndPoints, filterType As FDAFilterTypes, filterList As List(Of String), ByVal maxresultsize As Integer, ByVal cntField As String) As SearchSummary

        'Dim fda As New OpenFDA
        Dim apiUrl As String = String.Empty
        'Dim searchResults As String
        Dim srMetaData As MetaResults
        Dim tmpRecallResultList As New List(Of ResultRecall)

        Dim searchSummary As New SearchSummary With {.Keyword = filterList(0)}


        _fda.AddSearchFilter(endPointType, filterType, filterList)
        apiUrl = _fda.BuildUrl(endPointType, maxresultsize)
        apiUrl += String.Format("&count={0}.exact", cntField.ToLower)
        Dim searchResults As String = _fda.Execute(apiUrl)

        If Not String.IsNullOrEmpty(searchResults) Then

            Dim jo As JObject = JObject.Parse(searchResults)

            Dim countResults As JArray = jo("results")
            'Dim countResults_1 As JObject = jo("results")

            Dim termCountFound As Boolean = False

            Dim termCount As Integer
            For Each itm In countResults

                ' termCount = Integer.TryParse(itm("count"), termCount)

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

        items = System.Enum.GetValues(GetType(EnumStates))

        'Dim item As String
        Dim tmpState As EnumStates

        For Each item In items

            tmpState = DirectCast([Enum].Parse(GetType(EnumStates), item), EnumStates)

            If itm.Distribution_Pattern.Contains(tmpState.ToString) OrElse
                itm.Distribution_Pattern.Contains(GetEnumDescription(tmpState)) OrElse
                recallData.IsNationWide Then

                recallData.Regions.Add(tmpState.ToString)

            End If

        Next

    End Sub

#End Region

End Class
