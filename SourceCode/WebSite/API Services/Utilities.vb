Module Utilities

    Public Function AddBS(ByVal str As String) As String

        If str.Length = 0 Then
            'DO Nothing
        ElseIf Not str.Substring(str.Length - 1, 1) = "\" Then
            str += "\"
        End If

        Return str

    End Function

    Public Function AddFS(ByVal str As String) As String

        If str.Length = 0 Then
            'DO Nothing
        ElseIf Not str.Substring(str.Length - 1, 1) = "/" Then
            str += "/"
        End If

        Return str

    End Function


    'DefaultProperty <DefaultProperty("MyProperty")> _ Public Class MyControl
    '<DefaultValue(False)> _Public Property MyProperty() As Boolean '  https://msdn.microsoft.com/en-us/library/system.componentmodel.defaultvalueattribute%28v=vs.110%29.aspx
    ' <Description("Demonstrates DisplayNameAttribute."), _ DisplayName("RenamedProperty")> _ Public ReadOnly Property MisnamedProperty() As Boolean  ' https://msdn.microsoft.com/en-us/library/system.componentmodel.displaynameattribute%28v=vs.110%29.aspx

    Public Function GetEnumDescription(ByVal EnumConstant As [Enum]) As String

        Dim fi As System.Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        Dim aattr() As System.ComponentModel.DescriptionAttribute = DirectCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DescriptionAttribute), False), System.ComponentModel.DescriptionAttribute())

        If aattr.Length > 0 Then
            Return aattr(0).Description
        Else
            Return EnumConstant.ToString()
        End If

    End Function

    Public Function GetEnumDisplayName(ByVal EnumConstant As [Enum]) As String

        Dim fi As System.Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        Dim aattr() As System.ComponentModel.DisplayNameAttribute = DirectCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DisplayNameAttribute), False), System.ComponentModel.DisplayNameAttribute())

        If aattr.Length > 0 Then
            Return aattr(0).DisplayName
        Else
            Return EnumConstant.ToString()
        End If

    End Function

    Public Function GetEnumDefault(ByVal EnumConstant As [Enum]) As String

        Dim fi As System.Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        Dim aattr() As System.ComponentModel.DefaultValueAttribute = DirectCast(fi.GetCustomAttributes(GetType(System.ComponentModel.DefaultValueAttribute), False), System.ComponentModel.DefaultValueAttribute())

        If aattr.Length > 0 Then
            Return aattr(0).Value.ToString
        Else
            Return EnumConstant.ToString()
        End If

    End Function

End Module
