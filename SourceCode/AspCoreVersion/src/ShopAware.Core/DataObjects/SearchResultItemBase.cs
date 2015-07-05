using System;
using System.Globalization;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        public class SearchResultItemBase
        {
            #region Member Variables

            private string _Classification = string.Empty;
            private string _reportDate = string.Empty;

            #endregion

            #region Properties

            public string Classification
            {
                get
                {
                    return _Classification;
                }
                set
                {
                    _Classification = value;
                }
            }

            public string DateStarted { get; set; }

            /// <summary>
            ///     Date that most recent information in the report was received by FDA.
            /// </summary>
            /// <value></value>
            /// <returns></returns>
            /// <remarks></remarks>
            public string ReportDate
            {
                get
                {
                    if (_reportDate.Length == 0)
                    {
                        return DateStarted;
                    }
                    else
                    {
                        return _reportDate;
                    }
                }
                set
                {
                    _reportDate = value;
                }
            }

            public DateTime SortDate
            {
                get
                {
                    return DateTime.ParseExact(ReportDate, "ddMMMyyyy", CultureInfo.InvariantCulture);
                }
            }

            #endregion
        }
    }
}