﻿#Region " Imports "

Imports ApiDalc.Enumerations
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    Public Class SearchResultDrugEventItem

        Public Property BrandName As New List(Of String)
        Public Property GenericName As New List(Of String)
        Public Property ManufacturerName As New List(Of String)
        Public Property Route As New List(Of String)

        Public Property BrandNamesString As String
            Get
                Return Join(BrandName.ToArray, ", ")
            End Get
            Set(value As String)
            End Set
        End Property

        Public Property GenericNamesString As String
            Get
                Return Join(GenericName.ToArray, ", ")
            End Get
            Set(value As String)
            End Set
        End Property

    End Class

End Namespace
