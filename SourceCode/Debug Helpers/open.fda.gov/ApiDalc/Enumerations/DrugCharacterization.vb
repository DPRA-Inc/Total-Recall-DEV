Imports System.ComponentModel

Namespace Enumerations

    ''' <summary>
    ''' Reported role of the drug in the adverse event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DrugCharacterization

        <Description("Suspect drug")>
        SuspectDrug = 1

        <Description("Concomitant drug")>
        ConcomitantDrug = 2

        <Description("Interacting drug")>
        InteractingDrug = 3

    End Enum

End Namespace