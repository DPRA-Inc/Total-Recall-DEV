Namespace DataObjects

    ''' <summary>
    ''' Recall Search Results
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RecallSearchResultData

#Region " Public Methods "

        Public Property KeyWord As String
        Public Property Count As Integer
        Public Property Type As String
        Public Property Classification As String
        Public Property ProductDescription As String
        Public Property ReasonForRecall As String

        Public Property Regions As New HashSet(Of String)
        Public Property IsNationWide As Boolean = False

#End Region

    End Class

End Namespace