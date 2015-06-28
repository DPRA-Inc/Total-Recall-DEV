#Region " Imports "

Imports ApiDalc.DataObjects
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects


    ''' <summary>
    ''' Adverse Device Event Data Object
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AdverseDeviceEvent


#Region " Properties "

        'Y = The report is about an incident where the use of the device is suspected to have resulted in an adverse outcome in a patient.
        'N = The report is not about an adverse outcome in a patient.
        'Empty if no data was available or entered.
        Public Property AdverseEventFlag As String

        'Y = The report is about the quality, performance, or safety of a device—for example, defects or malfunctions. This flag is set when a device malfunction could lead to a death or serious injury if the malfunction were to recur.
        'N = The report is not about a defect or malfunction.
        'Empty if no data was available or entered.
        Public Property ProductProblemFlag As String

        'Actual or best estimate of the date of first onset of the adverse event. This field was added in 2006.
        Public Property DateOfEvent As String
        'Date the initial reporter (whoever initially provided information to the user facility, manufacturer, or importer) provided the information about the event.
        Public Property DateReport As String
        'Date the report was received by the FDA.
        Public Property DateReceived As String

        'Number of devices noted in the adverse event report. Almost always 1. May be empty if report_source_code contains Voluntary report.
        Public Property NumberDevicesInEvent As String
        'Number of patients noted in the adverse event report. Almost always 1. May be empty if report_source_code contains Voluntary report.
        Public Property NumberPatientsInEvent As String
        'Identifying number for the adverse event report. The format varies, according to the source of the report. 
        'The field is empty when a user facility submits a report.For manufacturer reports. Manufacturer Report Number. 
        'The report number consists of three components: The manufacturer’s FDA registration number for the manufacturing site of the reported device, the 4-digit calendar year, and a consecutive 5-digit number for each report filed during the year by the manufacturer (e.g. 1234567-2013-00001, 1234567-2013-00002).For user facility/importer (distributor) reports. Distributor Report Number. Documentation forthcoming.For consumer reports. This field is empty.
        Public Property ReportNumber As String


        'Source of the adverse event report. 
        'Possible values:Manufacturer report, Voluntary report, User facility report, Distributor report
        Public Property ReportSourceCode As String
        'Whether the initial reporter was a health professional (e.g. physician, pharmacist, nurse, etc.) or not.
        'Y = The initial reporter is a health professional.
        'N = The initial reporter is not a health professional.
        Public Property HealthProfessional As String
        'Initial reporter occupation.
        'Other, Physician, Nurse, Health professional, Lay user/patient, Other health care professional, Audiologist, Dental hygienist, Dietician, 
        'Emergency medical technician, Medical technologist, Nuclear medicine technologist, Occupational therapist, Paramedic, Pharmacist, 
        'Phlebotomist, Physical therapist, Physician assistant, Radiologic technologist, Respiratory therapis, tSpeech therapist, Dentist, 
        'Other caregivers, Dental assistant, Home health aide, Medical assistant, Nursing assistantPatient, Patient family member or friend, 
        'Personal care assistantService and testing personnel, Biomedical engineer, Hospital service technician, Medical equipment company technician/representative, 
        'Physicist, Service personnel, Device unattended, Risk manager, Attorney, Unknown, Not applicable, No information, Unknown, Invalid data
        Public Property ReporterOccupationCode As String
        'Whether the initial reporter also notified or submitted a copy of this report to FDA.
        'Yes = FDA was also notified by the initial reporter.
        'No = FDA was not notified by the initial reporter.Unknown = Unknown whether FDA was also notified by the initial reporter.No answer provided or 
        'empty = This information was not provided.
        Public Property InitialReportToFda As String
        'Indicates whether the suspect device was a single-use device that was reprocessed and reused on a patient.
        'Y = Was a single-use device that was reprocessed and reused.
        'N = Was not a single-use device that was reprocessed and reused.
        'UNK = The original equipment manufacturer was unable to determine if their single-use device was reprocessed and reused.
        Public Property ReprocessedAndReusedFlag As String

        'Reporter-dependent fields
        'By user facility/importer

        'The type of report.
        'Initial submission = Initial report of an event.
        'Followup = Additional or corrected information
        '.Extra copy received = Documentation forthcoming.
        'Other information submitted = Documentation forthcoming.
        Public Property TypeOfReport As New List(Of String)
        'Date the user facility’s medical personnel or the importer (distributor) became aware that the device has or may have caused or contributed to the reported event.
        Public Property DateFacilityAware As String
        'Whether the report was sent to the FDA by a user facility or importer (distributor). User facilities are required to send reports of device-related deaths. 
        'Importers are required to send reports of device-related deaths and serious injuries.
        'Y = The report was sent to the FDA by a user facility or importer.
        'N = The report was not sent to the FDA by a user facility or importer.
        'Empty if this information was not provided.
        Public Property ReportDate As String
        Public Property ReportToFda As String
        'Date the user facility/importer (distributor) sent the report to the FDA, if applicable.
        Public Property DateReportToFda As String
        'Whether the report was sent to the manufacturer by a user facility or importer (distributor). 
        'User facilities are required to send reports of device-related deaths and serious injuries to manufacturers. 
        'Importers are required to send reports to manufacturers of device-related deaths, device-related serious injuries, and device-related malfunctions that could cause or contribute to a death or serious injury.
        'Y = The report was sent to the manufacturer by a user facility or importer.
        'N = The report was not sent to the manufacturer by a user facility or importer.
        'Empty if this information was not provided.
        Public Property ReportToManufacturer As String
        'Date the user facility/importer (distributor) sent the report to the manufacturer, if applicable.
        Public Property DateReportToManufacturer As String
        Public Property EventLocation As String

        'Name and address

        'User facility or importer (distributor) name.
        Public Property DistributorName As String
        Public Property DistributorAddress1 As String
        Public Property DistributorAddress2 As String
        Public Property DistributorCity As String
        Public Property DistributorState As String
        Public Property DistributorZipCode As String
        Public Property DistributorZipCodeExt As String

        'Suspect device manufacturer'
        Public Property ManufacturerName As String
        Public Property ManufacturerAddress1 As String
        Public Property ManufacturerAddress2 As String
        Public Property ManufacturerCity As String
        Public Property ManufacturerState As String
        Public Property ManufacturerZipCode As String
        Public Property ManufacturerZipCodeExt As String
        Public Property ManufacturerCountry As String
        Public Property ManufacturerPostalCode As String

        'By device manufacturer

        'Outcomes associated with the adverse event.
        'Death = Death, either caused by or associated with the adverse event.
        'Injury (IN) = Documentation forthcoming.
        'Injury (IL) = Documentation forthcoming.
        'Injury (IJ) = Documentation forthcoming.
        'Malfunction = Product malfunction.
        'Other = Other serious/important medical event.
        'No answer provided = No information was provided.
        Public Property EventType As String

        'date string - YYYYmmdd
        'Date of manufacture of the suspect medical device.U = Unknown.
        Public Property DeviceDateOfManufacture As String
        'Whether the device was labeled for single use or not.
        'Yes = The device was labeled for single use.
        'No = The device was not labeled for single use, or this is irrelevant to the device being reported (e.g. an X-ray machine).
        'Empty = This information was not provided.
        Public Property SingleUseFlag As String
        'Whether the use of the suspect medical device was the initial use, reuse, or unknown.
        'I = Initial use
        '.R = Reuse.
        'U = Unknown.
        '* or empty = Invalid data or this information was not provided.
        Public Property PreviousUseCode As String

        'Corrective or remedial action


        'Follow-up actions taken by the device manufacturer at the time of the report submission, if applicable.
        'Recall
        'Repair
        'Replace
        'Relabeling
        'Other
        'Notification
        'Inspection
        'Patient Monitoring
        'Modification/Adjustment
        'Invalid Data
        Public Property RemedialAction As New List(Of String)
        'If a corrective action was reported to FDA under 21 USC 360i(f), the correction or removal reporting number (according to the format directed by 21 CFR 807). 
        'If a firm has not submitted a correction or removal report to the FDA, but the FDA has assigned a recall number to the corrective action, the recall number may be used.
        Public Property RemovalCorrectionNumber As String

        'Contact

        'Contact person
        Public Property ManufactureContactNameTitle As String
        Public Property ManufactureContactNameFirst As String
        Public Property ManufactureContactNameLast As String

        'Contact person address

        Public Property ManufactureContactStreet1 As String
        Public Property ManufactureContactStreet2 As String
        Public Property ManufactureContactCity As String
        Public Property ManufactureContactState As String
        Public Property ManufactureContactZipCode As String
        Public Property ManufactureContactZipCodeExt As String
        Public Property ManufactureContactPostal As String
        Public Property ManufactureContactCountry As String

        'Contact person phone number

        'Manufacturer contact person phone number country code. Note: For medical device adverse event reports, comparing country codes with city names in the same record demonstrates widespread use of conflicting codes. Caution should be exercised when interpreting country code data in device records.
        Public Property ManufactureContactPhoneCountry As String
        Public Property ManufactureContactPhoneAreaCode As String
        Public Property ManufactureContactPhoneExchange As String
        Public Property ManufactureContactPhoneExtension As String
        Public Property ManufactureContactPhoneCity As String
        Public Property ManufactureContactPhoneNumber As String
        Public Property ManufactureContactLocal As String

        'Manufacturer name and address


        Public Property ManufactureName As String
        Public Property ManufactureCity As String
        Public Property ManufactureStreet1 As String
        Public Property ManufactureStreet2 As String
        Public Property ManufactureState As String
        Public Property ManufactureZipCode As String
        Public Property ManufactureZipCodeExt As String
        Public Property ManufacturePostalCode As String
        Public Property ManufactureCountry As String

        'By any manufacturer


        Public Property DateManufacturerReceived As String
        Public Property SourceType As New List(Of String)

        'Keys and flags


        Public Property EventKey As String
        Public Property MdrReportKey As String
        Public Property ManufacturerLinkFlag As String

        Public Property Device As New List(Of DeviceEventDeviceData)
        Public Property Patient As New List(Of DeviceEventPatientData)
        Public Property MdrText As New List(Of DeviceEventMdrTextData)

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Convert JSON Data
        ''' </summary>
        ''' <param name="jsondata">JSON Object</param>
        ''' <returns>List of Adverse Device Events</returns>
        ''' <remarks></remarks>
        Public Shared Function CnvJsonDataToList(jsondata As JObject) As List(Of AdverseDeviceEvent)


            Dim result As New List(Of AdverseDeviceEvent)
            'jsondata.dump("In")
            For Each obj In jsondata.GetValue("results")
                'obj.Dump("Obj")
                Dim tmp As New AdverseDeviceEvent


                With tmp
                    .AdverseEventFlag = obj("adverse_event_flag")
                    .ProductProblemFlag = obj("product_problem_flag")
                    .DateOfEvent = obj("date_of_event")
                    .DateReport = obj("date_report")
                    .DateReceived = obj("date_received")
                    .NumberDevicesInEvent = obj("number_devices_in_event")
                    .NumberPatientsInEvent = obj("number_patients_in_event")
                    'Source
                    .ReportSourceCode = obj("report_source_code")
                    .HealthProfessional = obj("health_professional")
                    .ReporterOccupationCode = obj("reporter_occupation_code")
                    .InitialReportToFda = obj("initial_report_to_fda")
                    .ReprocessedAndReusedFlag = obj("reprocessed_and_reused_flag")

                    For Each itm In obj("type_of_report")
                        .TypeOfReport.Add(itm)
                    Next

                    .DateFacilityAware = obj("date_facility_aware")
                    .ReportDate = obj("report_date")
                    .ReportToFda = obj("report_to_fda")
                    .DateReportToFda = obj("date_report_to_fda")
                    .ReportToManufacturer = obj("report_to_manufacturer")
                    .DateReportToManufacturer = obj("date_report_to_manufacturer")
                    .EventLocation = obj("event_location")

                    .DistributorName = obj("distributor_name")
                    .DistributorAddress1 = obj("distributor_address_1")
                    .DistributorAddress2 = obj("distributor_address_2")
                    .DistributorCity = obj("distributor_city")
                    .DistributorState = obj("distributor_state")
                    .DistributorZipCode = obj("distributor_zip_code")
                    .DistributorZipCodeExt = obj("distributor_zip_code_ext")

                    .ManufacturerName = obj("manufacturer_name")
                    .ManufacturerAddress1 = obj("manufacturer_address_1")
                    .ManufacturerAddress2 = obj("manufacturer_address_2")
                    .ManufacturerCity = obj("manufacturer_city")
                    .ManufacturerState = obj("manufacturer_state")
                    .ManufacturerZipCode = obj("manufacturer_zip_code")
                    .ManufacturerZipCodeExt = obj("manufacturer_zip_code_ext")
                    .ManufacturerCountry = obj("manufacturer_country")
                    .ManufacturerPostalCode = obj("manufacturer_postal_code")

                    .EventType = obj("event_type")
                    .DeviceDateOfManufacture = obj("device_date_of_manufacture")
                    .SingleUseFlag = obj("single_use_flag")
                    .PreviousUseCode = obj("previous_use_code")

                    For Each itm In obj("remedial_action")
                        .RemedialAction.Add(itm)
                    Next
                    .RemovalCorrectionNumber = obj("removal_correction_number")

                    .ManufactureContactNameTitle = obj("manufacturer_contact_t_name")
                    .ManufactureContactNameFirst = obj("manufacturer_contact_f_name")
                    .ManufactureContactNameLast = obj("manufacturer_contact_l_name")

                    .ManufactureContactStreet1 = obj("manufacturer_contact_street_1")
                    .ManufactureContactStreet2 = obj("manufacturer_contact_street_2")
                    .ManufactureContactCity = obj("manufacturer_contact_city")
                    .ManufactureContactState = obj("manufacturer_contact_state")
                    .ManufactureContactZipCode = obj("manufacturer_contact_zip_code")
                    .ManufactureContactZipCodeExt = obj("manufacturer_contact_zip_ext")
                    .ManufactureContactPostal = obj("manufacturer_contact_postal")
                    .ManufactureContactCountry = obj("manufacturer_contact_country")

                    .ManufactureContactPhoneCountry = obj("manufacturer_contact_pcountry")
                    .ManufactureContactPhoneAreaCode = obj("manufacturer_contact_area_code")
                    .ManufactureContactPhoneExchange = obj("manufacturer_contact_exchange")
                    .ManufactureContactPhoneExtension = obj("manufacturer_contact_extension")
                    .ManufactureContactPhoneCity = obj("manufacturer_contact_pcity")
                    .ManufactureContactPhoneNumber = obj("manufacturer_contact_phone_number")
                    .ManufactureContactLocal = obj("manufacturer_contact_plocal")

                    .ManufactureName = obj("manufacturer_g1_name")
                    .ManufactureStreet1 = obj("manufacturer_g1_street_1")
                    .ManufactureStreet2 = obj("manufacturer_g1_street_2")
                    .ManufactureCity = obj("manufacturer_g1_city")
                    .ManufactureState = obj("manufacturer_g1_state")
                    .ManufactureZipCode = obj("manufacturer_g1_zip_code")
                    .ManufactureZipCodeExt = obj("manufacturer_g1_zip_ext")
                    .ManufacturePostalCode = obj("manufacturer_g1_postal_code")
                    .ManufactureCountry = obj("manufacturer_g1_country")

                    .DateManufacturerReceived = obj("date_manufacturer_received")

                    For Each itm In obj("source_type")
                        .SourceType.Add(itm)
                    Next

                    .EventKey = obj("event_key")
                    .MdrReportKey = obj("mdr_report_key")
                    .ManufacturerLinkFlag = obj("manufacturer_link_flag_")

                    .Device = DeviceEventDeviceData.CnvJsonDataToList(obj("device"))
                    .Patient = DeviceEventPatientData.CnvJsonDataToList(obj("patient"))
                    .MdrText = DeviceEventMdrTextData.CnvJsonDataToList(obj("mdr_text"))


                End With

                result.Add(tmp)

            Next

            Return result

        End Function


        ''' <summary>
        ''' Convert JSON Data 
        ''' </summary>
        ''' <param name="jsondata">JSON as String</param>
        ''' <returns>LIst of Adverse Device Event Objects</returns>
        ''' <remarks></remarks>
        Public Shared Function CnvJsonDataToList(jsondata As String) As List(Of AdverseDeviceEvent)

            If jsondata.Length = 0 Then
                Return New List(Of AdverseDeviceEvent)
            End If

            Dim jo As JObject = JObject.Parse(jsondata)

            Return CnvJsonDataToList(jo)

        End Function


        Public Shared Function CnvDeviceEventsToResultDrugEvents(tmpAdverseDeviceEventList As List(Of AdverseDeviceEvent)) As List(Of SearchResultDrugEvent)

            Dim result As New List(Of SearchResultDrugEvent)

            For Each itm As AdverseDeviceEvent In tmpAdverseDeviceEventList

                Dim obj As New SearchResultDrugEvent

                With obj

                    .Classification = "Device Event"
                    .IsEvent = False
                    .IsDeviceEvent = True

                    If itm.EventType.ToString.Length > 1 Then
                        .Seriousness.Add(itm.EventType)
                    End If

                    .DateStarted = ConvertDateStringToDate(itm.DateOfEvent, "yyyyMMdd").ToShortDateString
                    .ReceiptDate = ConvertDateStringToDate(itm.DateReceived, "yyyyMMdd").ToShortDateString
                    ' DateTime.TryParse(itm.DateOfEvent, .DateStarted)
                    'DateTime.TryParse(itm.DateReceived, .ReceiptDate)

                    Dim objDetail As New SearchResultDrugEventItem

                    For Each deviceItem In itm.Device


                        If TypeOf deviceItem.BrandName Is System.String Then

                            If Not String.IsNullOrEmpty(deviceItem.BrandName) Then
                                objDetail.BrandName.Add(StrConv(deviceItem.BrandName, VbStrConv.ProperCase))
                            End If

                        Else

                            For Each brand As String In deviceItem.BrandName
                                objDetail.BrandName.Add(StrConv(brand, VbStrConv.ProperCase))
                            Next

                        End If

                        If TypeOf deviceItem.GenericName Is System.String Then

                            If Not String.IsNullOrEmpty(deviceItem.GenericName) Then
                                objDetail.GenericName.Add(StrConv(deviceItem.GenericName, VbStrConv.ProperCase))
                            End If

                        Else

                            For Each generic As String In deviceItem.GenericName
                                objDetail.GenericName.Add(StrConv(generic, VbStrConv.ProperCase))
                            Next

                        End If

                        For Each mdrItem As DeviceEventMdrTextData In itm.MdrText
                            objDetail.Route.Add(StrConv(mdrItem.Text, VbStrConv.ProperCase))
                        Next

                    Next

                    .DrugItem.Add(objDetail)

                End With

                result.Add(obj)

            Next

            Return result

        End Function

#End Region

    End Class

End Namespace

