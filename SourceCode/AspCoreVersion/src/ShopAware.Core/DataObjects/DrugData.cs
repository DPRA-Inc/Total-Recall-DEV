using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ShopAware.Core.Enumerations;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        /// <summary>
        ///     Drug Data Object
        /// </summary>
        /// <remarks></remarks>
        public class DrugData
        {
            #region Properties

            public ActionDrug ActionDrug { get; set; }

            public string DrugCumulativeDosageNumb { get; set; }

            public DrugCumulativeDosageUnit DrugCumulativeDosageUnit { get; set; }

            public string DrugTreatmentDuration { get; set; }

            public DrugTreatmentDurationUnit DrugTreatmentDurationUnit { get; set; }

            public string DrugIntervalDosageUnitNumb { get; set; }

            public DrugIntervalDosageDefinition DrugIntervalDosageDefinition { get; set; }

            public DrugRecurreAdministration DrugRecurreAdministration { get; set; }

            public string DrugAdditional { get; set; }

            public string DrugAuthorizationNumb { get; set; }

            public string DrugDosageForm { get; set; }

            public DrugCharacterization DrugCharacterization { get; set; }

            public string Drugadministrationroute { get; set; }

            public string DrugDosageText { get; set; }

            public string DrugStartDate { get; set; }

            public string DrugEndDate { get; set; }

            public string DrugStartDateFormat { get; set; }

            public string DrugEndDateFormat { get; set; }

            //Public Property drugcharacterization As String

            //Public Property medicinalproduct As String

            public string DrugIndication { get; set; }

            public string MedicinalProduct { get; set; }

            public OpenFdaData OpenFDA { get; set; }

            #endregion

            #region Public Methods

            /// <summary>
            ///     Convert JSON Data
            /// </summary>
            /// <param name="jToken">Token</param>
            /// <returns>List of Drug Data</returns>
            /// <remarks></remarks>
            public static List<DrugData> ConvertJsonData(JToken jToken)
            {
                var data = new List<DrugData>();

                if (Utilities.IsJTokenValid(jToken))
                {
                    foreach (var drug in jToken)
                    {
                        var obj = new DrugData();

                        var actionDrug = (int) obj.ActionDrug;
                        var drugRecurreAdministraton = (int) obj.DrugRecurreAdministration;
                        var drugIntervalDosageDefinition = (int) obj.DrugIntervalDosageDefinition;
                        var drugTreatmentDurationUnit = (int) obj.DrugTreatmentDurationUnit;
                        var drugCumulativeDosageUnit = (int) obj.DrugCumulativeDosageUnit;
                        var drugCharacterization = (int) obj.DrugCharacterization;

                        int.TryParse((Utilities.GetJTokenString(drug, "actiondrug")), out actionDrug);
                        obj.ActionDrug = (ActionDrug) actionDrug;

                        int.TryParse((Utilities.GetJTokenString(drug, "drugrecurreadministration")), out drugRecurreAdministraton);
                        obj.DrugRecurreAdministration = (DrugRecurreAdministration) drugRecurreAdministraton;

                        obj.DrugIntervalDosageUnitNumb = (Utilities.GetJTokenString(drug, "drugintervaldosageunitnumb"));

                        int.TryParse((Utilities.GetJTokenString(drug, "drugintervaldosagedefinition")), out drugIntervalDosageDefinition);
                        obj.DrugIntervalDosageDefinition = (DrugIntervalDosageDefinition) drugIntervalDosageDefinition;

                        obj.DrugTreatmentDuration = (Utilities.GetJTokenString(drug, "drugtreatmentduration"));

                        int.TryParse((Utilities.GetJTokenString(drug, "drugtreatmentdurationunit")), out drugTreatmentDurationUnit);
                        obj.DrugTreatmentDurationUnit = (DrugTreatmentDurationUnit) drugTreatmentDurationUnit;

                        obj.DrugCumulativeDosageNumb = (Utilities.GetJTokenString(drug, "drugcumulativedosagenumb"));

                        int.TryParse((Utilities.GetJTokenString(drug, "drugcumulativedosageunit")), out drugCumulativeDosageUnit);
                        obj.DrugCumulativeDosageUnit = (DrugCumulativeDosageUnit) drugCumulativeDosageUnit;

                        int.TryParse((Utilities.GetJTokenString(drug, "drugcharacterization")), out drugCharacterization);
                        obj.DrugCharacterization = (DrugCharacterization) drugCharacterization;

                        obj.DrugAdditional = (Utilities.GetJTokenString(drug, "drugadditional"));

                        obj.DrugAuthorizationNumb = (Utilities.GetJTokenString(drug, "drugauthorizationnumb"));

                        obj.DrugIndication = (Utilities.GetJTokenString(drug, "drugindication"));
                        obj.MedicinalProduct = (Utilities.GetJTokenString(drug, "medicinalproduct"));
                        obj.Drugadministrationroute = (Utilities.GetJTokenString(drug, "drugadministrationroute"));
                        obj.DrugDosageText = (Utilities.GetJTokenString(drug, "drugdosagetext"));

                        obj.DrugStartDate = (Utilities.GetJTokenString(drug, "drugstartdate"));
                        obj.DrugStartDateFormat = (Utilities.GetJTokenString(drug, "drugstartdateformat"));
                        obj.DrugEndDate = (Utilities.GetJTokenString(drug, "drugenddate"));
                        obj.DrugEndDateFormat = (Utilities.GetJTokenString(drug, "drugenddateformat"));

                        obj.DrugDosageForm = (Utilities.GetJTokenString(drug, "drugdosageform"));

                        obj.OpenFDA = OpenFdaData.ConvertJsonData((JObject) Utilities.GetJTokenObject(drug, "openfda"));

                        data.Add(obj);
                    }
                }

                return data;
            }

            #endregion
        }
    }
}