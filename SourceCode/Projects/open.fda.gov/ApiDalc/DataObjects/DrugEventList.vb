<Obsolete>
Public Class DrugEventList

    Public Property Events As New List(Of DrugEventItem)

    Public Sub New(jsonString As String)

        Dim x As Dictionary(Of String, Object) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonString)
        Dim listing As List(Of Dictionary(Of String, Object)) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(x("results").ToString)

        For Each item In listing

            Events.Add(DrugEventItem.Fill(item))

        Next

    End Sub

End Class
