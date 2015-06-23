
Public Class SearchResultItem

    Public Property DateStarted As Date 'recall_initiation_date

    Public Property Content As String ' reason_for_recall  + code_info

    Public Property Status As String ' status

    Public Property DistributionPattern As String  '"distribution_pattern
    Public Property DistributionList As New List(Of String)  '"distribution_pattern

    Public Property State As String

    Public Property City As String

    Public Property ProductDescription As String 'product_description

    Public Property Voluntary As String 'voluntary_mandated


End Class