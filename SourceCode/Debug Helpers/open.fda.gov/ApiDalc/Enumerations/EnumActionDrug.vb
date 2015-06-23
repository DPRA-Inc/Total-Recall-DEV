Imports System.ComponentModel

Namespace Enumerations
    ''' <summary>
    ''' Actions taken with the drug
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EnumActionDrug

        <Description("Drug Withdrawn")>
        DrugWithdrawn = 1

        <Description("Dose Reduced")>
        DoseReduced = 2

        <Description("Dose Increased")>
        DoseIncreased = 3

        <Description("Dose not changed")>
        DoseNotChanged = 4

        <Description("Unknown")>
        Unknown = 5

        <Description("Not Applicable")>
        NotApplicable = 6

    End Enum

End Namespace