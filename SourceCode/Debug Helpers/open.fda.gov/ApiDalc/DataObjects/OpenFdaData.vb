#Region " Imports "

Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    ''' <summary>
    ''' Open FDA Data Object
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OpenFdaData

#Region " Properties "

        Public Property Application_Number As New List(Of String)

        Public Property Brand_Name As New List(Of String)

        Public Property Generic_Name As New List(Of String)

        Public Property Manufacturer_Name As New List(Of String)

        Public Property Nui As List(Of String)

        Public Property Package_Ndc As List(Of String)

        Public Property Pharm_Class_Cs As List(Of String)

        Public Property Pharm_Class_Epc As List(Of String)

        Public Property Product_Ndc As List(Of String)

        Public Property Product_Type As List(Of String)

        Public Property Route As New List(Of String)

        Public Property Rxcui As List(Of String)

        Public Property Spl_id As List(Of String)

        Public Property Spl_set_id As List(Of String)

        Public Property Substance_name As List(Of String)

        Public Property Unii As List(Of String)

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Convert JSON Data
        ''' </summary>
        ''' <param name="jToken">Token</param>
        ''' <returns>OpenFdaData Object</returns>
        ''' <remarks></remarks>
        Public Shared Function ConvertJsonData(jToken As JToken) As OpenFdaData

            Dim data As New OpenFdaData

            If IsJTokenValid(jToken) Then

                'For Each itm In jToken("application_number")
                'Next
                For Each itm In jToken("brand_name")
                    data.Brand_Name.Add(itm)
                Next

                For Each itm In jToken("generic_name")
                    data.Generic_Name.Add(itm)
                Next

                For Each itm In jToken("manufacturer_name")
                    data.Manufacturer_Name.Add(itm)
                Next

                For Each itm In jToken("route")
                    data.Route.Add(itm)
                Next

                'For Each reaction In jToken

                '    Dim obj As New ReactionData

                '    obj.ReactionMedDrapt = reaction("reactionmeddrapt")
                '    obj.ReactionMeddraversionPt = reaction("reactionmeddraversionpt")
                '    Integer.TryParse(reaction("reactionoutcome"), obj.ReactionOutcome)

                '    data.Add(obj)

                'Next

            End If

            Return data

        End Function

#End Region

    End Class

End Namespace
