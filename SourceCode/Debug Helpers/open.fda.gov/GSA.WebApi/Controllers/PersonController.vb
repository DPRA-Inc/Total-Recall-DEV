

#Region " Imports "

Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

#End Region


Namespace Controllers
    Public Class PersonController
        Inherits ApiController

        ' GET: api/Person
        <HttpGet>
        Public Function GetValues() As HttpResponseMessage

            Dim msg As HttpResponseMessage = Nothing

            Dim personList As List(Of DataObjects.Person) = (New DataAccess.Dalc).GetAllPerson()

            If personList Is Nothing OrElse personList.Count = 0 Then
                Return Request.CreateErrorResponse(HttpStatusCode.NotFound, "recordsNotFound")
            Else
                'Return Request.CreateResponse(Of DataObjects.ItemList)(HttpStatusCode.OK, result)

                'Dim result As New List(Of Item_sc)
                'For Each itm In ItemList
                '    result.Add(Item_sc.FromDataObject(itm))
                'Next
                Return Request.CreateResponse(Of List(Of DataObjects.Person))(HttpStatusCode.OK, personList)

            End If

        End Function

        ' GET: api/Person/5
        Public Function GetValue(ByVal id As Integer) As String
            Return "value"
        End Function


        <Route("api/Person/fname/{value}")>
        <HttpGet>
        Public Function GetValues(ByVal value As String) As HttpResponseMessage


            Dim msg As HttpResponseMessage = Nothing

            Dim personList As List(Of DataObjects.Person) = (New DataAccess.Dalc).GetPersonByFName(value)

            If personList Is Nothing OrElse personList.Count = 0 Then
                Return Request.CreateErrorResponse(HttpStatusCode.NotFound, "recordsNotFound")
            Else
                'Return Request.CreateResponse(Of DataObjects.ItemList)(HttpStatusCode.OK, result)

                'Dim result As New List(Of Item_sc)
                'For Each itm In ItemList
                '    result.Add(Item_sc.FromDataObject(itm))
                'Next
                Return Request.CreateResponse(Of List(Of DataObjects.Person))(HttpStatusCode.OK, personList)

            End If

        End Function

        'Public Function GetValue(<FromBody()> ByVal value As DataObjects.Person) As HttpResponseMessage

        '    Debug.WriteLine(value)

        '    Dim msg As HttpResponseMessage = Nothing

        '    Dim personList As List(Of DataObjects.Person) = (New DataAccess.Dalc).GetAllPerson()

        '    If personList Is Nothing OrElse personList.Count = 0 Then
        '        Return Request.CreateErrorResponse(HttpStatusCode.NotFound, "recordsNotFound")
        '    Else
        '        'Return Request.CreateResponse(Of DataObjects.ItemList)(HttpStatusCode.OK, result)

        '        'Dim result As New List(Of Item_sc)
        '        'For Each itm In ItemList
        '        '    result.Add(Item_sc.FromDataObject(itm))
        '        'Next
        '        Return Request.CreateResponse(Of List(Of DataObjects.Person))(HttpStatusCode.OK, personList)

        '    End If

        'End Function

        ' POST: api/Person
        Public Sub PostValue(<FromBody()> ByVal value As String)

        End Sub

        ' PUT: api/Person/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/Person/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub

    End Class


    'Public Class Person
    '    Public Property FirstName As String

    'End Class

End Namespace