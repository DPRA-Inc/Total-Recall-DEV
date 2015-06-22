﻿#Region " Imports "

Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

''' <summary>
''' Meta Results Data Object
''' </summary>
''' <remarks></remarks>
Public Class MetaResults

#Region " Public Properties "

    Public Property Limit As Integer = 0
    Public Property Skip As Integer = 0
    Public Property Total As Integer = 0

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Converts JSON Data
    ''' </summary>
    ''' <param name="jsondata">JSON Data as String</param>
    ''' <returns>Meta Results Data Object</returns>
    ''' <remarks></remarks>
    Public Shared Function CnvJsonData(jsondata As String) As MetaResults

        Dim result As MetaResults = Nothing

        If String.IsNullOrEmpty(jsondata) Then
            result = New MetaResults
        Else

            Dim jo As JObject = JObject.Parse(jsondata)
            result = CnvJsonData(jo)

        End If

        Return result

    End Function

    ''' <summary>
    ''' Converts JSON Data
    ''' </summary>
    ''' <param name="jsondata">JSON Data as Object</param>
    ''' <returns>Meta Results Data Object</returns>
    ''' <remarks></remarks>
    Public Shared Function CnvJsonData(jsondata As JObject) As MetaResults

        Dim metaData As New MetaResults
        Dim obj = jsondata.GetValue("meta")

        If obj("results") IsNot Nothing Then

            With metaData
                .Limit = obj("results")("limit")
                .Skip = obj("results")("skip")
                .Total = obj("results")("total")
            End With

        End If

        Return metaData

    End Function

#End Region

End Class

