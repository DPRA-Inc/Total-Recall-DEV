Public MustInherit Class WebBusinessInfo

    ''' <summary>
    ''' Used when you want the business to Redirect The Web To a different page.
    ''' </summary>
    Public Property WebRedirectTo() As String

    ''' <summary>
    ''' Used internally for JSON to Object Serialization.
    ''' </summary>
    Public Property WebObjectType() As String
        Get
            Return Me.[GetType]().ToString()
        End Get
        Set(value As String)
        End Set
    End Property

End Class
