using System.Collections.Generic;

namespace ShopAware.Core.DataObjects
{
    public class ReportData
    {
        #region Properties

        public List<string> Labels { get; set; }

        public List<int> Data1 { get; set; }

        public List<int> Data2 { get; set; }

        public List<int> Data3 { get; set; }

        public List<int> DataE { get; set; }

        #endregion

        #region Constructors

        public ReportData()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            Labels = new List<string>();
            Data1 = new List<int>();
            Data2 = new List<int>();
            Data3 = new List<int>();
            DataE = new List<int>();
        }

        #endregion
    }
}