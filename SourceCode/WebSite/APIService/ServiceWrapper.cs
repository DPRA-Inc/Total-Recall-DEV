using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TotalRecall
{
    public static class ServiceWrapper
    {
        public static void ProcessRequest(HttpContext context)
        {
            if (!CheckSecurity()) return;

            // This Disables WebClient Caching...  Since we are using the Web Client 
            // to do all the socket work, it caches responses.  We have to disable this
            // in order to have it keep giving us updated data.
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // Get the SessionID            
            byte[] requestBuffer = GetRequestDetails(context.Request);

            string responseInfo = "";

            try
            {
                if (context.Request.HttpMethod == "POST")
                {
                    responseInfo = HandlePOSTs(context, requestBuffer);
                }
                else
                {
                    string sInputbuffer = System.Text.Encoding.UTF8.GetString(requestBuffer);
                    responseInfo = HandleGETs(context, sInputbuffer);
                }

                if (responseInfo != null)
                {
                    object obj = JsonConvert.DeserializeObject(responseInfo);

                    var ob = obj as JObject;

                    if (responseInfo.StartsWith("{\"WebObjectType\":\"TotalRecall.SiteExceptionInfo\""))
                    {
                        var code = ob.GetValue("StatusCode");
                        context.Response.StatusCode = code.Value<int>();
                    }

                    context.Response.Write(obj);
                }

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                context.Response.Write("ERROR: " + ex.Message);
            }

        }

        private static byte[] GetRequestDetails(HttpRequest request)
        {
            byte[] inputbuffer = null;

            int length = Convert.ToInt32(request.InputStream.Length);

            if (length > 0)
            {
                inputbuffer = new byte[length];
                int read = request.InputStream.Read(inputbuffer, 0, length);
            }

            return inputbuffer;
        }

        private static bool CheckSecurity()
        {
            // Future, maybe we have a login page?  Or when operating out of https?
            return true;
        }

        private static string HandleGETs(HttpContext context, string request)
        {
            List<string> param = new List<string>();

            switch (context.Request.QueryString["Command"].ToUpper())
            {
                case "LOGIN":
 
                case "GETWEBSETTINGS":
 
                case "KEEPALIVE":
 
                case "GETREPORTDATA":
 
                case "GETTERMSANDCONDITIONS":
                
                case "GETPRIVACYSTATEMENT":
                
                default:
                    return null;

            }

        }

        private static string HandlePOSTs(HttpContext context, byte[] requestBuffer)
        {

            string response = string.Empty;

            //if (context.Request.QueryString["Method"].ToUpper() == "SENDMAILFORGOTPASSWORD")
            //{
            //    string user = context.Request.QueryString["user"];

            //    return ForgotPassword(user).ToString();
            //}

            switch (context.Request.QueryString["Command"].ToUpper())
            {
                case "REGISTER":
                    //return ServiceWrapper.Register(requestBuffer);

                //case "EXECUTEBUSINESS":
                //    return ServiceWrapper.ExecuteBusiness(context, requestBuffer);

                //case "EXECUTEBUSINESSMULTIPART":
                //    return ServiceWrapper.ExecuteBusinessMultipart(context, requestBuffer);

                default:
                    return null;

            }

        }

    }
}