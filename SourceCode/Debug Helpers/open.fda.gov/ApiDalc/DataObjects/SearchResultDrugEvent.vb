#Region " Imports "

Imports ApiDalc.Enumerations
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    Public Class SearchResultDrugEvent

        Public Property PatientSex As String
        Public Property PatientAge As String
        Public Property PatientWeight As String
        Public Property IsPatientDeath As Boolean
        Public Property Reactions As New List(Of String)
        Public Property Seriousness As New List(Of String)
        Public Property PrimarySource As String
        Public Property Sender As String
        Public Property Receiver As String

        Public Property DrugItem As New List(Of SearchResultDrugEventItem)


#Region " Public Methods "

        Public Shared Function ConvertJsonData(drugEvent As List(Of AdverseDrugEvent)) As List(Of SearchResultDrugEvent)

            Dim data As New List(Of SearchResultDrugEvent)

            For Each itm As AdverseDrugEvent In drugEvent


                Dim obj As New SearchResultDrugEvent

                With obj

                    If Not itm.Patient.PatientDeathDate = Nothing Then
                        .IsPatientDeath = True
                    End If

                    .PatientSex = GetEnumDescription(itm.Patient.PatientSex)

                    'Seriousneess
                    If Not String.IsNullOrEmpty(itm.Serious) Then

                        Select Case itm.Serious
                            Case "1"
                                .Seriousness.Add("The adverse event resulted in death, a life threatening condition, hospitalization, disability, congenital anomali, or other serious condition.")
                            Case "2"
                                .Seriousness.Add("The adverse event did not result in (death, a life threatening condition, hospitalization, disability, congenital anomali, or other serious condition.)")
                        End Select

                    End If
                    If Not String.IsNullOrEmpty(itm.SeriousnessCongenitalAnomali) Then
                        If itm.SeriousnessCongenitalAnomali = "1" Then
                            .Seriousness.Add("The adverse event resulted in a congenital anomali")
                        End If
                    End If
                    If Not String.IsNullOrEmpty(itm.SeriousnessDeath) Then
                        If itm.SeriousnessDeath = "1" Then
                            .Seriousness.Add("The adverse event resulted in death")
                        End If
                    End If
                    If Not String.IsNullOrEmpty(itm.SeriousnessDisabling) Then
                        If itm.SeriousnessDisabling = "1" Then
                            .Seriousness.Add("The adverse event resulted in disability")
                        End If
                    End If
                    If Not String.IsNullOrEmpty(itm.SeriousnessHospitalization) Then
                        If itm.SeriousnessHospitalization = "1" Then
                            .Seriousness.Add("The adverse event resulted in a hospitalization")
                        End If
                    End If
                    If Not String.IsNullOrEmpty(itm.SeriousnessLifeThreatening) Then
                        If itm.SeriousnessLifeThreatening = "1" Then
                            .Seriousness.Add("The adverse event resulted in a life threatening condition")
                        End If
                    End If
                    If Not String.IsNullOrEmpty(itm.SeriousnessOther) Then
                        If itm.SeriousnessOther = "1" Then
                            .Seriousness.Add("The adverse event resulted in some other serious condition")
                        End If
                    End If

                    '.Sender
                    '.Receiver
                    ' .Reactions
                    For Each react In itm.Patient.Reaction
                        .Reactions.Add(react.ReactionMedDrapt)
                    Next

                    For Each drug In itm.Patient.Drug

                        Dim drugItem As New SearchResultDrugEventItem

                        For Each ofda In drug.OpenFDA.Brand_Name
                            drugItem.BrandName.Add(ofda)
                        Next
                        For Each ofda In drug.OpenFDA.Generic_Name
                            drugItem.GenericName.Add(ofda)
                        Next
                        For Each ofda In drug.OpenFDA.Manufacturer_Name
                            drugItem.ManufacturerName.Add(ofda)
                        Next
                        For Each ofda In drug.OpenFDA.Route
                            drugItem.Route.Add(ofda)
                        Next

                        .DrugItem.Add(drugItem)
                    Next

                End With

                data.Add(obj)

            Next

            Return data

        End Function

#End Region

    End Class


End Namespace
