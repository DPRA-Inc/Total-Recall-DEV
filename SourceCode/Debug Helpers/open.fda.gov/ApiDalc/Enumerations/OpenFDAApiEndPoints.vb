Imports System.ComponentModel

Namespace Enumerations

    ''' <summary>
    ''' open.fda.gov API Endpoints
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum OpenFDAApiEndPoints

        '<Description("drug/event")>
        '<DisplayNameAttribute("drug/event")>
        <Description("Drug Event")>
        <DefaultValueAttribute("drug/event")>
        DrugEvent

        <Description("Drug Label")>
        <DefaultValueAttribute("drug/label")>
        DrugLabel

        <Description("Drug Recall")>
        <DefaultValueAttribute("drug/enforcement")>
        DrugRecall

        <Description("Device Event")>
        <DefaultValueAttribute("device/event")>
        DeviceEvent

        <Description("Device Recall")>
        <DefaultValueAttribute("device/enforcement")>
        DeviceRecall

        <Description("Food Recall")>
        <DefaultValueAttribute("food/enforcement")>
        FoodRecall

    End Enum

End Namespace