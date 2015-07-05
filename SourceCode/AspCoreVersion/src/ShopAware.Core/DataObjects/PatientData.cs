using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using ShopAware.Core.Enumerations;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        /// <summary>
        ///     Patient Data Object
        /// </summary>
        /// <remarks></remarks>
        public class PatientData
        {
            #region Member Variables

            private DateTime _PatientDeathDate = default(DateTime);

            #endregion

            #region Properties

            public PatientSex PatientSex { get; set; }

            //The age of the patient when the event first occured
            public string PatientOnSetAge { get; set; }

            //The unit of measurement for the patient.patientonsetage field
            public string PatientOnSetAgeUnit { get; set; }

            //The weight of the patient expressed in kilograms.
            public string PatientWeight { get; set; }

            public DateTime PatientDeathDate
            {
                get
                {
                    return _PatientDeathDate;
                }
                set
                {
                    _PatientDeathDate = value;
                }
            }

            //Public Property patientdeath As List(Of String)
            //patient.patientdeath
            //patient.patientdeath.patientdeathdate
            //patient.patientdeath.patientdeathdateformat

            public List<DrugData> Drug { get; set; }

            public List<ReactionData> Reaction { get; set; }

            #endregion

            #region Public Methods

            /// <summary>
            ///     Convert JSON Date
            /// </summary>
            /// <param name="jToken">Token</param>
            /// <returns>Patient Data Object</returns>
            /// <remarks></remarks>
            public static PatientData ConvertJsonDate(JToken jToken)
            {
                var data = new PatientData();

                if (Utilities.IsJTokenValid(jToken))
                {
                    data.PatientOnSetAge = (jToken["patientonsetage"]).ToString();
                    data.PatientOnSetAgeUnit = (jToken["patientonsetageunit"]).ToString();
                    //        patient.patientonsetageunit()
                    //string
                    //The unit of measurement for the patient.patientonsetage field.
                    //800 = Decade
                    //801 = Year
                    //802 = Month
                    //803 = Week
                    //804 = Day
                    //805 = Hour

                    var patientSex = (int) data.PatientSex;

                    int.TryParse((jToken["patientsex"]).ToString(), out patientSex);
                    data.PatientSex = (PatientSex) patientSex;

                    //data.patientsex = jToken("patientsex")
                    //        patient.patientsex()
                    //string
                    //The sex of the patient.
                    //0 = Unknown
                    //1 = Male
                    //2 = Female

                    data.PatientWeight = Utilities.GetJTokenString(jToken, "patientweight"); // KiloGrams

                    if (Utilities.IsJTokenValid(Utilities.GetJTokenString(jToken, "patientdeath")))
                    {
                        //data.PatientDeathDate = ConvertDateStringToDate(jToken("patientdeath")("patientdeathdate"), "yyyyMMdd")
                        data.PatientDeathDate = DateTime.ParseExact((jToken["patientdeath"]["patientdeathdate"]).ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    }

                    data.Drug = DrugData.ConvertJsonData(jToken["drug"]);
                    data.Reaction = ReactionData.ConvertJsonData(jToken["reaction"]);
                }

                return data;
            }

            #endregion
        }
    }
}