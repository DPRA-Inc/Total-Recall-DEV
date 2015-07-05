namespace ShopAware.Core
{
    namespace DataObjects
    {
        public class SearchSummary
        {
            #region Properties

            public string Keyword { get; set; }

            public string State { get; set; }

            public int EventCount { get; set; }

            public int ClassICount { get; set; }

            //  Public Property ClassIDescription As String = GetEnumDescription(Classification.Class_I)

            public int ClassIICount { get; set; }

            //  Public Property ClassIIDescription As String = GetEnumDescription(Classification.Class_II)

            public int ClassIIICount { get; set; }

            #endregion
        }
    }
}