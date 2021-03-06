﻿Imports ApiDalc.Enumerations

Public Module Utilities

#Region " Public Methods "

    ''' <summary>
    ''' Converts a string date to a Date Object
    ''' </summary>
    ''' <param name="dateString">21050102</param>
    ''' <param name="dateFormat">yyyyMMdd</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConvertDateStringToDate(ByVal dateString As String, ByVal dateFormat As String) As Date

        If dateString Is Nothing OrElse dateString.Length = 0 Then
            Return Nothing
        End If

        Dim result As DateTime

        Select Case dateFormat
            Case "yyyyMMdd"
                result = DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)

            Case Else
                result = Nothing

        End Select

        Return result

    End Function


    ''' <summary>
    ''' Fixes String values
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddBackslash(ByVal str As String) As String

        If str IsNot Nothing AndAlso Not str.EndsWith("\") Then
            str += "\"
        End If

        Return str

    End Function

    ''' <summary>
    ''' Fixes String values
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddForwardSlash(ByVal str As String) As String

        If str IsNot Nothing AndAlso Not str.EndsWith("/") Then
            str += "/"
        End If

        Return str

    End Function

    ''' <summary>
    ''' Checks for token validity
    ''' </summary>
    ''' <param name="jToken"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsJTokenValid(ByVal jToken As Newtonsoft.Json.Linq.JToken) As Boolean

        Dim result As Boolean = False

        If jToken IsNot Nothing Then

            If Not String.IsNullOrEmpty(jToken.ToString) Then
                result = True
            End If

        End If

        Return result

    End Function

    ''' <summary>
    ''' Enumeration: Returns Description
    ''' </summary>
    ''' <param name="EnumConstant"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEnumDescription(ByVal enumConstant As [Enum]) As String

        Dim result As String = String.Empty

        If enumConstant IsNot Nothing Then

            Dim fi As System.Reflection.FieldInfo = enumConstant.GetType().GetField(enumConstant.ToString())

            If fi IsNot Nothing Then

                Dim aattr() As System.ComponentModel.DescriptionAttribute = DirectCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DescriptionAttribute), False), System.ComponentModel.DescriptionAttribute())

                If aattr.Length > 0 Then
                    result = aattr(0).Description
                Else
                    result = enumConstant.ToString()
                End If

            End If

        End If

        Return result

    End Function

    ''' <summary>
    ''' Enumeration: Returns Display Name
    ''' </summary>
    ''' <param name="EnumConstant"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEnumDisplayName(ByVal enumConstant As [Enum]) As String

        Dim displayName As String = String.Empty

        If Not enumConstant Is Nothing Then

            Dim fi As System.Reflection.FieldInfo = enumConstant.GetType().GetField(enumConstant.ToString())
            Dim aattr() As System.ComponentModel.DisplayNameAttribute = DirectCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DisplayNameAttribute), False), System.ComponentModel.DisplayNameAttribute())

            If aattr.Length > 0 Then
                displayName = aattr(0).DisplayName
            Else
                displayName = enumConstant.ToString()
            End If

        End If

        Return displayName

    End Function

    ''' <summary>
    ''' Enumeration: Returns Default Value
    ''' </summary>
    ''' <param name="EnumConstant"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEnumDefaultValue(ByVal enumConstant As [Enum]) As String

        Dim defaultValue As String = String.Empty

        If enumConstant IsNot Nothing Then

            Dim fi As System.Reflection.FieldInfo = enumConstant.GetType().GetField(enumConstant.ToString())
            Dim aattr() As System.ComponentModel.DefaultValueAttribute = DirectCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DefaultValueAttribute), False), System.ComponentModel.DefaultValueAttribute())

            If aattr.Length > 0 Then
                defaultValue = aattr(0).Value.ToString
            Else
                defaultValue = enumConstant.ToString()
            End If

        End If

        Return defaultValue

    End Function

#End Region

End Module
