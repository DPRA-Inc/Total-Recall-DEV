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

                    Dim actionDrug As Integer = CInt(obj.ActionDrug)
                    Dim drugRecurreAdministraton As Integer = CInt(obj.DrugRecurreAdministration)
                    Dim drugIntervalDosageDefinition As Integer = CInt(obj.DrugIntervalDosageDefinition)
                    Dim drugTreatmentDurationUnit As Integer = CInt(obj.DrugTreatmentDurationUnit)
                    Dim drugCumulativeDosageUnit As Integer = CInt(obj.DrugCumulativeDosageUnit)
                    Dim drugCharacterization As Integer = CInt(obj.DrugCharacterization)

                    Integer.TryParse(CStr(drug("actiondrug")), actionDrug)
                    obj.ActionDrug = CType(actionDrug, Enumerations.ActionDrug)

                    Integer.TryParse(CStr(drug("drugrecurreadministration")), drugRecurreAdministraton)
                    obj.DrugRecurreAdministration = CType(drugRecurreAdministraton, Enumerations.DrugRecurreAdministration)

                    obj.DrugIntervalDosageUnitNumb = CStr(drug("drugintervaldosageunitnumb"))

                    Integer.TryParse(CStr(drug("drugintervaldosagedefinition")), drugIntervalDosageDefinition)
                    obj.DrugIntervalDosageDefinition = CType(drugIntervalDosageDefinition, Enumerations.DrugIntervalDosageDefinition)

                    obj.DrugTreatmentDuration = CStr(drug("drugtreatmentduration"))

                    Integer.TryParse(CStr(drug("drugtreatmentdurationunit")), drugTreatmentDurationUnit)
                    obj.DrugTreatmentDurationUnit = CType(drugTreatmentDurationUnit, Enumerations.DrugTreatmentDurationUnit)

                    obj.DrugCumulativeDosageNumb = CStr(drug("drugcumulativedosagenumb"))

                    Integer.TryParse(CStr(drug("drugcumulativedosageunit")), drugCumulativeDosageUnit)
                    obj.DrugCumulativeDosageUnit = CType(drugCumulativeDosageUnit, Enumerations.DrugCumulativeDosageUnit)

                    Integer.TryParse(CStr(drug("drugcharacterization")), drugCharacterization)
                    obj.DrugCharacterization = CType(drugCharacterization, Enumerations.DrugCharacterization)

                    obj.DrugAdditional = CStr(drug("drugadditional"))

                    obj.DrugAuthorizationNumb = CStr(drug("drugauthorizationnumb"))

                    obj.DrugIndication = CStr(drug("drugindication"))
                    obj.MedicinalProduct = CStr(drug("medicinalproduct"))
                    obj.Drugadministrationroute = CStr(drug("drugadministrationroute"))
                    obj.DrugDosageText = CStr(drug("drugdosagetext"))

                    obj.DrugStartDate = CStr(drug("drugstartdate"))
                    obj.DrugStartDateFormat = CStr(drug("drugstartdateformat"))
                    obj.DrugEndDate = CStr(drug("drugenddate"))
                    obj.DrugEndDateFormat = CStr(drug("drugenddateformat"))

                    obj.DrugDosageForm = CStr(drug("drugdosageform"))


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
