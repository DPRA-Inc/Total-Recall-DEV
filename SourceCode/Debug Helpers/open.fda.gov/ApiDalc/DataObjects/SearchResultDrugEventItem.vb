#Region " Imports "

Imports ApiDalc.Enumerations
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    Public Class SearchResultDrugEventItem

        Public Property BrandName As New List(Of String)
        Public Property GenericName As New List(Of String)
        Public Property ManufacturerName As New List(Of String)
        Public Property Route As New List(Of String)

    End Class

End Namespace
