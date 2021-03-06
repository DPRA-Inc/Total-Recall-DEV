﻿#Region " Imports "

Imports ApiDalc.DataObjects
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    Public Class SearchResultItem
        Inherits SearchResultItemBase

#Region " Public Properties "

        'Public Property Classification As String

        Public Property Rank As String
            Get
                Return Replace(Classification, " ", "").ToLower
            End Get
            Set(value As String)
            End Set
        End Property

        Public Property IsEvent As Boolean = False
        Public Property IsProduct As Boolean = True

        'Public Property DateStarted As String 'recall_initiation_date

        Public Property Content As String ' reason_for_recall  + code_info
        Public ReadOnly Property ContentTruncated As String
            Get

                Dim value As String = Me.Content

                If Content.Length > 1000 Then
                    value = String.Concat(Me.Content.Substring(0, 999), "...")
                End If

                Return value

            End Get
        End Property
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
        'Public Property ReportDate As String ' report_date
        'Public ReadOnly Property SortDate As DateTime
        '    Get
        '        Return DateTime.ParseExact(Me.ReportDate, "ddMMMyyyy", System.Globalization.CultureInfo.InvariantCulture)
        '    End Get
        'End Property
        Public Property CodeInfo As String ' code_info

#End Region

    End Class

End Namespace