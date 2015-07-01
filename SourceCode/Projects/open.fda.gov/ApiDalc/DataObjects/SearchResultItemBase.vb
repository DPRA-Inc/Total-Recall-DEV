#Region " Imports "

Imports ApiDalc.DataObjects
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    Public Class SearchResultItemBase


#Region " Private Members "

        Private _reportDate As String = String.Empty

#End Region

#Region " Public Property "

        Public Property Classification As String = String.Empty

        Public Property DateStarted As String

        ''' <summary>
        ''' Date that most recent information in the report was received by FDA.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ReportDate As String
            Get
                If _reportDate.Length = 0 Then
                    Return Me.DateStarted
                Else
                    Return _reportDate
                End If
            End Get
            Set(value As String)
                _reportDate = value
            End Set
        End Property

        Public ReadOnly Property SortDate As DateTime
            Get
                Return DateTime.ParseExact(Me.ReportDate, "ddMMMyyyy", System.Globalization.CultureInfo.InvariantCulture)
            End Get
        End Property

#End Region

    End Class

End Namespace
