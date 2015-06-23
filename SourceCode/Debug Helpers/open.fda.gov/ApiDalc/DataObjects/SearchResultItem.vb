#Region " Imports "

Imports ApiDalc.DataObjects
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    Public Class SearchResultItem

        Public Property DateStarted As String 'recall_initiation_date

        Public Property Content As String ' reason_for_recall  + code_info

        Public Property Status As String ' status

        Public Property DistributionPattern As String  '"distribution_pattern
        Public Property DistributionList As New List(Of String)  '"distribution_pattern

        Public Property State As String

        Public Property City As String

        Public Property ProductDescription As String 'product_description

        Public Property Voluntary As String 'voluntary_mandated

        Public Property RecallingFirm As String 'recalling_firm
        Public Property RecallNumber As String 'recall_number
        Public Property EventId As String 'event_id
        Public Property ProductQuantity As String ' product_quantity
        Public Property Country As String ' country
        Public Property ReportDate As String ' report_date
        Public Property CodeInfo As String ' code_info


    End Class

End Namespace