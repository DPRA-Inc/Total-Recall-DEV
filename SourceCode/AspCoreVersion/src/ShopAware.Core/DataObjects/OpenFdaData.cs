using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        /// <summary>
        ///     Open FDA Data Object
        /// </summary>
        /// <remarks></remarks>
        public class OpenFdaData
        {
            #region Properties

            public List<string> Application_Number { get; set; }

            public List<string> Brand_Name { get; set; }

            public List<string> Generic_Name { get; set; }

            public List<string> Manufacturer_Name { get; set; }

            public List<string> Nui { get; set; }

            public List<string> Package_Ndc { get; set; }

            public List<string> Pharm_Class_Cs { get; set; }

            public List<string> Pharm_Class_Epc { get; set; }

            public List<string> Product_Ndc { get; set; }

            public List<string> Product_Type { get; set; }

            public List<string> Route { get; set; }

            public List<string> Rxcui { get; set; }

            public List<string> Spl_id { get; set; }

            public List<string> Spl_set_id { get; set; }

            public List<string> Substance_name { get; set; }

            public List<string> Unii { get; set; }

            #endregion

            #region Constructors

            public OpenFdaData()
            {
                // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
                Application_Number = new List<string>();
                Brand_Name = new List<string>();
                Generic_Name = new List<string>();
                Manufacturer_Name = new List<string>();
                Route = new List<string>();
            }

            #endregion

            #region Public Methods

            /// <summary>
            ///     Convert JSON Data
            /// </summary>
            /// <param name="jToken">Token</param>
            /// <returns>OpenFdaData Object</returns>
            /// <remarks></remarks>
            public static OpenFdaData ConvertJsonData(JToken jToken)
            {
                var data = new OpenFdaData();

                if (Utilities.IsJTokenValid(jToken))
                {
                    //For Each itm In jToken("application_number")
                    //Next
                    data.Brand_Name = ConvertJTokenToList(jToken["brand_name"]);
                    data.Generic_Name = ConvertJTokenToList(jToken["generic_name"]);
                    data.Manufacturer_Name = ConvertJTokenToList(jToken["manufacturer_name"]);
                    data.Route = ConvertJTokenToList(jToken["route"]);
                    data.Generic_Name = ConvertJTokenToList(jToken["generic_name"]);

                    //For Each itm In jToken("brand_name")
                    //    data.Brand_Name.Add(itm)
                    //Next

                    //For Each itm In jToken("generic_name")
                    //    data.Generic_Name.Add(itm)
                    //Next

                    //For Each itm In jToken("manufacturer_name")
                    //    data.Manufacturer_Name.Add(itm)
                    //Next

                    //For Each itm In jToken("route")
                    //    data.Route.Add(itm)
                    //Next

                    //For Each reaction In jToken

                    //    Dim obj As New ReactionData

                    //    obj.ReactionMedDrapt = reaction("reactionmeddrapt")
                    //    obj.ReactionMeddraversionPt = reaction("reactionmeddraversionpt")
                    //    Integer.TryParse(reaction("reactionoutcome"), obj.ReactionOutcome)

                    //    data.Add(obj)

                    //Next
                }

                return data;
            }

            #endregion

            #region Private Methods

            private static List<string> ConvertJTokenToList(JToken jToken)
            {
                var result = new List<string>();

                if (Utilities.IsJTokenValid(jToken))
                {
                    foreach (var itm in jToken)
                    {
                        result.Add((itm).ToString());
                    }
                }

                return result;
            }

            #endregion
        }
    }
}