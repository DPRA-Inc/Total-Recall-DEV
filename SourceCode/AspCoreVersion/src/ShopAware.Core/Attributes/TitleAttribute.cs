using System;

namespace ShopAware.Core.Attributes
{
    public class TitleAttribute : Attribute
    {
        #region Properties

        public string Title { get; set; }

        #endregion

        #region Constructors

        public TitleAttribute(string description)
        {
            Title = description;
        }

        #endregion
    }
}