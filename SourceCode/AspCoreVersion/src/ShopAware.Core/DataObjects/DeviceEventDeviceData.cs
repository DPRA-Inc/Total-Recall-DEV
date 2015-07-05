using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        public class DeviceEventDeviceData
        {
            #region Properties

            //Number identifying this particular device.
            //For example, the first device object will have the value 1. This is an enumeration corresponding to the number of patients involved in an adverse event.
            public string DeviceSequenceNumber { get; set; }

            public string DeviceEventKey { get; set; }

            public string DateReceived { get; set; }

            //IDENTIFICATION

            //The trade or proprietary name of the suspect medical device as used in product labeling or in the catalog (e.g. Flo-Easy Catheter, Reliable Heart Pacemaker, etc.).
            //If the suspect device is a reprocessed single-use device, this field will contain NA.
            public string BrandName { get; set; }

            //The generic or common name of the suspect medical device or a generally descriptive name (e.g. urological catheter, heart pacemaker, patient restraint, etc.).
            public string GenericName { get; set; }

            //Three-letter FDA Product Classification Code. Medical devices are classified under 21 CFR Parts 862-892.
            //The assigned FDA Product Classification Code (procode) can be identified using the Product Classification Database.
            public string DeviceReportProductCode { get; set; }

            //Device model and catalog numbers

            //The exact model number found on the device label or accompanying packaging.
            public string ModelNumber { get; set; }

            //The exact number as it appears in the manufacturer’s catalog, device labeling, or accompanying packaging.
            public string CatalogNumber { get; set; }

            //If available, the lot number found on the label or packaging material.
            public string LotNumber { get; set; }

            //Any other identifier that might be used to identify the device. Expect wide variability in the use of this field. It is commonly empty, or marked NA, N/A, *, or UNK, if unknown or not applicable.
            public string OtherIdNumber { get; set; }

            //Age and expiration date

            //If available; this date is often be found on the device itself or printed on the accompanying packaging.
            public string ExpirationDateOfDevice { get; set; }

            //Age of the device or a best estimate, often including the unit of time used. Contents vary widely, but common patterns include:
            //nn YR or n.n YR = Device age nn or n.n years.
            //nn MO or n.n MO = Device age nn or n.n months.
            //nn DA or nn DA or nn DAY = Device age nn or n.n days.
            //UNK or UNKNOWN = Device age unknown.
            //DA = Documentation forthcoming.
            //NO INFO = Documentation forthcoming.* or
            //empty if information not provided.
            public string DeviceAgeText { get; set; }

            //Evaluation by manufacturer

            //Whether the device is available for evaluation by the manufacturer, or whether the device was returned to the manufacturer.
            //Yes
            //No
            //Device was returned to manufacturerNo answer provided
            //I = Documentation forthcoming.May also be
            //empty if no answer provided.
            public string DeviceAvailability { get; set; }

            //Date the device was returned to the manufacturer, if applicable.
            public string DateReturnedToManufacturer { get; set; }

            public string DeviceEvaluatedByManufacturer { get; set; }

            //Whether the suspect device was evaluated by the manufacturer.
            //Yes = An evaluation was made of the suspect or related medical device.
            //No = An evaluation of a returned suspect or related medical device was not conducted.
            //Device not returned to manufacturer = An evaluation could not be made because the device was not returned to, or made available to, the manufacturer.
            //No answer provided or
            //empty = No answer was provided or this information was unavailable.

            //Use of device

            public string DeviceOperator { get; set; }

            //Whether a device was implanted or not. May be either marked N or left empty if this was not applicable.
            public string ImplantFlag { get; set; }

            //Whether an implanted device was removed from the patient, and if so, what kind of date was provided.Month and year provided only day defaults to 01 = Only a year and month were provided. Day was set to 01.Year provided only = Only a year was provided. Month was set to 01 (January) and day set to 01.No information at this time = Documentation forthcoming.Not available = Documentation forthcoming.Unknown = Documentation forthcoming.* = Documentation forthcoming.B = Documentation forthcoming.V = Documentation forthcoming.
            public string DateRemovedFlag { get; set; }

            //Manufacturer

            //Device manufacturer name.
            public string ManufacturerName { get; set; }

            //Device manufacturer address line 1.
            public string ManufacturerAddress1 { get; set; }

            //Device manufacturer address line 2.
            public string ManufacturerAddress2 { get; set; }

            public string ManufacturerCity { get; set; }

            public string ManufacturerState { get; set; }

            public string ManufacturerCountry { get; set; }

            //Device manufacturer 5-digit zip code.
            public string ManufacturerZipCode { get; set; }

            //Device manufacturer 4-digit zip code extension (zip+4 code).
            public string ManufacturerZipCodeExt { get; set; }

            //Device manufacturer postal code. May contain the zip code for addresses in the United States.
            public string ManufacturerPostalCode { get; set; }

            #endregion

            #region Public Methods

            /// <summary>
            ///     Convert JSON Data
            /// </summary>
            /// <param name="jsondata">JSON Object</param>
            /// <returns>List(Of DeviceEventDeviceData)</returns>
            /// <remarks></remarks>
            public static List<DeviceEventDeviceData> CnvJsonDataToList(JArray jsondata)
            {
                var result = new List<DeviceEventDeviceData>();

                foreach (var obj in jsondata)
                {
                    var tmp = new DeviceEventDeviceData();

                    tmp.DeviceSequenceNumber = Utilities.GetJTokenString(obj, "device_sequence_number");
                    tmp.DeviceEventKey = Utilities.GetJTokenString(obj, "device_event_key");
                    tmp.DateReceived = Utilities.GetJTokenString(obj, "date_received");

                    tmp.BrandName = Utilities.GetJTokenString(obj, "brand_name");
                    tmp.GenericName = Utilities.GetJTokenString(obj, "generic_name");
                    tmp.DeviceReportProductCode = Utilities.GetJTokenString(obj, "device_report_product_code");

                    tmp.ModelNumber = Utilities.GetJTokenString(obj, "model_number");
                    tmp.CatalogNumber = Utilities.GetJTokenString(obj, "catalog_number");
                    tmp.LotNumber = Utilities.GetJTokenString(obj, "device.");
                    tmp.OtherIdNumber = Utilities.GetJTokenString(obj, "other_id_number");

                    tmp.ExpirationDateOfDevice = Utilities.GetJTokenString(obj, "expiration_date_of_device");
                    tmp.DeviceAgeText = Utilities.GetJTokenString(obj, "device_age_text");

                    tmp.DeviceAvailability = Utilities.GetJTokenString(obj, "device_availability");
                    tmp.DateReturnedToManufacturer = Utilities.GetJTokenString(obj, "date_returned_to_manufacturer");
                    tmp.DeviceEvaluatedByManufacturer = Utilities.GetJTokenString(obj, "device_evaluated_by_manufacturer");

                    tmp.DeviceOperator = Utilities.GetJTokenString(obj, "device_operator");
                    tmp.ImplantFlag = Utilities.GetJTokenString(obj, "implant_flag");
                    tmp.DateRemovedFlag = Utilities.GetJTokenString(obj, "date_removed_flag");

                    tmp.ManufacturerName = Utilities.GetJTokenString(obj, "manufacturer_d_name");
                    tmp.ManufacturerAddress1 = Utilities.GetJTokenString(obj, "manufacturer_d_address_1");
                    tmp.ManufacturerAddress2 = Utilities.GetJTokenString(obj, "manufacturer_d_address_2");
                    tmp.ManufacturerCity = Utilities.GetJTokenString(obj, "manufacturer_d_city");
                    tmp.ManufacturerState = Utilities.GetJTokenString(obj, "manufacturer_d_state");
                    tmp.ManufacturerCountry = Utilities.GetJTokenString(obj, "manufacturer_d_country");
                    tmp.ManufacturerZipCode = Utilities.GetJTokenString(obj, "manufacturer_d_zip_code");
                    tmp.ManufacturerZipCodeExt = Utilities.GetJTokenString(obj, "manufacturer_d_zip_code_ext");
                    tmp.ManufacturerPostalCode = Utilities.GetJTokenString(obj, "manufacturer_d_postal_code");

                    result.Add(tmp);
                }

                return result;
            }

            #endregion
        }
    }
}