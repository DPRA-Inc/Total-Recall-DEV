#Region " Imports "

Imports ApiDalc.DataObjects
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    Public Class DeviceEventPatientData

#Region " Properties "

        Public Property PatientSequenceNumber As String
        Public Property DateReceived As String
        Public Property SequenceNumberTreatment As String
        Public Property SequenceNumberOutcome As New List(Of String)
        ' Public Property MdrText As New List(Of DeviceEventMdrTextData)

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Convert JSON Data
        ''' </summary>
        ''' <param name="jsondata">JSON Object</param>
        ''' <returns>List(Of DeviceEventPatientData)</returns>
        ''' <remarks></remarks>
        Public Shared Function CnvJsonDataToList(jsondata As JArray) As List(Of DeviceEventPatientData)

            Dim result As New List(Of DeviceEventPatientData)

            For Each obj In jsondata

                Dim tmp As New DeviceEventPatientData

                With tmp

                    .PatientSequenceNumber = obj("patient_sequence_number")
                    .DateReceived = obj("date_received")
                    .SequenceNumberTreatment = obj("sequence_number_treatment")
                    For Each itm In obj("sequence_number_outcome")
                        .SequenceNumberOutcome.Add(itm)
                    Next

                    '.MdrText = DeviceEventMdrTextData.CnvJsonDataToList(obj("mdr_text"))

                End With

                result.Add(tmp)

            Next

            Return result

        End Function

#End Region

    End Class

End Namespace
