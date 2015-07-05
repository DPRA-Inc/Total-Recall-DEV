using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShopAware.Core
{
    [Obsolete]
    public class DrugEventItem
    {
        #region Member Variables

        public Dictionary<string, object> AllData = new Dictionary<string, object>();

        #endregion

        #region Properties

        public string SafetyReportID { get; set; }

        public string ReceivedDateString { get; set; }

        public string SenderOrganization { get; set; }

        public string PatientSex { get; set; }

        public string PatientAge { get; set; }

        public string PatientWeight { get; set; }

        public List<string> PatientReactions { get; set; }

        public string DrugStartDate { get; set; }

        public string DrugEndDate { get; set; }

        public string Doseage { get; set; }

        public List<string> Manufactures { get; set; }

        public List<string> BrandNames { get; set; }

        public List<string> GenericNames { get; set; }

        #endregion

        #region Constructors

        public DrugEventItem()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            PatientReactions = new List<string>();
            Manufactures = new List<string>();
            BrandNames = new List<string>();
            GenericNames = new List<string>();
        }

        private DrugEventItem(Dictionary<string, object> jsonItem)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            PatientReactions = new List<string>();
            Manufactures = new List<string>();
            BrandNames = new List<string>();
            GenericNames = new List<string>();

            Fetch(jsonItem);
        }

        #endregion

        #region Public Methods

        public static DrugEventItem Fill(Dictionary<string, object> item)
        {
            return new DrugEventItem(item);
        }

        #endregion

        #region Private Methods

        private void CheckKeys(string key, object value)
        {
            switch (key)
            {
                case "safetyreportid":
                    SafetyReportID = value.ToString();
                    break;
                case "receivedate":
                    ReceivedDateString = value.ToString();
                    break;
                case "senderorganization":
                    SenderOrganization = value.ToString();
                    break;
                case "patientonsetage":
                    PatientAge = value.ToString();
                    break;
                case "patientweight":
                    PatientWeight = value.ToString();
                    break;
                //            Case "medicinalproduct" : Drug = value.ToString
                case "manufacturer_name":
                    Manufactures = (List<string>) value;
                    break;
                case "brand_name":
                    BrandNames = (List<string>) value;
                    break;
                case "drugstartdate":
                    DrugStartDate = value.ToString();
                    break;
                case "drugenddate":
                    DrugEndDate = value.ToString();
                    break;
            }
        }

        private void Fetch(Dictionary<string, object> jsonItem)
        {
            foreach (var item in jsonItem)
            {
                var key = item.Key;
                object value = null;

                if (item.Value != null)
                {
                    var checkValue = item.Value.ToString();

                    if (checkValue.StartsWith("{"))
                    {
                        value = MakeDictionaryObject(item.Value.ToString());
                    }
                    else if (checkValue.StartsWith("["))
                    {
                        value = MakeListObject(item.Value.ToString());
                    }
                }

                CheckKeys(key, value);
                AllData.Add(key, value);
            }
        }

        private Dictionary<string, object> MakeDictionaryObject(string data)
        {
            var completeObject = new Dictionary<string, object>();
            var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);

            foreach (var item in obj)
            {
                var key = item.Key;
                object value = null;

                if (item.Value != null)
                {
                    var checkValue = item.Value.ToString();

                    if (checkValue.StartsWith("{"))
                    {
                        value = MakeDictionaryObject(item.Value.ToString());
                    }
                    else if (checkValue.StartsWith("["))
                    {
                        value = MakeListObject(item.Value.ToString());
                    }
                }

                completeObject.Add(key, value);
            }

            return completeObject;
        }

        private List<object> MakeListObject(string data)
        {
            var completeObject = new List<object>();
            var obj = (JArray) (JsonConvert.DeserializeObject(data));

            for (var i = 0; i <= obj.Count - 1; i++)
            {
                var item = obj[i].ToString();
                object value = null;

                if (item.StartsWith("{"))
                {
                    value = MakeDictionaryObject(item.ToString());
                }
                else if (item.StartsWith("["))
                {
                    value = MakeListObject(item.ToString());
                }

                completeObject.Add(value);
            }

            return completeObject;
        }

        #endregion
    }
}