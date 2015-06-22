#Region " Public Enumerations "

''' <summary>
''' Reported role of the drug in the adverse event.
''' </summary>
''' <remarks></remarks>
Public Enum EnumDrugCharacterization

    <System.ComponentModel.Description("Suspect drug")>
        SuspectDrug = 1
    <System.ComponentModel.Description("Concomitant drug")>
        ConcomitantDrug = 2
    <System.ComponentModel.Description("Interacting drug")>
        InteractingDrug = 3

End Enum

#End Region
