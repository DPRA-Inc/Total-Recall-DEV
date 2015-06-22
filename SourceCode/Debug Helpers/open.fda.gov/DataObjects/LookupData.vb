''' <summary>
''' LookupData Object
''' </summary>
''' <remarks></remarks>
Public Class LookupData

#Region " Member Variables "

    Private _key As Integer = -1
#End Region

#Region " Public Properties "

    Public Property Key As Integer
        Get
            Return _key
        End Get
        Set(value As Integer)
            _key = value
        End Set
    End Property

#End Region

#Region " Public Functions "

    Public Function ToValue() As String
        Return Key.ToString
    End Function
#End Region

End Class
