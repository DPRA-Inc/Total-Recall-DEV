using System;
using System.Collections.Generic;
using System.Globalization;
using ShopAware.Core.Enumerations;
using ShopAware.Core.Extensions;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        public class SearchResultDrugEvent : SearchResultItemBase
        {
            #region Member Variables

            private bool _IsDeviceEvent = false;
            private bool _IsEvent = true;
            private bool _IsProduct = false;
            private string _Rank = "events";

            #endregion

            #region Properties

            public string Rank
            {
                get
                {
                    return _Rank;
                }
                set
                {
                    _Rank = value;
                }
            }

            public bool IsEvent
            {
                get
                {
                    return _IsEvent;
                }
                set
                {
                    _IsEvent = value;
                }
            }

            public bool IsDeviceEvent
            {
                get
                {
                    return _IsDeviceEvent;
                }
                set
                {
                    _IsDeviceEvent = value;
                }
            }

            public bool IsProduct
            {
                get
                {
                    return _IsProduct;
                }
                set
                {
                    _IsProduct = value;
                }
            }

            public List<string> Description { get; set; }

            public string PatientSex { get; set; }

            public string PatientAge { get; set; }

            public string PatientWeight { get; set; }

            public bool IsPatientDeath { get; set; }

            public List<string> Reactions { get; set; }

            public string ReactionsString
            {
                get
                {
                    return string.Join(", ", Reactions.ToArray());
                }
                set
                {
                }
            }

            public List<string> Seriousness { get; set; }

            public string SeriousnessString
            {
                get
                {
                    return string.Join(", ", Seriousness.ToArray());
                }
                set
                {
                }
            }

            public string PrimarySource { get; set; }

            // ''' <summary>
            // ''' Date that the report was first received by FDA. If this report has multiple versions, this will be the date the first version was received by FDA.
            // ''' </summary>
            // ''' <value></value>
            // ''' <returns></returns>
            // ''' <remarks></remarks>
            //Public Property DateStarted As String

            // ''' <summary>
            // ''' Date that most recent information in the report was received by FDA.
            // ''' </summary>
            // ''' <value></value>
            // ''' <returns></returns>
            // ''' <remarks></remarks>
            //Public Property ReportDate As String
            //    Get
            //        If _reportDate.Length = 0 Then
            //            Return Me.DateStarted
            //        Else
            //            Return _reportDate
            //        End If
            //    End Get
            //    Set(value As String)
            //        _reportDate = value
            //    End Set
            //End Property

            //Public ReadOnly Property SortDate As DateTime
            //    Get
            //        Return DateTime.ParseExact(Me.ReportDate, "ddMMMyyyy", System.Globalization.CultureInfo.InvariantCulture)
            //    End Get
            //End Property

            public string Sender { get; set; }

            public string Receiver { get; set; }

            public List<SearchResultDrugEventItem> DrugItem { get; set; }

            #endregion

            #region Constructors

            public SearchResultDrugEvent()
            {
                // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
                Description = new List<string>();
                Reactions = new List<string>();
                Seriousness = new List<string>();
                DrugItem = new List<SearchResultDrugEventItem>();

                Classification = "Event";
            }

            #endregion

            #region Public Methods

            public static List<SearchResultDrugEvent> ConvertJsonData(List<AdverseDrugEvent> drugEvent)
            {
                var data = new List<SearchResultDrugEvent>();

                foreach (var itm in drugEvent)
                {
                    var obj = new SearchResultDrugEvent();

                    if (!(itm.Patient.PatientDeathDate == null))
                    {
                        obj.IsPatientDeath = true;
                    }

                    obj.PatientSex = Enum.GetName(typeof (PatientSex), itm.Patient.PatientSex);

                    if (!string.IsNullOrEmpty(itm.ReceiveDate))
                    {
                        //.DateStarted = ConvertDateStringToDate(itm.ReceiveDate, "yyyyMMdd")
                        obj.DateStarted = DateTime.ParseExact(itm.ReceiveDate, "yyyyMMdd", CultureInfo.InvariantCulture).
                                                   ToString("ddMMMyyyy");
                    }

                    if (!string.IsNullOrEmpty(itm.ReceiptDate))
                    {
                        //.ReceiptDate = ConvertDateStringToDate(itm.ReceiptDate, "yyyyMMdd")
                        obj.ReportDate = DateTime.ParseExact(itm.ReceiptDate, "yyyyMMdd", CultureInfo.InvariantCulture).
                                                  ToString("ddMMMyyyy");
                    }

                    //Seriousneess
                    if (!string.IsNullOrEmpty(itm.Serious))
                    {
                        switch (itm.Serious)
                        {
                            case "1":
                                obj.Seriousness.Add(
                                    "The adverse event resulted in death, a life threatening condition, hospitalization, disability, congenital anomali, or other serious condition.");
                                break;
                            case "2":
                                obj.Seriousness.Add(
                                    "The adverse event did not result in (death, a life threatening condition, hospitalization, disability, congenital anomali, or other serious condition.)");
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(itm.SeriousnessCongenitalAnomali))
                    {
                        if (itm.SeriousnessCongenitalAnomali == "1")
                        {
                            obj.Seriousness.Add("The adverse event resulted in a congenital anomali");
                        }
                    }

                    if (!string.IsNullOrEmpty(itm.SeriousnessDeath))
                    {
                        if (itm.SeriousnessDeath == "1")
                        {
                            obj.Seriousness.Add("The adverse event resulted in death");
                        }
                    }

                    if (!string.IsNullOrEmpty(itm.SeriousnessDisabling))
                    {
                        if (itm.SeriousnessDisabling == "1")
                        {
                            obj.Seriousness.Add("The adverse event resulted in disability");
                        }
                    }

                    if (!string.IsNullOrEmpty(itm.SeriousnessHospitalization))
                    {
                        if (itm.SeriousnessHospitalization == "1")
                        {
                            obj.Seriousness.Add("The adverse event resulted in a hospitalization");
                        }
                    }

                    if (!string.IsNullOrEmpty(itm.SeriousnessLifeThreatening))
                    {
                        if (itm.SeriousnessLifeThreatening == "1")
                        {
                            obj.Seriousness.Add("The adverse event resulted in a life threatening condition");
                        }
                    }

                    if (!string.IsNullOrEmpty(itm.SeriousnessOther))
                    {
                        if (itm.SeriousnessOther == "1")
                        {
                            obj.Seriousness.Add("The adverse event resulted in some other serious condition");
                        }
                    }

                    //.Sender
                    //.Receiver
                    // .Reactions
                    foreach (var react in itm.Patient.Reaction)
                    {
                        obj.Reactions.Add(react.ReactionMedDrapt);
                    }

                    foreach (var drug in itm.Patient.Drug)
                    {
                        var drugItem = new SearchResultDrugEventItem();

                        foreach (var ofda in drug.OpenFDA.Brand_Name)
                        {
                            drugItem.BrandName.Add(ofda.ToTitleCase());
                        }

                        foreach (var ofda in drug.OpenFDA.Generic_Name)
                        {
                            drugItem.GenericName.Add(ofda.ToTitleCase());
                        }

                        foreach (var ofda in drug.OpenFDA.Manufacturer_Name)
                        {
                            drugItem.ManufacturerName.Add(ofda.ToTitleCase());
                        }

                        foreach (var ofda in drug.OpenFDA.Route)
                        {
                            drugItem.Route.Add(ofda.ToTitleCase());
                        }

                        obj.DrugItem.Add(drugItem);
                    }

                    data.Add(obj);
                }

                return data;
            }

            #endregion
        }
    }
}