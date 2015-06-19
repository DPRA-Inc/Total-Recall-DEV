using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TotalRecall
{
    /// <summary>
    /// Summary description for QuickHandler
    /// </summary>
    public class QuickHandler : IHttpHandler
    {

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            ServiceWrapper.ProcessRequest(context);
        }
    }
}