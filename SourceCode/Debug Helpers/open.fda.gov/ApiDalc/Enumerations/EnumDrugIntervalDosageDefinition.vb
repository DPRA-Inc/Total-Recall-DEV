Imports System.ComponentModel

Namespace Enumerations
    ''' <summary>
    ''' Drug Interval Dosage Definition
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EnumDrugIntervalDosageDefinition

        Year = 801
        Month = 802
        Week = 803
        Day = 804
        Hour = 805
        Minute = 806
        <Description("Trimester")>
        Trimester_807 = 807
        Cyclical = 810
        <Description("Trimester")>
        Trimester_811 = 811
        <Description("As Necessary")>
        AsNecessary = 812
        Total = 813

    End Enum

End Namespace