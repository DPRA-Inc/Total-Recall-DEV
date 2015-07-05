using Newtonsoft.Json.Linq;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        /// <summary>
        ///     Meta Results Data Object
        /// </summary>
        /// <remarks></remarks>
        public class MetaResults
        {
            #region Member Variables

            private int _Limit = 0;
            private int _Skip = 0;
            private int _Total = 0;

            #endregion

            #region Properties

            public int Limit
            {
                get
                {
                    return _Limit;
                }
                set
                {
                    _Limit = value;
                }
            }

            public int Skip
            {
                get
                {
                    return _Skip;
                }
                set
                {
                    _Skip = value;
                }
            }

            public int Total
            {
                get
                {
                    return _Total;
                }
                set
                {
                    _Total = value;
                }
            }

            #endregion

            #region Public Methods

            /// <summary>
            ///     Converts JSON Data
            /// </summary>
            /// <param name="jsondata">JSON Data as String</param>
            /// <returns>Meta Results Data Object</returns>
            /// <remarks></remarks>
            public static MetaResults CnvJsonData(string jsondata)
            {
                MetaResults result = null;

                if (string.IsNullOrEmpty(jsondata))
                {
                    result = new MetaResults();
                }
                else
                {
                    var jo = JObject.Parse(jsondata);
                    result = CnvJsonData(jo);
                }

                return result;
            }

            /// <summary>
            ///     Converts JSON Data
            /// </summary>
            /// <param name="jsondata">JSON Data as Object</param>
            /// <returns>Meta Results Data Object</returns>
            /// <remarks></remarks>
            public static MetaResults CnvJsonData(JObject jsondata)
            {
                var metaData = new MetaResults();
                var obj = jsondata.GetValue("meta");

                if (obj["results"] != null)
                {
                    metaData.Limit = (int) obj["results"]["limit"];
                    metaData.Skip = (int) obj["results"]["skip"];
                    metaData.Total = (int) obj["results"]["total"];
                }

                return metaData;
            }

            #endregion
        }
    }
}