using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TotalRecall.BusinessObjects
{
    public abstract class WebBusinessInfo
    {

        /// <summary>
        /// Used when you want to Redirect The Web To a different page.
        /// </summary>
        public string WebRedirectTo { get; set; }

        /// <summary>
        /// Used internally for JSON to Object Serialization.
        /// </summary>
        public string WebObjectType
        {
            get
            {
                return this.GetType().ToString();
            }
            set { }
        }
    }

}