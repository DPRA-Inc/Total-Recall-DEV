using ShopAware.Core.Attributes;

namespace ShopAware.Core.Enumerations
{
    /// <summary>
    ///     Recall Classification Levels
    /// </summary>
    /// <remarks></remarks>
    public enum Classification
    {
        [Title("Class I")] [Description("Dangerous or defective products that predictably could cause serious health problems or death")] ClassI,

        [Title("Class II")] [Description("Products that might cause a temporary health problem, or pose only a slight threat of a serious nature")] ClassIi,

        [Title("Class III")] [Description("Products that are unlikely to cause any adverse health reaction, but that violate FDA labeling or manufacturing laws")] ClassIii
    }
}