<Obsolete>
Public Class DrugEventList

    Public Property Events As New List(Of DrugEventItem)

    Public Sub New(jsonString As String)

        Dim x = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonString)
        Dim listing = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(x("results").ToString)

        For Each item In listing

            Events.Add(DrugEventItem.Fill(item))

        Next

    End Sub

End Class
