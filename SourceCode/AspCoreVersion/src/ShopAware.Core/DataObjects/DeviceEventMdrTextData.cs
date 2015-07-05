using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        public class DeviceEventMdrTextData
        {
            #region Properties

            public string PatientSequenceNumber { get; set; }

            public string TextTypeCode { get; set; }

            public string Text { get; set; }

            public string MdrTextKey { get; set; }

            public string DateReceived { get; set; }

            #endregion

            #region Public Methods

            /// <summary>
            ///     Convert JSON Data
            /// </summary>
            /// <param name="jsondata">JSON Object</param>
            /// <returns>MDR Text Data</returns>
            /// <remarks></remarks>
            public static List<DeviceEventMdrTextData> CnvJsonDataToList(JArray jsondata)
            {
                var result = new List<DeviceEventMdrTextData>();

                foreach (var obj in jsondata)
                {
                    var tmp = new DeviceEventMdrTextData();

                    tmp.PatientSequenceNumber = Utilities.GetJTokenString(obj, "patient_sequence_number");
                    tmp.TextTypeCode = Utilities.GetJTokenString(obj, "text_type_code");
                    tmp.Text = Utilities.GetJTokenString(obj, "text");
                    tmp.MdrTextKey = Utilities.GetJTokenString(obj, "mdr_text_key");
                    tmp.DateReceived = Utilities.GetJTokenString(obj, "date_received");

                    result.Add(tmp);
                }

                return result;
            }

            #endregion
        }
    }
}