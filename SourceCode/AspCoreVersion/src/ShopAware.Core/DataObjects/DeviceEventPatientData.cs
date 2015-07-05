using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        public class DeviceEventPatientData
        {
            #region Properties

            public string PatientSequenceNumber { get; set; }

            public string DateReceived { get; set; }

            public string SequenceNumberTreatment { get; set; }

            public List<string> SequenceNumberOutcome { get; set; }

            #endregion

            #region Constructors

            public DeviceEventPatientData()
            {
                // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
                SequenceNumberOutcome = new List<string>();
            }

            #endregion

            #region Public Methods

            /// <summary>
            ///     Convert JSON Data
            /// </summary>
            /// <param name="jsondata">JSON Object</param>
            /// <returns>List(Of DeviceEventPatientData)</returns>
            /// <remarks></remarks>
            public static List<DeviceEventPatientData> CnvJsonDataToList(JArray jsondata)
            {
                var result = new List<DeviceEventPatientData>();

                foreach (var obj in jsondata)
                {
                    var tmp = new DeviceEventPatientData();

                    tmp.PatientSequenceNumber = (obj["patient_sequence_number"]).ToString();
                    tmp.DateReceived = (obj["date_received"]).ToString();
                    tmp.SequenceNumberTreatment = (obj["sequence_number_treatment"]).ToString();
                    foreach (var itm in obj["sequence_number_outcome"])
                    {
                        tmp.SequenceNumberOutcome.Add((itm).ToString());
                    }

                    //.MdrText = DeviceEventMdrTextData.CnvJsonDataToList(obj("mdr_text"))

                    result.Add(tmp);
                }

                return result;
            }

            #endregion
        }
    }
}