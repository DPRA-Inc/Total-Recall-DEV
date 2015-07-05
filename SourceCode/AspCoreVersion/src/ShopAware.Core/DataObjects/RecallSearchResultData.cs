using System.Collections.Generic;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        /// <summary>
        ///     Recall Search Results
        /// </summary>
        /// <remarks></remarks>
        public class RecallSearchResultData
        {
            #region Member Variables

            private bool _IsNationWide = false;

            #endregion

            #region Properties

            public string KeyWord { get; set; }

            public int Count { get; set; }

            public string Type { get; set; }

            public string Classification { get; set; }

            public string ProductDescription { get; set; }

            public string ReasonForRecall { get; set; }

            public HashSet<string> Regions { get; set; }

            public bool IsNationWide
            {
                get
                {
                    return _IsNationWide;
                }
                set
                {
                    _IsNationWide = value;
                }
            }

            #endregion

            #region Constructors

            public RecallSearchResultData()
            {
                // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
                Regions = new HashSet<string>();
            }

            #endregion
        }
    }
}