#Region " Public Enumerations "

''' <summary>
''' Actions taken with the drug
''' </summary>
''' <remarks></remarks>
Public Enum EnumActionDrug

    <System.ComponentModel.Description("Drug Withdrawn")>
        DrugWithdrawn = 1

    <System.ComponentModel.Description("Dose Reduced")>
        DoseReduced = 2

    <System.ComponentModel.Description("Dose Increased")>
        DoseIncreased = 3

    <System.ComponentModel.Description("Dose not changed")>
        DoseNotChanged = 4

    <System.ComponentModel.Description("Unknown")>
        Unknown = 5

    <System.ComponentModel.Description("Not Applicable")>
        NotApplicable = 6

End Enum

#End Region
