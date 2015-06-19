Public Class SiteExceptionInfo
    Inherits WebBusinessInfo

    Public Property Type() As String
    Public Property ErrorType() As ErrorTypes
    Public Property ErrorMessage() As String
    Public Property ErrorDetails() As String
    Public Property ValidationErrors() As List(Of ValidationError)
    Public Property StatusCode() As Integer

    Public Sub New()

        Me.ErrorType = ErrorTypes.General
        Me.Type = "SiteException"

    End Sub

    Public Shared Function Fill(errorMessage As String, errorType As ErrorTypes) As SiteExceptionInfo
        Dim x As New SiteExceptionInfo()

        x.ErrorMessage = errorMessage
        x.ErrorType = errorType

        Return x

    End Function

End Class

Public Class ValidationError
    Inherits WebBusinessInfo

    Public Property PropertyName() As String
    Public Property [Error]() As String

End Class

Public Enum ErrorTypes As Byte
    [Error] = 0
    General = 1
    Fatal = 2
    Validation = 3
    Warning = 4
End Enum

