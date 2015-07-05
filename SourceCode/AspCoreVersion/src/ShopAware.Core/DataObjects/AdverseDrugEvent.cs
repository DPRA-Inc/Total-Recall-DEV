using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        /// <summary>
        ///     Adverse Drug Event Data Object
        /// </summary>
        /// <remarks></remarks>
        public class AdverseDrugEvent
        {
            #region Properties

            public string CompanyNumb { get; set; }

            public string Duplicate { get; set; }

            public string FulfillExpediteCriteria { get; set; }

            public string ReceiveDateFormat { get; set; }

            public object Receiver { get; set; }

            public object ReportDuplicate { get; set; }

            public string SeriousnessLifeThreatening { get; set; }

            public string ReportType { get; set; }

            public string ReceiptDateFormat { get; set; }

            public string ReceiveDate { get; set; }

            public object Sender { get; set; }

            public PatientData Patient { get; set; }

            public string ReceiptDate { get; set; }

            public string SafetyReportVersion { get; set; }

            public string SafetyReportId { get; set; }

            public dynamic PrimarySource { get; set; }

            public string PrimarySourceCountry { get; set; }

            public string TransmissionDate { get; set; }

            public string TransmissionDateFormat { get; set; }

            public string OccurCountry { get; set; }

            public string Serious { get; set; }

            //    serious
            //string
            //1 = The adverse event resulted in death, a life threatening condition, hospitalization, disability, congenital anomali, or other serious condition.
            //2 = The adverse event did not result in any of the above.

            public string SeriousnessDeath { get; set; } // boolean 1=True - death occured
            //seriousnessdeath
            //string
            //This value is 1 if the adverse event resulted in death, and absent otherwise.

            public string SeriousnessOther { get; set; }

            //seriousnessother
            //string
            //This value is 1 if the adverse event resulted in some other serious condition, and absent otherwise.

            public string SeriousnessHospitalization { get; set; }

            //seriousnesshospitalization
            //string
            //This value is 1 if the adverse event resulted in a hospitalization, and absent otherwise.

            public string SeriousnessCongenitalAnomali { get; set; }

            //seriousnesscongenitalanomali
            //string
            //This value is 1 if the adverse event resulted in a congenital anomali, and absent otherwise.

            public string SeriousnessDisabling { get; set; }

            //seriousnessdisabling
            //string
            //This value is 1 if the adverse event resulted in disability, and absent otherwise.
            public string SeriousnessLifeThreateningOther { get; set; }

            #endregion

            #region Public Methods

            /// <summary>
            ///     Convert JSON Data
            /// </summary>
            /// <param name="jsondata">JSON Object</param>
            /// <returns>List of Adverse Drug Events</returns>
            /// <remarks></remarks>
            public static List<AdverseDrugEvent> CnvJsonDataToList(JObject jsondata)
            {
                return jsondata.GetValue("results").
                                Select(obj => new AdverseDrugEvent
                                              {
                                                  CompanyNumb = Utilities.GetJTokenString(obj, "companynumb"),
                                                  SafetyReportId = Utilities.GetJTokenString(obj, "safetyreportid"),
                                                  FulfillExpediteCriteria = Utilities.GetJTokenString(obj, "fulfillexpeditecriteria"),
                                                  ReceiveDateFormat = Utilities.GetJTokenString(obj, "receivedateformat"),
                                                  ReceiptDateFormat = Utilities.GetJTokenString(obj, "receiptdateformat"),
                                                  PrimarySource = obj["primarysource"],
                                                  ReceiveDate = Utilities.GetJTokenString(obj, "receivedate"),
                                                  OccurCountry = Utilities.GetJTokenString(obj, "occurcountry"),
                                                  Serious = Utilities.GetJTokenString(obj, "serious"),
                                                  SeriousnessCongenitalAnomali = Utilities.GetJTokenString(obj, "seriousnesscongenitalanomali"),
                                                  SeriousnessDeath = Utilities.GetJTokenString(obj, "seriousnessdeath"),
                                                  SeriousnessDisabling = Utilities.GetJTokenString(obj, "seriousnessdisabling"),
                                                  SeriousnessHospitalization = Utilities.GetJTokenString(obj, "seriousnesshospitalization"),
                                                  SeriousnessLifeThreatening = Utilities.GetJTokenString(obj, "seriousnesslifethreatening"),
                                                  SeriousnessOther = Utilities.GetJTokenString(obj, "seriousnessother"),
                                                  SafetyReportVersion = Utilities.GetJTokenString(obj, "safetyreportversion"),
                                                  Patient = PatientData.ConvertJsonDate(((JObject) Utilities.GetJTokenObject(obj, "patient")))
                                              }).
                                ToList();
            }

            /// <summary>
            ///     Convert JSON Data
            /// </summary>
            /// <param name="jsondata">JSON as String</param>
            /// <returns>LIst of Adverse Drug Event Objects</returns>
            /// <remarks></remarks>
            public static List<AdverseDrugEvent> CnvJsonDataToList(string jsondata)
            {
                var jo = JObject.Parse(jsondata);

                return CnvJsonDataToList(jo);
            }

            #endregion
        }
    }
}