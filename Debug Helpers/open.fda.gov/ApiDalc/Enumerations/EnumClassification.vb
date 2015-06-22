#Region " Public Enumerations "

''' <summary>
''' Recall Classification Levels
''' </summary>
''' <remarks></remarks>
Public Enum EnumClassification

    <System.ComponentModel.Description("Dangerous or defective products that predictably could cause serious health problems or death")>
    <System.ComponentModel.DefaultValue("Class I")>
    Class_I
    <System.ComponentModel.Description("Products that might cause a temporary health problem, or pose only a slight threat of a serious nature")>
    <System.ComponentModel.DefaultValue("Class II")>
    Class_II
    <System.ComponentModel.Description("Products that are unlikely to cause any adverse health reaction, but that violate FDA labeling or manufacturing laws")>
    <System.ComponentModel.DefaultValue("Class III")>
    Class_III

End Enum

#End Region
