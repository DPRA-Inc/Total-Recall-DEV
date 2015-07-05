using ShopAware.Core.Attributes;

namespace ShopAware.Tests.Core.Enumerations
{
    public enum Place
    {
        [Description("Home Sweet Home")]
        [Coordinates(1.1,1.123)]
        Home,
        Work
    }
}
