/// <summary>
/// Search Result Map Data Object
/// </summary>
/// <remarks>1 to 1 relation with the state</remarks>

namespace ShopAware.Core
{
    public class SearchResultMapData
    {
        #region Member Variables

        /// <summary>
        ///     Bitwise Icon Set
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        private IconSet _IconSet = 0;

        /// <summary>
        ///     Latitude Value of State
        /// </summary>
        /// <value>string</value>
        /// <returns></returns>
        /// <remarks></remarks>
        private string _Latitude = "0";

        /// <summary>
        ///     Longitude Value of State
        /// </summary>
        /// <value>string</value>
        /// <returns></returns>
        /// <remarks></remarks>
        private string _Longitude = "0";

        private int _Rank = 9;

        /// <summary>
        ///     State 2 character Abbr
        /// </summary>
        /// <value>string</value>
        /// <returns></returns>
        /// <remarks></remarks>
        private string _State = "00";

        /// <summary>
        ///     Tooltip property when placed on the map
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        private string _Tooltip = string.Empty;

        #endregion

        #region Properties

        public string State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }

        public string Tooltip
        {
            get
            {
                return _Tooltip;
            }
            set
            {
                _Tooltip = value;
            }
        }

        public IconSet IconSet
        {
            get
            {
                return _IconSet;
            }
            set
            {
                _IconSet = value;
            }
        }

        /// <summary>
        ///     Image to retrieve based on IconSet
        /// </summary>
        /// <value>string</value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Image
        {
            get
            {
                return string.Format("{0}", IconSet);
            }
        }

        public string Latitude
        {
            get
            {
                return _Latitude;
            }
            set
            {
                _Latitude = value;
            }
        }

        public string Longitude
        {
            get
            {
                return _Longitude;
            }
            set
            {
                _Longitude = value;
            }
        }

        /// <summary>
        ///     Icon Style for OL3.angularjs
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public IconStyle icon
        {
            get
            {
                var ic = new IconStyle();

                ic.src = string.Format(ic.src, IconSet);

                return ic;
            }
        }

        public int Rank
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

        #endregion
    }
}