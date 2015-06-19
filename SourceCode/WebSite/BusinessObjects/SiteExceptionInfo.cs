using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TotalRecall.BusinessObjects
{
    public class SiteExceptionInfo : WebBusinessInfo
    {
        public string Type { get; set; }

        public ErrorTypes ErrorType { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorDetails { get; set; }

        public List<ValidationError> ValidationErrors { get; set; }

        public int StatusCode { get; set; }

        public SiteExceptionInfo()
        {
            this.ErrorType = ErrorTypes.General;
            this.Type = "SiteException";
        }

        public static SiteExceptionInfo Fill(string errorMessage, ErrorTypes errorType)
        {
            SiteExceptionInfo x = new SiteExceptionInfo();
            x.ErrorMessage = errorMessage;
            x.ErrorType = errorType;

            return x;
        }

    }

    public class ValidationError : WebBusinessInfo
    {
        public string PropertyName { get; set; }

        public string Error { get; set; }
    }

    public enum ErrorTypes : byte
    {
        Error = 0,
        General = 1,
        Fatal = 2,
        Validation = 3,
        Warning = 4
    }
}
