''' <summary>
''' LookupDesc Object
''' </summary>
''' <remarks></remarks>
Public Class LookupDescData
    Inherits LookupData

#Region " Member Variables "

    Private _description As String = String.Empty

#End Region

#Region " Public Properties "

    Public Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property

#End Region

#Region " Overriden ToString "

    Public Overrides Function ToString() As String

        Dim array As List(Of String) = Description.Trim.Split("-").ToList

        'If array.Count = 1 Then
        '    Return Description.Trim
        'ElseIf array.Count = 2 Then
        '    Return array(1).Trim + " - " + array(0).Trim
        'Else
        Return Description.Trim
        'End If

    End Function

#End Region

End Class