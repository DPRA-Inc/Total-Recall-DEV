using System.Collections.Generic;

namespace ShopAware.Core
{
    public class FeatureObject
    {
        #region Properties

        public string Type { get; set; }

        public FeaturePropertyObject Properties { get; set; }

        public GeometryObject Teometery { get; set; }

        #endregion
    }

    public class FeaturePropertyObject
    {
        #region Properties

        public string GEO_ID { get; set; }

        public string STATE { get; set; }

        public string NAME { get; set; }

        public string LSAD { get; set; }

        public double CENSUSAREA { get; set; }

        #endregion
    }

    public class GeometryObject
    {
        #region Properties

        public string type { get; set; }

        public List<LatLon> coordinates { get; set; }

        #endregion
    }

    public class LatLon
    {
        #region Properties

        public List<double> loc { get; set; }

        #endregion
    }
}