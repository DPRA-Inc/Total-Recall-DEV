#Region " Public Enumerations "

''' <summary>
''' open.fda.gov API Endpoints
''' </summary>
''' <remarks></remarks>
Public Enum OpenFDAApiEndPoints

    '<System.ComponentModel.Description("drug/event")>
    '<System.ComponentModel.DisplayNameAttribute("drug/event")>
    <System.ComponentModel.Description("Drug Event")>
    <System.ComponentModel.DefaultValueAttribute("drug/event")>
    DrugEvent

    <System.ComponentModel.Description("Drug Label")>
    <System.ComponentModel.DefaultValueAttribute("drug/label")>
    DrugLabel

    <System.ComponentModel.Description("Drug Recall")>
    <System.ComponentModel.DefaultValueAttribute("drug/enforcement")>
    DrugRecall

    <System.ComponentModel.Description("Device Event")>
    <System.ComponentModel.DefaultValueAttribute("device/event")>
    DeviceEvent

    <System.ComponentModel.Description("Device Recall")>
    <System.ComponentModel.DefaultValueAttribute("device/enforcement")>
    DeviceRecall

    <System.ComponentModel.Description("Food Recall")>
    <System.ComponentModel.DefaultValueAttribute("food/enforcement")>
    FoodRecall

End Enum

#End Region
