using System.Collections.Generic;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        public class SearchResultDrugEventItem
        {
            #region Properties

            public List<string> BrandName { get; set; }

            public List<string> GenericName { get; set; }

            public List<string> ManufacturerName { get; set; }

            public List<string> Route { get; set; }

            public string BrandNamesString
            {
                get
                {
                    return string.Join(", ", BrandName.ToArray());
                }
                set
                {
                }
            }

            public string GenericNamesString
            {
                get
                {
                    return string.Join(", ", GenericName.ToArray());
                }
                set
                {
                }
            }

            #endregion

            #region Constructors

            public SearchResultDrugEventItem()
            {
                // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
                BrandName = new List<string>();
                GenericName = new List<string>();
                ManufacturerName = new List<string>();
                Route = new List<string>();
            }

            #endregion
        }
    }
}