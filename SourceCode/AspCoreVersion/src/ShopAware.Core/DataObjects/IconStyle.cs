namespace ShopAware.Core
{
    public class IconStyle
    {
        #region Member Variables

        /// <summary>
        ///     Anchor location of icon
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <summary>
        ///     Anchor X units
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        private string _anchorXUnits = "fraction";

        /// <summary>
        ///     Anchor Y units
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        private string _anchorYUnits = "fraction";

        /// <summary>
        ///     Opacity of icon
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        private double _opacity = (double) 0.85M;

        /// <summary>
        ///     Image location
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        private string _src = "img/mapIcon/{0}.png";

        #endregion

        #region Properties

        public string anchorXUnits
        {
            get
            {
                return _anchorXUnits;
            }
            set
            {
                _anchorXUnits = value;
            }
        }

        public string anchorYUnits
        {
            get
            {
                return _anchorYUnits;
            }
            set
            {
                _anchorYUnits = value;
            }
        }

        public double opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                _opacity = value;
            }
        }

        public string src
        {
            get
            {
                return _src;
            }
            set
            {
                _src = value;
            }
        }

        #endregion
    }
}