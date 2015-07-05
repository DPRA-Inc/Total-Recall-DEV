using System.Collections.Generic;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        public class FdaResult
        {
            #region Properties

            public string Keyword { get; set; }

            public List<SearchResultItemBase> Results { get; set; }

            public List<SearchResultMapData> MapObjects { get; set; }

            public ReportData GraphObjects { get; set; }

            #endregion
        }
    }
}