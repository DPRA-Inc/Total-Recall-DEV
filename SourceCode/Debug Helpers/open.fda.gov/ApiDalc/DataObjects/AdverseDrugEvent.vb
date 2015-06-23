#Region " Imports "

Imports ApiDalc.DataObjects
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    ''' <summary>
    ''' Adverse Drug Event Data Object
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AdverseDrugEvent

#Region " Properties "

        Public Property CompanyNumb As String
        Public Property Duplicate As String
        Public Property FulfillExpediteCriteria As String
        Public Property ReceiveDateFormat As String
        Public Property Receiver As Object
        Public Property ReportDuplicate As Object
        Public Property SeriousnessLifeThreatening As String
        Public Property ReportType As String
        Public Property ReceiptDateFormat As String
        Public Property ReceiveDate As String
        Public Property Sender As Object
        Public Property Patient As PatientData
        Public Property ReceiptDate As String
        Public Property SafetyReportVersion As String
        Public Property SafetyReportId As String
        Public Property PrimarySource As Object
        Public Property PrimarySourceCountry As String
        Public Property TransmissionDate As String
        Public Property TransmissionDateFormat As String
        Public Property OccurCountry As String


        Public Property Serious As String
        '    serious
        'string
        '1 = The adverse event resulted in death, a life threatening condition, hospitalization, disability, congenital anomali, or other serious condition.
        '2 = The adverse event did not result in any of the above.

        Public Property SeriousnessDeath As String ' boolean 1=True - death occured
        'seriousnessdeath
        'string
        'This value is 1 if the adverse event resulted in death, and absent otherwise.

        Public Property SeriousnessOther As String
        'seriousnessother
        'string
        'This value is 1 if the adverse event resulted in some other serious condition, and absent otherwise.

        Public Property SeriousnessHospitalization As String
        'seriousnesshospitalization
        'string
        'This value is 1 if the adverse event resulted in a hospitalization, and absent otherwise.


        Public Property SeriousnessCongenitalanomali As String
        'seriousnesscongenitalanomali
        'string
        'This value is 1 if the adverse event resulted in a congenital anomali, and absent otherwise.

        Public Property SeriousnessDisabling As String
        'seriousnessdisabling
        'string
        'This value is 1 if the adverse event resulted in disability, and absent otherwise.
        Public Property SeriousnSeriousnessLifeThreateningessOther As String
        'seriousnesslifethreatening
        'string
        'This value is 1 if the adverse event resulted in a life threatening condition, and absent otherwise.

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Convert JSON Data
        ''' </summary>
        ''' <param name="jsondata">JSON Object</param>
        ''' <returns>List of Adverse Drug Events</returns>
        ''' <remarks></remarks>
        Public Shared Function CnvJsonDataToList(jsondata As JObject) As List(Of AdverseDrugEvent)

            Dim result As New List(Of AdverseDrugEvent)

            For Each obj In jsondata.GetValue("results")

                Dim tmp As New AdverseDrugEvent

                tmp.CompanyNumb = obj("companynumb")
                tmp.SafetyReportId = obj("safetyreportid")
                tmp.FulfillExpediteCriteria = obj("fulfillexpeditecriteria")
                tmp.ReceiveDateFormat = obj("receivedateformat")
                tmp.ReceiptDateFormat = obj("receiptdateformat")
                tmp.PrimarySource = obj("primarysource")
                tmp.ReceiveDate = obj("receivedate")
                tmp.OccurCountry = obj("occurcountry")


                tmp.Serious = obj("serious")
                tmp.SeriousnessCongenitalanomali = obj("seriousnesscongenitalanomali")
                tmp.SeriousnessDeath = obj("seriousnessdeath")
                tmp.SeriousnessDisabling = obj("seriousnessdisabling")
                tmp.SeriousnessHospitalization = obj("seriousnesshospitalization")
                tmp.SeriousnessLifeThreatening = obj("seriousnesslifethreatening")
                tmp.SeriousnessOther = obj("seriousnessother")

                tmp.SafetyReportId = obj("safetyreportid")
                tmp.SafetyReportVersion = obj("safetyreportversion")


                tmp.Patient = PatientData.ConvertJsonDate(obj("patient"))

                result.Add(tmp)

            Next

            Return result

        End Function

        ''' <summary>
        ''' Convert JSON Data 
        ''' </summary>
        ''' <param name="jsondata">JSON as String</param>
        ''' <returns>LIst of Adverse Drug Event Objects</returns>
        ''' <remarks></remarks>
        Public Shared Function CnvJsonDataToList(jsondata As String) As List(Of AdverseDrugEvent)

            Dim jo As JObject = JObject.Parse(jsondata)

            Return CnvJsonDataToList(jo)

        End Function

#End Region

    End Class

End Namespace
