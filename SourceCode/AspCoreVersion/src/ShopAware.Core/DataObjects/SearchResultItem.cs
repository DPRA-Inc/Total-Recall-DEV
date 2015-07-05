using System.Collections.Generic;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        public class SearchResultItem : SearchResultItemBase
        {
            #region Member Variables

            private bool _IsEvent = false;
            private bool _IsProduct = true;

            #endregion

            #region Properties

            //Public Property Classification As String

            public string Rank
            {
                get
                {
                    return Classification.Replace(" ", "").
                                          ToLower();
                }
                set
                {
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

            //Public Property DateStarted As String 'recall_initiation_date

            public string Content { get; set; } // reason_for_recall  + code_info

            public string ContentTruncated
            {
                get
                {
                    var value = Content;

                    if (Content.Length > 1000)
                    {
                        value = string.Concat(Content.Substring(0, 999), "...");
                    }

                    return value;
                }
            }

            public string Status { get; set; } // status

            public string DistributionPattern { get; set; } //"distribution_pattern

            public List<string> DistributionList { get; set; } //"distribution_pattern

            public string State { get; set; }

            public string City { get; set; }

            public string ProductDescription { get; set; } //product_description

            public string Voluntary { get; set; } //voluntary_mandated

            public string RecallingFirm { get; set; } //recalling_firm

            public string RecallNumber { get; set; } //recall_number

            public string EventId { get; set; } //event_id

            public string ProductQuantity { get; set; } // product_quantity

            public string Country { get; set; } // country
            //Public Property ReportDate As String ' report_date
            //Public ReadOnly Property SortDate As DateTime
            //    Get
            //        Return DateTime.ParseExact(Me.ReportDate, "ddMMMyyyy", System.Globalization.CultureInfo.InvariantCulture)
            //    End Get
            //End Property
            public string CodeInfo { get; set; } // code_info

            #endregion

            #region Constructors

            public SearchResultItem()
            {
                // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
                DistributionList = new List<string>();
            }

            #endregion
        }
    }
}