using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ShopAware.Core.Extensions;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        /// <summary>
        ///     Adverse Device Event Data Object
        /// </summary>
        /// <remarks></remarks>
        public class AdverseDeviceEvent
        {
            #region Properties

            //Y = The report is about an incident where the use of the device is suspected to have resulted in an adverse outcome in a patient.
            //N = The report is not about an adverse outcome in a patient.
            //Empty if no data was available or entered.
            public string AdverseEventFlag { get; set; }

            //Y = The report is about the quality, performance, or safety of a device—for example, defects or malfunctions. This flag is set when a device malfunction could lead to a death or serious injury if the malfunction were to recur.
            //N = The report is not about a defect or malfunction.
            //Empty if no data was available or entered.
            public string ProductProblemFlag { get; set; }

            //Actual or best estimate of the date of first onset of the adverse event. This field was added in 2006.
            public string DateOfEvent { get; set; }

            //Date the initial reporter (whoever initially provided information to the user facility, manufacturer, or importer) provided the information about the event.
            public string DateReport { get; set; }

            //Date the report was received by the FDA.
            public string DateReceived { get; set; }

            //Number of devices noted in the adverse event report. Almost always 1. May be empty if report_source_code contains Voluntary report.
            public string NumberDevicesInEvent { get; set; }

            //Number of patients noted in the adverse event report. Almost always 1. May be empty if report_source_code contains Voluntary report.
            public string NumberPatientsInEvent { get; set; }

            //Identifying number for the adverse event report. The format varies, according to the source of the report.
            //The field is empty when a user facility submits a report.For manufacturer reports. Manufacturer Report Number.
            //The report number consists of three components: The manufacturer’s FDA registration number for the manufacturing site of the reported device, the 4-digit calendar year, and a consecutive 5-digit number for each report filed during the year by the manufacturer (e.g. 1234567-2013-00001, 1234567-2013-00002).For user facility/importer (distributor) reports. Distributor Report Number. Documentation forthcoming.For consumer reports. This field is empty.
            public string ReportNumber { get; set; }

            //Source of the adverse event report.
            //Possible values:Manufacturer report, Voluntary report, User facility report, Distributor report
            public string ReportSourceCode { get; set; }

            //Whether the initial reporter was a health professional (e.g. physician, pharmacist, nurse, etc.) or not.
            //Y = The initial reporter is a health professional.
            //N = The initial reporter is not a health professional.
            public string HealthProfessional { get; set; }

            //Initial reporter occupation.
            //Other, Physician, Nurse, Health professional, Lay user/patient, Other health care professional, Audiologist, Dental hygienist, Dietician,
            //Emergency medical technician, Medical technologist, Nuclear medicine technologist, Occupational therapist, Paramedic, Pharmacist,
            //Phlebotomist, Physical therapist, Physician assistant, Radiologic technologist, Respiratory therapis, tSpeech therapist, Dentist,
            //Other caregivers, Dental assistant, Home health aide, Medical assistant, Nursing assistantPatient, Patient family member or friend,
            //Personal care assistantService and testing personnel, Biomedical engineer, Hospital service technician, Medical equipment company technician/representative,
            //Physicist, Service personnel, Device unattended, Risk manager, Attorney, Unknown, Not applicable, No information, Unknown, Invalid data
            public string ReporterOccupationCode { get; set; }

            //Whether the initial reporter also notified or submitted a copy of this report to FDA.
            //Yes = FDA was also notified by the initial reporter.
            //No = FDA was not notified by the initial reporter.Unknown = Unknown whether FDA was also notified by the initial reporter.No answer provided or
            //empty = This information was not provided.
            public string InitialReportToFda { get; set; }

            //Indicates whether the suspect device was a single-use device that was reprocessed and reused on a patient.
            //Y = Was a single-use device that was reprocessed and reused.
            //N = Was not a single-use device that was reprocessed and reused.
            //UNK = The original equipment manufacturer was unable to determine if their single-use device was reprocessed and reused.
            public string ReprocessedAndReusedFlag { get; set; }

            //Reporter-dependent fields
            //By user facility/importer

            //The type of report.
            //Initial submission = Initial report of an event.
            //Followup = Additional or corrected information
            //.Extra copy received = Documentation forthcoming.
            //Other information submitted = Documentation forthcoming.
            public List<string> TypeOfReport { get; set; }

            //Date the user facility’s medical personnel or the importer (distributor) became aware that the device has or may have caused or contributed to the reported event.
            public string DateFacilityAware { get; set; }

            //Whether the report was sent to the FDA by a user facility or importer (distributor). User facilities are required to send reports of device-related deaths.
            //Importers are required to send reports of device-related deaths and serious injuries.
            //Y = The report was sent to the FDA by a user facility or importer.
            //N = The report was not sent to the FDA by a user facility or importer.
            //Empty if this information was not provided.
            public string ReportDate { get; set; }

            public string ReportToFda { get; set; }

            //Date the user facility/importer (distributor) sent the report to the FDA, if applicable.
            public string DateReportToFda { get; set; }

            //Whether the report was sent to the manufacturer by a user facility or importer (distributor).
            //User facilities are required to send reports of device-related deaths and serious injuries to manufacturers.
            //Importers are required to send reports to manufacturers of device-related deaths, device-related serious injuries, and device-related malfunctions that could cause or contribute to a death or serious injury.
            //Y = The report was sent to the manufacturer by a user facility or importer.
            //N = The report was not sent to the manufacturer by a user facility or importer.
            //Empty if this information was not provided.
            public string ReportToManufacturer { get; set; }

            //Date the user facility/importer (distributor) sent the report to the manufacturer, if applicable.
            public string DateReportToManufacturer { get; set; }

            public string EventLocation { get; set; }

            //Name and address

            //User facility or importer (distributor) name.
            public string DistributorName { get; set; }

            public string DistributorAddress1 { get; set; }

            public string DistributorAddress2 { get; set; }

            public string DistributorCity { get; set; }

            public string DistributorState { get; set; }

            public string DistributorZipCode { get; set; }

            public string DistributorZipCodeExt { get; set; }

            //Suspect device manufacturer'
            public string ManufacturerName { get; set; }

            public string ManufacturerAddress1 { get; set; }

            public string ManufacturerAddress2 { get; set; }

            public string ManufacturerCity { get; set; }

            public string ManufacturerState { get; set; }

            public string ManufacturerZipCode { get; set; }

            public string ManufacturerZipCodeExt { get; set; }

            public string ManufacturerCountry { get; set; }

            public string ManufacturerPostalCode { get; set; }

            //By device manufacturer

            //Outcomes associated with the adverse event.
            //Death = Death, either caused by or associated with the adverse event.
            //Injury (IN) = Documentation forthcoming.
            //Injury (IL) = Documentation forthcoming.
            //Injury (IJ) = Documentation forthcoming.
            //Malfunction = Product malfunction.
            //Other = Other serious/important medical event.
            //No answer provided = No information was provided.
            public string EventType { get; set; }

            //date string - YYYYmmdd
            //Date of manufacture of the suspect medical device.U = Unknown.
            public string DeviceDateOfManufacture { get; set; }

            //Whether the device was labeled for single use or not.
            //Yes = The device was labeled for single use.
            //No = The device was not labeled for single use, or this is irrelevant to the device being reported (e.g. an X-ray machine).
            //Empty = This information was not provided.
            public string SingleUseFlag { get; set; }

            //Whether the use of the suspect medical device was the initial use, reuse, or unknown.
            //I = Initial use
            //.R = Reuse.
            //U = Unknown.
            //* or empty = Invalid data or this information was not provided.
            public string PreviousUseCode { get; set; }

            //Corrective or remedial action

            //Follow-up actions taken by the device manufacturer at the time of the report submission, if applicable.
            //Recall
            //Repair
            //Replace
            //Relabeling
            //Other
            //Notification
            //Inspection
            //Patient Monitoring
            //Modification/Adjustment
            //Invalid Data
            public List<string> RemedialAction { get; set; }

            //If a corrective action was reported to FDA under 21 USC 360i(f), the correction or removal reporting number (according to the format directed by 21 CFR 807).
            //If a firm has not submitted a correction or removal report to the FDA, but the FDA has assigned a recall number to the corrective action, the recall number may be used.
            public string RemovalCorrectionNumber { get; set; }

            //Contact

            //Contact person
            public string ManufactureContactNameTitle { get; set; }

            public string ManufactureContactNameFirst { get; set; }

            public string ManufactureContactNameLast { get; set; }

            //Contact person address

            public string ManufactureContactStreet1 { get; set; }

            public string ManufactureContactStreet2 { get; set; }

            public string ManufactureContactCity { get; set; }

            public string ManufactureContactState { get; set; }

            public string ManufactureContactZipCode { get; set; }

            public string ManufactureContactZipCodeExt { get; set; }

            public string ManufactureContactPostal { get; set; }

            public string ManufactureContactCountry { get; set; }

            //Contact person phone number

            //Manufacturer contact person phone number country code. Note: For medical device adverse event reports, comparing country codes with city names in the same record demonstrates widespread use of conflicting codes. Caution should be exercised when interpreting country code data in device records.
            public string ManufactureContactPhoneCountry { get; set; }

            public string ManufactureContactPhoneAreaCode { get; set; }

            public string ManufactureContactPhoneExchange { get; set; }

            public string ManufactureContactPhoneExtension { get; set; }

            public string ManufactureContactPhoneCity { get; set; }

            public string ManufactureContactPhoneNumber { get; set; }

            public string ManufactureContactLocal { get; set; }

            //Manufacturer name and address

            public string ManufactureName { get; set; }

            public string ManufactureCity { get; set; }

            public string ManufactureStreet1 { get; set; }

            public string ManufactureStreet2 { get; set; }

            public string ManufactureState { get; set; }

            public string ManufactureZipCode { get; set; }

            public string ManufactureZipCodeExt { get; set; }

            public string ManufacturePostalCode { get; set; }

            public string ManufactureCountry { get; set; }

            //By any manufacturer

            public string DateManufacturerReceived { get; set; }

            public List<string> SourceType { get; set; }

            //Keys and flags

            public string EventKey { get; set; }

            public string MdrReportKey { get; set; }

            public string ManufacturerLinkFlag { get; set; }

            public List<DeviceEventDeviceData> Device { get; set; }

            public List<DeviceEventPatientData> Patient { get; set; }

            public List<DeviceEventMdrTextData> MdrText { get; set; }

            #endregion

            #region Constructors

            public AdverseDeviceEvent()
            {
                // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
                TypeOfReport = new List<string>();
                RemedialAction = new List<string>();
                SourceType = new List<string>();
                Device = new List<DeviceEventDeviceData>();
                Patient = new List<DeviceEventPatientData>();
                MdrText = new List<DeviceEventMdrTextData>();
            }

            #endregion

            #region Public Methods

            public static List<SearchResultDrugEvent> CnvDeviceEventsToResultDrugEvents(List<AdverseDeviceEvent> tmpAdverseDeviceEventList)
            {
                var result = new List<SearchResultDrugEvent>();

                foreach (var itm in tmpAdverseDeviceEventList)
                {
                    var obj = new SearchResultDrugEvent();

                    obj.Classification = "Device Event";
                    obj.IsEvent = false;
                    obj.IsDeviceEvent = true;

                    if (itm.EventType.ToString().
                            Length > 1)
                    {
                        obj.Seriousness.Add(itm.EventType);
                    }

                    obj.DateStarted = Utilities.ConvertDateStringToDate(itm.DateOfEvent, "yyyyMMdd").
                                                ToString("ddMMMyyyy");
                    obj.ReportDate = Utilities.ConvertDateStringToDate(itm.DateReceived, "yyyyMMdd").
                                               ToString("ddMMMyyyy");
                    // DateTime.TryParse(itm.DateOfEvent, .DateStarted)
                    //DateTime.TryParse(itm.DateReceived, .ReceiptDate)

                    var objDetail = new SearchResultDrugEventItem();

                    foreach (var deviceItem in itm.Device)
                    {
                        if (!string.IsNullOrEmpty(deviceItem.BrandName))
                        {
                            objDetail.BrandName.Add(deviceItem.BrandName.ToTitleCase());
                        }

                        if (!string.IsNullOrEmpty(deviceItem.GenericName))
                        {
                            objDetail.GenericName.Add(deviceItem.GenericName.ToTitleCase());
                        }

                        foreach (var mdrItem in itm.MdrText)
                        {
                            obj.Description.Add(mdrItem.Text.ToTitleCase());
                        }
                    }

                    obj.DrugItem.Add(objDetail);

                    result.Add(obj);
                }

                return result;
            }

            /// <summary>
            ///     Convert JSON Data
            /// </summary>
            /// <param name="jsondata">JSON Object</param>
            /// <returns>List of Adverse Device Events</returns>
            /// <remarks></remarks>
            public static List<AdverseDeviceEvent> CnvJsonDataToList(JObject jsondata)
            {
                var result = new List<AdverseDeviceEvent>();
                //jsondata.dump("In")
                foreach (var obj in jsondata.GetValue("results"))
                {
                    //obj.Dump("Obj")
                    var tmp = new AdverseDeviceEvent();

                    tmp.AdverseEventFlag = Utilities.GetJTokenString(obj, "adverse_event_flag");
                    tmp.ProductProblemFlag = Utilities.GetJTokenString(obj, "product_problem_flag");
                    tmp.DateOfEvent = Utilities.GetJTokenString(obj, "date_of_event");
                    tmp.DateReport = Utilities.GetJTokenString(obj, "date_report");
                    tmp.DateReceived = Utilities.GetJTokenString(obj, "date_received");
                    tmp.NumberDevicesInEvent = Utilities.GetJTokenString(obj, "number_devices_in_event");
                    tmp.NumberPatientsInEvent = Utilities.GetJTokenString(obj, "number_patients_in_event");
                    //Source
                    tmp.ReportSourceCode = Utilities.GetJTokenString(obj, "report_source_code");
                    tmp.HealthProfessional = Utilities.GetJTokenString(obj, "health_professional");
                    tmp.ReporterOccupationCode = Utilities.GetJTokenString(obj, "reporter_occupation_code");
                    tmp.InitialReportToFda = Utilities.GetJTokenString(obj, "initial_report_to_fda");
                    tmp.ReprocessedAndReusedFlag = Utilities.GetJTokenString(obj, "reprocessed_and_reused_flag");

                    var reportTypes = obj.Value<JArray>("type_of_report");

                    if (reportTypes != null)
                    {
                        foreach (var itm in reportTypes)
                        {
                            tmp.TypeOfReport.Add((itm).ToString());
                        }
                    }

                    // Not an array
                    //var remedialAction = obj.Value<JArray>("remedial_action");

                    //if (remedialAction != null)
                    //{
                    //    foreach (var itm in remedialAction)
                    //    {
                    //        tmp.RemedialAction.Add((itm).ToString());
                    //    }
                    //}

                    var sourceType = obj.Value<JArray>("source_type");

                    if (sourceType != null)
                    {
                        foreach (var itm in sourceType)
                        {
                            tmp.SourceType.Add((itm).ToString());
                        }
                    }

                    tmp.DateFacilityAware = Utilities.GetJTokenString(obj, "date_facility_aware");
                    tmp.ReportDate = Utilities.GetJTokenString(obj, "report_date");
                    tmp.ReportToFda = Utilities.GetJTokenString(obj, "report_to_fda");
                    tmp.DateReportToFda = Utilities.GetJTokenString(obj, "date_report_to_fda");
                    tmp.ReportToManufacturer = Utilities.GetJTokenString(obj, "report_to_manufacturer");
                    tmp.DateReportToManufacturer = Utilities.GetJTokenString(obj, "date_report_to_manufacturer");
                    tmp.EventLocation = Utilities.GetJTokenString(obj, "event_location");

                    tmp.DistributorName = Utilities.GetJTokenString(obj, "distributor_name");
                    tmp.DistributorAddress1 = Utilities.GetJTokenString(obj, "distributor_address_1");
                    tmp.DistributorAddress2 = Utilities.GetJTokenString(obj, "distributor_address_2");
                    tmp.DistributorCity = Utilities.GetJTokenString(obj, "distributor_city");
                    tmp.DistributorState = Utilities.GetJTokenString(obj, "distributor_state");
                    tmp.DistributorZipCode = Utilities.GetJTokenString(obj, "distributor_zip_code");
                    tmp.DistributorZipCodeExt = Utilities.GetJTokenString(obj, "distributor_zip_code_ext");

                    tmp.ManufacturerName = Utilities.GetJTokenString(obj, "manufacturer_name");
                    tmp.ManufacturerAddress1 = Utilities.GetJTokenString(obj, "manufacturer_address_1");
                    tmp.ManufacturerAddress2 = Utilities.GetJTokenString(obj, "manufacturer_address_2");
                    tmp.ManufacturerCity = Utilities.GetJTokenString(obj, "manufacturer_city");
                    tmp.ManufacturerState = Utilities.GetJTokenString(obj, "manufacturer_state");
                    tmp.ManufacturerZipCode = Utilities.GetJTokenString(obj, "manufacturer_zip_code");
                    tmp.ManufacturerZipCodeExt = Utilities.GetJTokenString(obj, "manufacturer_zip_code_ext");
                    tmp.ManufacturerCountry = Utilities.GetJTokenString(obj, "manufacturer_country");
                    tmp.ManufacturerPostalCode = Utilities.GetJTokenString(obj, "manufacturer_postal_code");

                    tmp.EventType = Utilities.GetJTokenString(obj, "event_type");
                    tmp.DeviceDateOfManufacture = Utilities.GetJTokenString(obj, "device_date_of_manufacture");
                    tmp.SingleUseFlag = Utilities.GetJTokenString(obj, "single_use_flag");
                    tmp.PreviousUseCode = Utilities.GetJTokenString(obj, "previous_use_code");

                    tmp.RemovalCorrectionNumber = Utilities.GetJTokenString(obj, "removal_correction_number");

                    tmp.ManufactureContactNameTitle = Utilities.GetJTokenString(obj, "manufacturer_contact_t_name");
                    tmp.ManufactureContactNameFirst = Utilities.GetJTokenString(obj, "manufacturer_contact_f_name");
                    tmp.ManufactureContactNameLast = Utilities.GetJTokenString(obj, "manufacturer_contact_l_name");

                    tmp.ManufactureContactStreet1 = Utilities.GetJTokenString(obj, "manufacturer_contact_street_1");
                    tmp.ManufactureContactStreet2 = Utilities.GetJTokenString(obj, "manufacturer_contact_street_2");
                    tmp.ManufactureContactCity = Utilities.GetJTokenString(obj, "manufacturer_contact_city");
                    tmp.ManufactureContactState = Utilities.GetJTokenString(obj, "manufacturer_contact_state");
                    tmp.ManufactureContactZipCode = Utilities.GetJTokenString(obj, "manufacturer_contact_zip_code");
                    tmp.ManufactureContactZipCodeExt = Utilities.GetJTokenString(obj, "manufacturer_contact_zip_ext");
                    tmp.ManufactureContactPostal = Utilities.GetJTokenString(obj, "manufacturer_contact_postal");
                    tmp.ManufactureContactCountry = Utilities.GetJTokenString(obj, "manufacturer_contact_country");

                    tmp.ManufactureContactPhoneCountry = Utilities.GetJTokenString(obj, "manufacturer_contact_pcountry");
                    tmp.ManufactureContactPhoneAreaCode = Utilities.GetJTokenString(obj, "manufacturer_contact_area_code");
                    tmp.ManufactureContactPhoneExchange = Utilities.GetJTokenString(obj, "manufacturer_contact_exchange");
                    tmp.ManufactureContactPhoneExtension = Utilities.GetJTokenString(obj, "manufacturer_contact_extension");
                    tmp.ManufactureContactPhoneCity = Utilities.GetJTokenString(obj, "manufacturer_contact_pcity");
                    tmp.ManufactureContactPhoneNumber = Utilities.GetJTokenString(obj, "manufacturer_contact_phone_number");
                    tmp.ManufactureContactLocal = Utilities.GetJTokenString(obj, "manufacturer_contact_plocal");

                    tmp.ManufactureName = Utilities.GetJTokenString(obj, "manufacturer_g1_name");
                    tmp.ManufactureStreet1 = Utilities.GetJTokenString(obj, "manufacturer_g1_street_1");
                    tmp.ManufactureStreet2 = Utilities.GetJTokenString(obj, "manufacturer_g1_street_2");
                    tmp.ManufactureCity = Utilities.GetJTokenString(obj, "manufacturer_g1_city");
                    tmp.ManufactureState = Utilities.GetJTokenString(obj, "manufacturer_g1_state");
                    tmp.ManufactureZipCode = Utilities.GetJTokenString(obj, "manufacturer_g1_zip_code");
                    tmp.ManufactureZipCodeExt = Utilities.GetJTokenString(obj, "manufacturer_g1_zip_ext");
                    tmp.ManufacturePostalCode = Utilities.GetJTokenString(obj, "manufacturer_g1_postal_code");
                    tmp.ManufactureCountry = Utilities.GetJTokenString(obj, "manufacturer_g1_country");
                    tmp.DateManufacturerReceived = Utilities.GetJTokenString(obj, "date_manufacturer_received");

                    tmp.EventKey = Utilities.GetJTokenString(obj, "event_key");
                    tmp.MdrReportKey = Utilities.GetJTokenString(obj, "mdr_report_key");
                    tmp.ManufacturerLinkFlag = Utilities.GetJTokenString(obj, "manufacturer_link_flag_");

                    tmp.Device = DeviceEventDeviceData.CnvJsonDataToList((JArray) Utilities.GetJTokenObject(obj, "device"));
                    tmp.Patient = DeviceEventPatientData.CnvJsonDataToList((JArray) Utilities.GetJTokenObject(obj, "patient"));
                    tmp.MdrText = DeviceEventMdrTextData.CnvJsonDataToList((JArray) Utilities.GetJTokenObject(obj, "mdr_text"));

                    result.Add(tmp);
                }

                return result;
            }

            /// <summary>
            ///     Convert JSON Data
            /// </summary>
            /// <param name="jsondata">JSON as String</param>
            /// <returns>LIst of Adverse Device Event Objects</returns>
            /// <remarks></remarks>
            public static List<AdverseDeviceEvent> CnvJsonDataToList(string jsondata)
            {
                if (jsondata.Length == 0)
                {
                    return new List<AdverseDeviceEvent>();
                }

                var jo = JObject.Parse(jsondata);

                return CnvJsonDataToList(jo);
            }

            #endregion
        }
    }
}