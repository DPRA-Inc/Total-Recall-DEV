Module Utilities

#Region " Public Methods "

    ''' <summary>
    ''' Fixes String values
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddBackSlash(ByVal path As String) As String

        If Not String.IsNullOrEmpty(path) AndAlso Not path.Substring(path.Length - 1, 1) = "\" Then
            path += "\"
        End If

        Return path

    End Function

    ''' <summary>
    ''' Fixes String values
    ''' </summary>
    ''' <param name="url"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddForwardSlash(ByVal url As String) As String
        
        If Not String.IsNullOrEmpty(url) AndAlso Not url.Substring(url.Length - 1, 1) = "\" Then
            url += "/"
        End If

        Return url

    End Function

#End Region

#Region " JSON Helper methods "

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


#End Region

#Region " Enumeration Helper methods "

    ''' <summary>
    ''' Enumeration: Returns Description
    ''' </summary>
    ''' <param name="EnumConstant"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEnumDescription(ByVal EnumConstant As [Enum]) As String

        Dim result As String = String.Empty

        Dim fi As System.Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())

        If fi IsNot Nothing Then

            Dim aattr() As System.ComponentModel.DescriptionAttribute = DirectCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DescriptionAttribute), False), System.ComponentModel.DescriptionAttribute())

            If aattr.Length > 0 Then
                result = aattr(0).Description
            Else
                result = (EnumConstant.ToString())
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
    Public Function GetEnumDisplayName(ByVal EnumConstant As [Enum]) As String

        Dim fi As System.Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        Dim aattr() As System.ComponentModel.DisplayNameAttribute = DirectCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DisplayNameAttribute), False), System.ComponentModel.DisplayNameAttribute())

        If aattr.Length > 0 Then
            Return aattr(0).DisplayName
        Else
            Return EnumConstant.ToString()
        End If

    End Function

    ''' <summary>
    ''' Enumeration: Returns Default Value
    ''' </summary>
    ''' <param name="EnumConstant"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEnumDefault(ByVal EnumConstant As [Enum]) As String

        Dim fi As System.Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        Dim aattr() As System.ComponentModel.DefaultValueAttribute = DirectCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DefaultValueAttribute), False), System.ComponentModel.DefaultValueAttribute())

        If aattr.Length > 0 Then
            Return aattr(0).Value.ToString
        Else
            Return EnumConstant.ToString()
        End If

    End Function

#End Region

End Module
