Imports ApiDalc.Enumerations

Namespace DataObjects

    Public Class SearchSummary

        Public Property Keyword As String

        Public Property EventCount As Integer

        Public Property ClassICount As Integer
        Public Property ClassIDescription As String = GetEnumDescription(Classification.Class_I)

        Public Property ClassIICount As Integer
        Public Property ClassIIDescription As String = GetEnumDescription(Classification.Class_II)

        Public Property ClassIIICount As Integer
        Public Property ClassIIIDescription As String = GetEnumDescription(Classification.Class_III)

    End Class

End Namespace
