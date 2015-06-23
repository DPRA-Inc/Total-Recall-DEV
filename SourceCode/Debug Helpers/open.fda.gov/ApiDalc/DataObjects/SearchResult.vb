Namespace DataObjects

    Public Class SearchResult

        Public Property Keyword As String

        Public Property ClassI As New List(Of SearchResultItem)

        Public Property ClassII As New List(Of SearchResultItem)

        Public Property ClassIII As New List(Of SearchResultItem)

        Public Property Events As New List(Of SearchResultItem)

        Public Property MapObjects As List(Of SearchResultItem)

    End Class

End Namespace
