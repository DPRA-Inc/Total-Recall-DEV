using System;

namespace ShopAware.Core.Attributes
{
    public class DescriptionAttribute : Attribute
    {
        #region Properties

        public string Description { get; set; }

        #endregion

        #region Constructors

        public DescriptionAttribute(string description)
        {
            Description = description;
        }

        #endregion
    }
}