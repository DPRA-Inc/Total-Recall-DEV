Namespace DataObjects

    Public Class FDAResult

        Public Property Keyword As String

        Public Property Results() As New List(Of SearchResultItemBase)

        Public Property MapObjects As List(Of SearchResultMapData)

        Public Property GraphObjects As ReportData

    End Class

End Namespace

