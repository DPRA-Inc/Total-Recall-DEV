using ShopAware.Tests.Core.Enumerations;
using Xunit;

namespace ShopAware.Tests.Core.Utilities
{
    public class Utilities
    {
        [Fact]
        public void GetAttribute_AttributeWithDescription_ReturnDescription()
        {
            Assert.Equal(ShopAware.Core.Utilities.GetEnumDescription(Place.Home), "Home Sweet Home");
            Assert.Equal(ShopAware.Core.Utilities.GetEnumDescription(Place.Work), "Work");
        }

        [Fact]
        public void GetAttribute_AttributeWithCoordinates_ReturnCoordinates()
        {
            Assert.Equal(ShopAware.Core.Utilities.GetEnumCoordinates(Place.Home).Latitude, 1.1);
            Assert.Equal(ShopAware.Core.Utilities.GetEnumCoordinates(Place.Work).Latitude, 0);
        }
    }
}
