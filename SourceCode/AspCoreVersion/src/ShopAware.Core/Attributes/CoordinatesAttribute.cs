using System;

namespace ShopAware.Core.Attributes
{
    public class CoordinatesAttribute : Attribute
    {
        #region Properties

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        #endregion

        #region Constructors

        public CoordinatesAttribute(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        #endregion
    }
}