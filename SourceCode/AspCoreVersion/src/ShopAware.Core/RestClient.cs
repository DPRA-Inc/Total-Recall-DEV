using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopAware.Core
{
    public class RestClient : IRestClient
    {
        #region Member Variables

        private List<string> _apiKeyList = new List<string>(new[]
                                                            {
                                                                "XJbQx98xnJ1c2UQ9PmT31fhrjfifhNZHrxkJqxpN",
                                                                "If79nXLMcUgAMEOKabW2xZwZWGduzxH6exXNyWXY",
                                                                "2wVtRR9NZp16SiQPFCzPg2F1yPybEzmfCYtNZt7H",
                                                                "VbLkhcEM5IFDA4DE22yVEfNp2EVDvhP2nPZLdNME",
                                                                "0GhxVK46VsvckW4PqjOhYpVHbhH1wdd7ylTOwbdp"
                                                            });

        #endregion

        #region Properties

        private string OpenFdaWebApiKey { get; set; }

        #endregion

        #region Interface Implementations

        // https://api.fda.gov/food/enforcement.json?&search=((reason_for_recall:nuts)+(product_description:nuts))+AND+(report_date:[20140705+TO+20150706]+recall_initiation_date:[20140705+TO+20150706])+AND+status=ongoing&count=classification.exact
        // https://api.fda.gov/FoodRecall.json?&search=((reason_for_recall:nuts)+(product_description:nuts))+AND+(report_date:[20140705+TO+20150706]+recall_initiation_date:[20140705+TO+20150706])+AND+status=ongoing&count=classification.exact
        public async Task<string> Execute(string url)
        {
            var result = string.Empty;
            var urlWithKey = url.Replace("?", string.Format("?api_key={0}", _apiKeyList));

            try
            {
                if (!string.IsNullOrEmpty(OpenFdaWebApiKey))
                {
                    if (!url.Contains("?api_key="))
                    {
                        url = url.Replace("?", string.Format("?api_key={0}", OpenFdaWebApiKey));
                    }
                }

                using (var webClient = new HttpClient())
                {
                    webClient.DefaultRequestHeaders.Clear();

                    var getStringTask = webClient.GetStringAsync(url);
                    result = await getStringTask;
                }
                ;
            }
            catch (HttpRequestException ex)
            {
                var throwException = !ex.Message.Contains("404");

                if (throwException)
                {
                    throw;
                }
            }

            return result;
        }

        #endregion
    }
}