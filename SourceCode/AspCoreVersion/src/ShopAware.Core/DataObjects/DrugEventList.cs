using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShopAware.Core
{
    [Obsolete]
    public class DrugEventList
    {
        #region Properties

        public List<DrugEventItem> Events { get; set; }

        #endregion

        #region Constructors

        public DrugEventList(string jsonString)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            Events = new List<DrugEventItem>();

            var x = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
            var listing = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(x["results"].ToString());

            foreach (var item in listing)
            {
                Events.Add(DrugEventItem.Fill(item));
            }
        }

        #endregion
    }
}