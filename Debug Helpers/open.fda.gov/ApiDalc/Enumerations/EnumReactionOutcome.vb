#Region " Public Enumerations "

''' <summary>
''' Reaction Outcomes
''' </summary>
''' <remarks></remarks>
Public Enum EnumReactionOutcome

    <System.ComponentModel.Description("Recovered/resolved")>
    RecoveredResolved = 1
    <System.ComponentModel.Description("Recovering/resolving")>
    RecoveringResolving = 2
    <System.ComponentModel.Description("Not recovered/not resolved")>
    NotRecoveredNotResolved = 3
    <System.ComponentModel.Description("Recovered/resolved with sequelae")>
    RecoveredResolvedWithSequelae = 4
    <System.ComponentModel.Description("Fatal")>
    Fatal = 5
    <System.ComponentModel.Description("Unknown")>
    Unknown = 6

End Enum

#End Region
