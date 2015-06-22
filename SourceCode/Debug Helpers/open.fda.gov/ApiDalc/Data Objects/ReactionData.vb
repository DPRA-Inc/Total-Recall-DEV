#Region " Imports "

Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

''' <summary>
''' Reaction Data
''' </summary>
''' <remarks></remarks>
Public Class ReactionData

#Region " Public Properties "

    Public Property ReactionMedDrapt As String
    Public Property ReactionMeddraversionPt As String
    Public Property ReactionOutcome As EnumReactionOutcome

#End Region

#Region " Public Methods "

    ''' <summary>
    ''' Convert JSON Data
    ''' </summary>
    ''' <param name="jToken">Token</param>
    ''' <returns>List of Reaction Data</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertJsonData(jToken As JToken) As List(Of ReactionData)

        Dim data As New List(Of ReactionData)

        If IsJTokenValid(jToken) Then

            For Each reaction In jToken

                Dim obj As New ReactionData

                obj.ReactionMedDrapt = reaction("reactionmeddrapt")
                obj.ReactionMeddraversionPt = reaction("reactionmeddraversionpt")
                Integer.TryParse(reaction("reactionoutcome"), obj.ReactionOutcome)

                data.Add(obj)

            Next

        End If

        Return data

    End Function

#End Region

End Class
