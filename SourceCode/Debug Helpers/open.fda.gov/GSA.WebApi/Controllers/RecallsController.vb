

#Region " Imports "

Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports ApiDalc
Imports ApiDalc.DataObjects
Imports ApiDalc.Enumerations

#End Region

Namespace Controllers
    Public Class RecallsController
        Inherits ApiController
        
        <HttpGet>
        Public Function GetRecalls() As HttpResponseMessage

            'Dim msg As HttpResponseMessage = Nothing

            'Dim fda As New OpenFDA
            'Dim filterType As FDAFilterTypes = FDAFilterTypes.RecallReason

            'fda.AddSearchFilter(filterType, fl)


            'Dim personList As List(Of DataObjects.Person) = (New ApiDalc..GetAllPerson()

            'If personList Is Nothing OrElse personList.Count = 0 Then
            '    Return Request.CreateErrorResponse(HttpStatusCode.NotFound, "recordsNotFound")
            'Else
            '    'Return Request.CreateResponse(Of DataObjects.ItemList)(HttpStatusCode.OK, result)

            '    'Dim result As New List(Of Item_sc)
            '    'For Each itm In ItemList
            '    '    result.Add(Item_sc.FromDataObject(itm))
            '    'Next
            '    Return Request.CreateResponse(Of List(Of DataObjects.Person))(HttpStatusCode.OK, personList)

            'End If
            Return Request.CreateErrorResponse(HttpStatusCode.NotFound, "recordsNotFound")
        End Function


        <Route("api/Recalls/{keyword}")>
        <HttpGet>
        Public Function GetRecalls(ByVal keyword As String) As HttpResponseMessage

            Return GetRecalls(keyword, 1)


        End Function

        <Route("api/Recalls/{keyword}/{maxresultsize}")>
        <HttpGet>
        Public Function GetRecalls(ByVal keyword As String, ByVal maxresultsize As Integer) As HttpResponseMessage

            Dim keyWordList As String() = keyword.Split(";")


            'TODO:  Add Filter of Status = OnGoing
            Dim msg As HttpResponseMessage = Nothing

            Dim fda As New OpenFDA
            Dim filterType As FdaFilterTypes = FDAFilterTypes.RecallReason


            Dim masterRecallResultList As New List(Of ResultRecall)

            Dim found As Boolean
            For Each kwGroup In keyWordList

                Dim filterList As New List(Of String)
                Dim xx As String() = kwGroup.Split(",")
                For Each itm In xx
                    filterList.Add(itm)
                Next


                ''Dim maxResultSize As Integer = 25
                'Dim endPointType As OpenFDAApiEndPoints
                'Dim apiUrl As String = String.Empty
                'Dim searchResults As String
                'Dim srMetaData As MetaResults

                Dim RecallResultList As New List(Of ResultRecall)


                found = ExecuteSearch(OpenFDAApiEndPoints.FoodRecall, filterType, filterList, maxresultsize, RecallResultList)
                found = ExecuteSearch(OpenFDAApiEndPoints.DrugRecall, filterType, filterList, maxresultsize, RecallResultList)

                For Each itm As ResultRecall In RecallResultList
                    itm.KeyWord = kwGroup
                    masterRecallResultList.Add(itm)
                Next

            Next



            'endPointType = OpenFDAApiEndPoints.FoodRecall
            'fda.AddSearchFilter(filterType, filterList)
            'apiUrl = fda.buildUrl(endPointType, maxResultSize)
            'searchResults = fda.Execute(apiUrl)

            'srMetaData = MetaResults.cnvJsonData(searchResults)
            'If srMetaData.total > 0 Then
            '    Dim tmpRecallResultList As List(Of ResultRecall) = ResultRecall.cnvJsonDataToList(searchResults)
            '    If tmpRecallResultList.Count > 0 Then
            '        For Each itm As ResultRecall In tmpRecallResultList
            '            RecallResultList.Add(itm)
            '        Next
            '    End If
            'End If


            'endPointType = OpenFDAApiEndPoints.DrugRecall
            'fda.AddSearchFilter(filterType, filterList)
            'apiUrl = fda.buildUrl(endPointType, maxResultSize)
            'searchResults = fda.Execute(apiUrl)

            'srMetaData = MetaResults.cnvJsonData(searchResults)
            'If srMetaData.total > 0 Then
            '    Dim tmpRecallResultList As List(Of ResultRecall) = ResultRecall.cnvJsonDataToList(searchResults)
            '    If tmpRecallResultList.Count > 0 Then
            '        For Each itm As ResultRecall In tmpRecallResultList
            '            RecallResultList.Add(itm)
            '        Next
            '    End If
            'End If

            'fda.AddSearchFilter(filterType, fl)


            'If RecallResultList.Count = 0 Then
            If masterRecallResultList.Count = 0 Then
                Return Request.CreateErrorResponse(HttpStatusCode.NotFound, "recordsNotFound")
            Else
                'Return Request.CreateResponse(Of List(Of ResultRecall))(HttpStatusCode.OK, RecallResultList)
                Return Request.CreateResponse(Of List(Of ResultRecall))(HttpStatusCode.OK, masterRecallResultList)
            End If

        End Function

        Private Function ExecuteSearch(endPointType As OpenFDAApiEndPoints, filterType As FDAFilterTypes, filterList As List(Of String), ByVal maxresultsize As Integer, ByRef RecallResultList As List(Of ResultRecall)) As Boolean

            Dim fda As New OpenFDA
            Dim apiUrl As String = String.Empty
            Dim searchResults As String
            Dim srMetaData As MetaResults
            Dim tmpRecallResultList As New List(Of ResultRecall)

            fda.AddSearchFilter(endPointType, filterType, filterList)
            apiUrl = fda.buildUrl(endPointType, maxResultSize)
            searchResults = fda.Execute(apiUrl)

            srMetaData = MetaResults.CnvJsonData(searchResults)
            If srMetaData.Total > 0 Then

                tmpRecallResultList = ResultRecall.CnvJsonDataToList(searchResults)
                If tmpRecallResultList.Count > 0 Then

                    For Each itm As ResultRecall In tmpRecallResultList
                        RecallResultList.Add(itm)
                    Next

                End If

            End If

            Return tmpRecallResultList.Count > 0

        End Function

    End Class


End Namespace