#Region " Imports "

Imports ApiDalc.Enumerations
Imports Newtonsoft.Json.Linq

#End Region

Namespace DataObjects

    ''' <summary>
    ''' Patient Data Object
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PatientData

#Region " Public Properties "

        Public Property PatientSex As PatientSex

        'The age of the patient when the event first occured
        Public Property PatientOnSetAge As String

        'The unit of measurement for the patient.patientonsetage field
        Public Property PatientOnSetAgeUnit As String

        'The weight of the patient expressed in kilograms.
        Public Property PatientWeight As String

        'If the patient died, this section contains information about the death.
        'Public Property patientdeath As List(Of String)
        'patient.patientdeath
        'patient.patientdeath.patientdeathdate
        'patient.patientdeath.patientdeathdateformat

        Public Property Drug As List(Of DrugData)
        Public Property Reaction As List(Of ReactionData)

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Convert JSON Date
        ''' </summary>
        ''' <param name="jToken">Token</param>
        ''' <returns>Patient Data Object</returns>
        ''' <remarks></remarks>
        Public Shared Function ConvertJsonDate(jToken As JToken) As PatientData

            Dim data As New PatientData

            If IsJTokenValid(jToken) Then

                data.PatientOnSetAge = jToken("patientonsetage")
                data.PatientOnSetAgeUnit = jToken("patientonsetageunit")
                '        patient.patientonsetageunit()
                'string
                'The unit of measurement for the patient.patientonsetage field.
                '800 = Decade
                '801 = Year
                '802 = Month
                '803 = Week
                '804 = Day
                '805 = Hour


                Integer.TryParse(jToken("patientsex"), data.PatientSex)

                'data.patientsex = jToken("patientsex")
                '        patient.patientsex()
                'string
                'The sex of the patient.
                '0 = Unknown
                '1 = Male
                '2 = Female

                data.PatientWeight = jToken("patientweight") ' KiloGrams
                'data.patientdeath = jToken("patientdeath")
                '        patient.patientdeath()
                '        List()
                'If the patient died, this section contains information about the death.
                '            patient.patientdeath.patientdeathdate()
                'string
                'Date that the patient died.
                '            patient.patientdeath.patientdeathdateformat()
                'string
                'Identifies the encoding format of the tient.patientdeath.patientdeathdate field. Always set to 102 (YYYYMMDD).

                data.Drug = DrugData.ConvertJsonData(jToken("drug"))
                data.Reaction = ReactionData.ConvertJsonData(jToken("reaction"))

            End If

            Return data

        End Function

#End Region

    End Class
End Namespace