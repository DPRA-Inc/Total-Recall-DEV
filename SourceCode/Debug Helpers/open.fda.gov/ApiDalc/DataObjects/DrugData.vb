#Region " Imports "

Imports ApiDalc.Enumerations
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    ''' <summary>
    ''' Drug Data Object
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DrugData

#Region " Public Properties "

        Public Property ActionDrug As ActionDrug

        Public Property DrugCumulativeDosageNumb As String

        Public Property DrugCumulativeDosageUnit As DrugCumulativeDosageUnit

        Public Property DrugTreatmentDuration As String

        Public Property DrugTreatmentDurationUnit As DrugTreatmentDurationUnit

        Public Property DrugIntervalDosageUnitNumb As String

        Public Property DrugIntervalDosageDefinition As DrugIntervalDosageDefinition

        Public Property DrugRecurreAdministration As DrugRecurreAdministration

        Public Property DrugAdditional As String

        Public Property DrugAuthorizationNumb As String

        Public Property DrugDosageForm As String

        Public Property DrugCharacterization As DrugCharacterization

        Public Property Drugadministrationroute As String

        Public Property DrugDosageText As String

        Public Property DrugStartDate As String

        Public Property DrugEndDate As String

        Public Property DrugStartDateFormat As String

        Public Property DrugEndDateFormat As String

        'Public Property drugcharacterization As String

        'Public Property medicinalproduct As String

        Public Property DrugIndication As String

        Public Property MedicinalProduct As String

        Public Property OpenFDA As OpenFdaData

#End Region

        ''' <summary>
        ''' Convert JSON Data
        ''' </summary>
        ''' <param name="jToken">Token</param>
        ''' <returns>List of Drug Data</returns>
        ''' <remarks></remarks>
        Public Shared Function ConvertJsonData(jToken As JToken) As List(Of DrugData)

            Dim data As New List(Of DrugData)

            If IsJTokenValid(jToken) Then

                For Each drug In jToken

                    Dim obj As New DrugData

                    Integer.TryParse(drug("actiondrug"), obj.ActionDrug)
                    Integer.TryParse(drug("drugrecurreadministration"), obj.DrugRecurreAdministration)

                    obj.DrugIntervalDosageUnitNumb = drug("drugintervaldosageunitnumb")
                    Integer.TryParse(drug("drugintervaldosagedefinition"), obj.DrugIntervalDosageDefinition)

                    obj.DrugTreatmentDuration = drug("drugtreatmentduration")
                    Integer.TryParse(drug("drugtreatmentdurationunit"), obj.DrugTreatmentDurationUnit)

                    obj.DrugCumulativeDosageNumb = drug("drugcumulativedosagenumb")
                    Integer.TryParse(drug("drugcumulativedosageunit"), obj.DrugCumulativeDosageUnit)


                    Integer.TryParse(drug("drugcharacterization"), obj.DrugCharacterization)

                    obj.DrugAdditional = drug("drugadditional")

                    obj.DrugAuthorizationNumb = drug("drugauthorizationnumb")

                    obj.DrugIndication = drug("drugindication")
                    obj.MedicinalProduct = drug("medicinalproduct")
                    obj.Drugadministrationroute = drug("drugadministrationroute")
                    obj.DrugDosageText = drug("drugdosagetext")

                    obj.DrugStartDate = drug("drugstartdate")
                    obj.DrugStartDateFormat = drug("drugstartdateformat")
                    obj.DrugEndDate = drug("drugenddate")
                    obj.DrugEndDateFormat = drug("drugenddateformat")

                    obj.DrugDosageForm = drug("drugdosageform")


                    'obj.drugcharacterization = drug("drugcharacterization")
                    'obj.medicinalproduct = drug("medicinalproduct")

                    obj.OpenFDA = OpenFdaData.ConvertJsonData(drug("openfda"))

                    data.Add(obj)

                Next

            End If
            Return data

        End Function

    End Class

End Namespace
