using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        /// <summary>
        ///     Recall Result
        /// </summary>
        /// <remarks></remarks>
        public class ResultRecall
        {
            #region Member Variables

            private string _KeyWord = string.Empty;

            #endregion

            #region Properties

            public string KeyWord
            {
                get
                {
                    return _KeyWord;
                }
                set
                {
                    _KeyWord = value;
                }
            }

            public string City { get; set; }

            public string Classification { get; set; }

            public string Code_info { get; set; }

            public string Country { get; set; }

            public string Distribution_Pattern { get; set; }

            public string Event_Id { get; set; }

            public string Initial_Firm_Notification { get; set; }

            public object Openfda { get; set; }

            public string Product_Description { get; set; }

            public string Product_Quantity { get; set; }

            public string Product_Type { get; set; }

            public string Reason_For_Recall { get; set; }

            public string Recall_Initiation_Date { get; set; }

            public string Recall_Number { get; set; }

            public string Recalling_Firm { get; set; }

            public string Report_Date { get; set; }

            public string State { get; set; }

            public string Status { get; set; }

            public string Voluntary_Mandated { get; set; }

            #endregion

            #region Public Methods

            /// <summary>
            ///     Converts JSON Data to List
            /// </summary>
            /// <param name="jsondata">JSON Object Data</param>
            /// <returns>List of Recall Data</returns>
            /// <remarks></remarks>
            public static List<ResultRecall> CnvJsonDataToList(JObject jsondata)
            {
                var result = new List<ResultRecall>();

                foreach (var obj in jsondata.GetValue("results"))
                {
                    var tmp = new ResultRecall();

                    tmp.City = (obj["city"]).ToString();
                    tmp.Classification = (obj["classification"]).ToString();
                    tmp.Code_info = (obj["code_info"]).ToString();
                    tmp.Country = (obj["country"]).ToString();
                    tmp.Distribution_Pattern = (obj["distribution_pattern"]).ToString();
                    tmp.Event_Id = (obj["event_id"]).ToString();
                    tmp.Initial_Firm_Notification = (obj["initial_firm_notification"]).ToString();
                    //tmp.openfda = zz("city")
                    tmp.Product_Description = (obj["product_description"]).ToString();
                    tmp.Product_Quantity = (obj["product_quantity"]).ToString();
                    tmp.Product_Type = (obj["product_type"]).ToString();
                    tmp.Reason_For_Recall = (obj["reason_for_recall"]).ToString();
                    tmp.Recall_Initiation_Date = (obj["recall_initiation_date"]).ToString();
                    tmp.Recall_Number = (obj["recall_number"]).ToString();
                    tmp.Recalling_Firm = (obj["recalling_firm"]).ToString();
                    tmp.Report_Date = (obj["report_date"]).ToString();
                    tmp.State = (obj["state"]).ToString();
                    tmp.Status = (obj["status"]).ToString();
                    tmp.Voluntary_Mandated = (obj["voluntary_mandated"]).ToString();

                    result.Add(tmp);
                }

                return result;
            }

            /// <summary>
            ///     Converts JSON Data to List
            /// </summary>
            /// <param name="jsondata">JSON String Data</param>
            /// <returns>List of Recall Data</returns>
            /// <remarks></remarks>
            public static List<ResultRecall> CnvJsonDataToList(string jsondata)
            {
                var jo = JObject.Parse(jsondata);

                return CnvJsonDataToList(jo);
            }

            public override string ToString()
            {
                return string.Format("{0} - {1}", Recall_Initiation_Date, Classification);
            }

            #endregion
        }
    }
}