#Region " Imports "

Imports ApiDalc.DataObjects
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    Public Class DeviceEventMdrTextData

#Region " Properties "

        Public Property PatientSequenceNumber As String
        Public Property TextTypeCode As String
        Public Property Text As String
        Public Property MdrTextKey As String
        Public Property DateReceived As String

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Convert JSON Data
        ''' </summary>
        ''' <param name="jsondata">JSON Object</param>
        ''' <returns>MDR Text Data</returns>
        ''' <remarks></remarks>
        Public Shared Function CnvJsonDataToList(jsondata As JArray) As List(Of DeviceEventMdrTextData)

            Dim result As New List(Of DeviceEventMdrTextData)

            For Each obj In jsondata

                Dim tmp As New DeviceEventMdrTextData

                With tmp

                    .PatientSequenceNumber = obj("patient_sequence_number")
                    .TextTypeCode = obj("text_type_code")
                    .Text = obj("text")
                    .MdrTextKey = obj("mdr_text_key")
                    .DateReceived = obj("date_received")

                End With

                result.Add(tmp)

            Next

            Return result

        End Function

#End Region

    End Class

End Namespace

