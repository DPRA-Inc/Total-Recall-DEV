Imports System.ComponentModel

Namespace Enumerations
    ''' <summary>
    ''' Recall Classification Levels
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Classification

        <Description("Dangerous or defective products that predictably could cause serious health problems or death")>
        <DefaultValue("Class I")>
        Class_I
        <Description("Products that might cause a temporary health problem, or pose only a slight threat of a serious nature")>
        <DefaultValue("Class II")>
        Class_II
        <Description("Products that are unlikely to cause any adverse health reaction, but that violate FDA labeling or manufacturing laws")>
        <DefaultValue("Class III")>
        Class_III

    End Enum

End Namespace