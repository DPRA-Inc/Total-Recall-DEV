Imports System.ComponentModel

Namespace Enumerations

    ''' <summary>
    ''' Reaction Outcomes
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ReactionOutcome

        <Description("Recovered/resolved")>
        RecoveredResolved = 1
        <Description("Recovering/resolving")>
        RecoveringResolving = 2
        <Description("Not recovered/not resolved")>
        NotRecoveredNotResolved = 3
        <Description("Recovered/resolved with sequelae")>
        RecoveredResolvedWithSequelae = 4
        <Description("Fatal")>
        Fatal = 5
        <Description("Unknown")>
        Unknown = 6

    End Enum

End Namespace