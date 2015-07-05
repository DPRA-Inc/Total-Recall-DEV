using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using ShopAware.Core.DataObjects;
using ShopAware.Core.Enumerations;

namespace ShopAware.Core
{
    /// <summary>
    ///     Open FDA
    /// </summary>
    /// <remarks></remarks>
    public class OpenFda
    {
        #region Member Variables

        private static string _HostUrl = "https://api.fda.gov/";
        private string _count;
        private OpenFdaApiEndPoints _endPointType;
        private HashSet<string> _keyWords = new HashSet<string>();
        private int _limit = 0;
        private JObject _meta;
        private IRestClient _restClient;
        private object _results; //JObject
        private string _search;

        #endregion

        #region Properties

        public static string HostUrl
        {
            get
            {
                return _HostUrl;
            }
            set
            {
                _HostUrl = value;
            }
        }

        #endregion

        #region Constructors

        public OpenFda()
        {
            _restClient = new RestClient();
        }

        public OpenFda(IRestClient restClient)
        {
            _restClient = restClient;
        }

        #endregion

        #region Public Methods

        public void AddCountField(string field)
        {
            _count = field;
        }

        public void AddSearchFilter(OpenFdaApiEndPoints endPointType, string endpointField, string keyWord, FilterCompairType operationCompairType)
        {
            keyWord = RemoveSpecialCharactersFromKeyword(keyWord);
            keyWord = keyWord.Replace(" ", "+");

            if (keyWord.Contains("+"))
            {
                keyWord = "\"" + keyWord + "\"";
            }

            var param = string.Format("{0}:({1})", endpointField, keyWord);

            if (!string.IsNullOrEmpty(_search))
            {
                if (operationCompairType == FilterCompairType.Or)
                {
                    _search += "+";
                }
                else
                {
                    _search += "+AND+";
                }
            }

            _search += param;
        }

        public string AddSearchFilter(OpenFdaApiEndPoints endpointType,
                                      FdaFilterTypes type,
                                      List<string> filters,
                                      FilterCompairType operationCompairType = FilterCompairType.Or)
        {
            // Add Filter to KeyWord List
            var keyword = string.Empty;
            var keywordToRemove = new string[]
                                  {
                                      "null",
                                      "all"
                                  };

            for (var indx = 0; indx <= filters.Count - 1; indx++)
            {
                filters[indx] = RemoveSpecialCharactersFromKeyword(filters[indx]);

                if (keywordToRemove.Contains(filters[indx].ToLower()))
                {
                    filters[indx] = string.Empty;
                }
            }

            var tmpFilters = (from el in filters
                              where el.Length > 0
                              select el).ToList();

            if (!(tmpFilters.Count == filters.Count))
            {
                filters.Clear();
                filters.AddRange(tmpFilters);
            }

            if (filters.Count == 0)
            {
                return string.Empty;
            }

            foreach (var itm in filters)
            {
                keyword += itm.ToLower() + ",";
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Substring(0, keyword.Length - 1);
            }

            if (!_keyWords.Contains(keyword))
            {
                _keyWords.Add(keyword);
            }

            var param = string.Empty;

            var tmp = string.Empty;

            switch (type)
            {
                case FdaFilterTypes.Date:

                    if (filters.Count == 1)
                    {
                        //Dim tmpDate As DateTime = ConvertDateStringToDate(filters(0), "yyyyMMdd")
                        var tmpDate = DateTime.ParseExact(filters[0], "yyyyMMdd", CultureInfo.InvariantCulture);

                        tmp = string.Format("{0:yyyyMMdd}", tmpDate); //tmpDate.ToString("yyyyMMdd")
                    }
                    else
                    {
                        Nullable<DateTime> minDate = null;
                        Nullable<DateTime> maxDate = null;

                        foreach (var itm in filters)
                        {
                            //Dim itmDate As DateTime = ConvertDateStringToDate(itm, "yyyyMMdd")
                            var itmDate = DateTime.ParseExact(itm, "yyyyMMdd", CultureInfo.InvariantCulture);

                            if (itmDate < minDate || minDate == null)
                            {
                                minDate = itmDate;
                            }

                            if (itmDate > maxDate || maxDate == null)
                            {
                                maxDate = itmDate;
                            }
                        }

                        var dtMin = string.Format("{0:yyyyMMdd}", minDate); //minDate.ToString("yyyyMMdd")
                        var dtMax = string.Format("{0:yyyyMMdd}", maxDate); // maxDate.ToString("yyyyMMdd")

                        tmp = string.Format("[{0}+TO+{1}]", dtMin, dtMax);
                    }
                    break;

                default:

                    var tmpItm = "";

                    foreach (var itm in filters)
                    {
                        tmpItm = itm.Replace(" ", "+");

                        if (tmpItm.Contains("+"))
                        {
                            tmpItm = "\"" + tmpItm + "\"";
                        }

                        tmp += tmpItm + "+";
                    }

                    if (!string.IsNullOrEmpty(tmp))
                    {
                        tmp = tmp.Substring(0, tmp.Length - 1);
                    }
                    break;
            }

            switch (endpointType)
            {
                case OpenFdaApiEndPoints.DrugEvent:

                    switch (type)
                    {
                        case FdaFilterTypes.Date:
                            param = "(";
                            param += "receivedate:" + tmp + "";
                            param += ")";
                            break;

                        case FdaFilterTypes.DrugEventDrugName:

                            param = "(patient.drug.openfda.substance_name:" + tmp;
                            param += "+";
                            param += "patient.drug.openfda.brand_name:" + tmp;
                            param += "+";
                            param += "patient.drug.openfda.generic_name:" + tmp;
                            param += "+";
                            param += "patient.drug.medicinalproduct:" + tmp + ")";
                            break;
                    }
                    break;

                case OpenFdaApiEndPoints.DeviceRecall:
                case OpenFdaApiEndPoints.DrugRecall:
                case OpenFdaApiEndPoints.FoodRecall:

                    switch (type)
                    {
                        case FdaFilterTypes.Region:

                            var tmpEnum = (States) (Enum.Parse(typeof (States), tmp));
                            param = "(state:(" + tmp + ")";
                            param += "+";
                            //param += "distribution_pattern:(Nationwide+" & tmp & "))" ' TODO:  Need the State NAME + GetEnumDescription(tmpEnum)
                            param += string.Format("distribution_pattern:(Nationwide+{0}+{1}))", tmp, Enum.GetName(typeof (States), tmpEnum));
                            // TODO:  Need the State NAME + GetEnumDescription(tmpEnum)
                            break;

                        case FdaFilterTypes.RecallReason:

                            var keywordList = tmp.Replace("\"", string.Empty).
                                                  Split('+');

                            param = "((";
                            foreach (var itm in keywordList)
                            {
                                param += string.Format("reason_for_recall:{0}+AND+", itm);
                            }
                            //Remove the Ending +AND+
                            param = param.Substring(0, param.Length - 5);
                            param += ")+(";
                            foreach (var itm in keywordList)
                            {
                                param += string.Format("product_description:{0}+AND+", itm);
                            }
                            //Remove the Ending +AND+
                            param = param.Substring(0, param.Length - 5);
                            param += "))";
                            break;

                        case FdaFilterTypes.Date:
                            param = "(";
                            param += "report_date:" + tmp + "";
                            param += "+";
                            param += "recall_initiation_date:" + tmp + "";
                            param += ")";
                            break;
                    }
                    break;

                case OpenFdaApiEndPoints.DeviceEvent:

                    switch (type)
                    {
                        case FdaFilterTypes.Date:
                            param = "(";
                            param += "date_of_event:" + tmp + "";
                            param += ")";
                            break;

                        case FdaFilterTypes.DeviceEventDescription:
                            param = "(device.brand_name:" + tmp;
                            param += "+";
                            param += "device.generic_name:" + tmp;
                            param += "+";
                            param += "mdr_text.text:" + tmp + ")";
                            break;
                    }
                    break;

                case OpenFdaApiEndPoints.DrugLabel:
                    break;
//TBD

                default:
                    break;
// do nothing
            }

            if (!string.IsNullOrEmpty(param))
            {
                if (!string.IsNullOrEmpty(_search))
                {
                    if (operationCompairType == FilterCompairType.Or)
                    {
                        _search += "+";
                    }
                    else
                    {
                        _search += "+AND+";
                    }
                }

                _search += param;
            }

            return param;
        }

        /// <summary>
        ///     Builds the url string for the endpoint, using the
        /// </summary>
        /// <param name="endPointType"></param>
        /// <param name="limit"></param>
        /// <param name="ongoingOnly"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string BuildUrl(OpenFdaApiEndPoints endPointType, int limit = 0, bool ongoingOnly = true)
        {
            var uri = default(Uri);
            var sb = new StringBuilder();
            var hostUrl = GetOpenFdaEndPoint(endPointType);

            sb.Append(hostUrl);

            if (!string.IsNullOrEmpty(_search))
            {
                sb.Append("&search=");
                sb.Append(_search);

                if (ongoingOnly)
                {
                    if (!string.IsNullOrEmpty(_search))
                    {
                        sb.Append("+AND");
                    }

                    sb.Append("+status=ongoing");
                }
            }

            if (!string.IsNullOrEmpty(_count))
            {
                sb.Append("&count=");
                sb.Append(_count);
            }
            else
            {
                // NOTE: if count is passed then Limit does not do anything
                // so if Count then Limit is ignored/does nothing
                _limit = limit;

                if (_limit < 0)
                {
                    _limit = 0;
                }
                else if (_limit > 100)
                {
                    _limit = 100;
                }

                if (_limit > 1)
                {
                    sb.Append("&limit=");
                    sb.Append(_limit);
                }
            }

            uri = new Uri(sb.ToString());

            return uri.ToString();
        }

        public string Execute(string url)
        {
            var resultTask = _restClient.Execute(url);
            var result = resultTask.Result.ToString();

            _meta = new JObject();

            if (!string.IsNullOrEmpty(result))
            {
                var jo = JObject.Parse(result);

                _meta = (JObject) (jo.GetValue("meta")); // If the property doesn't exist, it returns null
                _results = jo["results"]; // If the property doesn't exist, it returns null
            }

            return result;
        }

        public string ExecuteExact(string url)
        {
            var result = string.Empty;

            result = Execute(url);

            return result;
        }

        public List<SearchResultDrugEvent> GetDeviceEventByDescription(string keyword)
        {
            var deviceEventList = new List<AdverseDeviceEvent>();
            const OpenFdaApiEndPoints endPointType = OpenFdaApiEndPoints.DeviceEvent;
            var dataSetSize = 0;
            var yearCheck = -1;

            //Limit first query to a 1 year window
            var beginDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1));
            var endDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck));

            ResetSearch();
            AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, new List<string>(new[]
                                                                                                  {
                                                                                                      keyword
                                                                                                  }), FilterCompairType.And);
            AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                {
                                                                                    beginDate,
                                                                                    endDate
                                                                                }), FilterCompairType.And);
            var limit = AddResultLimit(100);
            var apiUrl = BuildUrl(endPointType);
            var searchResults = Execute(apiUrl + limit);

            dataSetSize = GetMetaResults().
                Total;

            do
            {
                // If there was not data in the 1 yr window the get all results.
                // Check date range window for results year 2 thur year 5.
                if (dataSetSize == 0)
                {
                    yearCheck--;
                    endDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck));

                    ResetSearch();
                    AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, new List<string>(new[]
                                                                                                          {
                                                                                                              keyword
                                                                                                          }), FilterCompairType.And);
                    AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                        {
                                                                                            beginDate,
                                                                                            endDate
                                                                                        }), FilterCompairType.And);

                    apiUrl = BuildUrl(endPointType);

                    searchResults = Execute(apiUrl + limit);
                    //OpenFdaApiHits += 1

                    dataSetSize = GetMetaResults().
                        Total;
                }
            }
            while (!(dataSetSize > 0 || Math.Abs(yearCheck) >= 5));

            //Search w/o Date range filter
            if (string.IsNullOrEmpty(searchResults))
            {
                ResetSearch();
                AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, new List<string>(new[]
                                                                                                      {
                                                                                                          keyword
                                                                                                      }), FilterCompairType.And);

                apiUrl = BuildUrl(endPointType);

                searchResults = Execute(apiUrl + limit);
                //OpenFdaApiHits += 1
                dataSetSize = GetMetaResults().
                    Total;
            }

            if (!string.IsNullOrEmpty(searchResults))
            {
                dataSetSize = GetMetaResults().
                    Total;
            }

            // LIMIT the number of page request to a MAX of 5
            var pageLimit = (int) (decimal.Ceiling((decimal) ((double) dataSetSize/100)));
            if (pageLimit > 5)
            {
                pageLimit = 5;
            }

            var skipValue = 0;
            if (dataSetSize > 0)
            {
                do
                {
                    pageLimit--;

                    if (!string.IsNullOrEmpty(searchResults))
                    {
                        var result = AdverseDeviceEvent.CnvJsonDataToList(searchResults);
                        deviceEventList.AddRange(result);
                    }

                    if (pageLimit > 0)
                    {
                        skipValue += 100;
                        //Dim newApiUrl As String = apiUrl.Replace("&limit=100", String.Format("&limit=100&skip={0}", skipValue))
                        limit = string.Format("&limit=100&skip={0}", skipValue);
                        searchResults = Execute(apiUrl + limit);
                        // OpenFdaApiHits += 1
                    }
                }
                while (!(pageLimit == 0));
            }

            var tmpSearchResultDeviceEvent = AdverseDeviceEvent.CnvDeviceEventsToResultDrugEvents(deviceEventList);

            return tmpSearchResultDeviceEvent;
        }

        public int GetDeviceEventsByDescriptionCount(string keyword)
        {
            var endPointType = OpenFdaApiEndPoints.DeviceEvent;
            var dataSetSize = 0;
            var yearCheck = -1;

            //Limit first query to a 1 year window
            var beginDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1));
            var endDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck));

            ResetSearch();
            AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, new List<string>(new[]
                                                                                                  {
                                                                                                      keyword
                                                                                                  }), FilterCompairType.And);
            AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                {
                                                                                    beginDate,
                                                                                    endDate
                                                                                }), FilterCompairType.And);
            var apiUrl = BuildUrl(endPointType);
            var searchResults = Execute(apiUrl);

            dataSetSize = GetMetaResults().
                Total;

            do
            {
                // If there was not data in the 1 yr window the get all results.
                // Check date range window for results year 2 thur year 5.
                if (dataSetSize == 0)
                {
                    yearCheck--;
                    endDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck));

                    ResetSearch();
                    AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, new List<string>(new[]
                                                                                                          {
                                                                                                              keyword
                                                                                                          }), FilterCompairType.And);
                    AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                        {
                                                                                            beginDate,
                                                                                            endDate
                                                                                        }), FilterCompairType.And);

                    apiUrl = BuildUrl(endPointType);

                    searchResults = Execute(apiUrl);
                    //OpenFdaApiHits += 1

                    dataSetSize = GetMetaResults().
                        Total;
                }
            }
            while (!(dataSetSize > 0 || Math.Abs(yearCheck) >= 5));

            //Search w/o Date range filter
            if (string.IsNullOrEmpty(searchResults))
            {
                ResetSearch();
                AddSearchFilter(endPointType, FdaFilterTypes.DeviceEventDescription, new List<string>(new[]
                                                                                                      {
                                                                                                          keyword
                                                                                                      }), FilterCompairType.And);

                apiUrl = BuildUrl(endPointType);

                searchResults = Execute(apiUrl);
            }

            if (!string.IsNullOrEmpty(searchResults))
            {
                dataSetSize = GetMetaResults().
                    Total;
            }

            return dataSetSize;
        }

        public List<SearchResultDrugEvent> GetDrugEventsByDrugName(string drugName)
        {
            var drugEventList = new List<AdverseDrugEvent>();
            var tmpSearchResultDrugEvent = new List<SearchResultDrugEvent>();
            var endPointType = OpenFdaApiEndPoints.DrugEvent;
            var dataSetSize = 0;
            var yearCheck = -1;

            var apiUrl = "";
            var searchResults = "";
            var limit = "";

            //Limit first query to a 1 year window
            var beginDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1));
            var endDate = string.Empty;

            ResetSearch();
            AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, new List<string>(new[]
                                                                                             {
                                                                                                 drugName
                                                                                             }), FilterCompairType.And);
            AddCountField("receivedate");

            apiUrl = BuildUrl(endPointType);
            searchResults = Execute(apiUrl);

            if (!string.IsNullOrEmpty(searchResults))
            {
                var jo = JObject.Parse(searchResults);
                var maxEventDate = (from el in jo["results"]
                                    select Utilities.ConvertDateStringToDate(el["time"].ToString(), "yyyyMMdd")).Max();

                if (!(maxEventDate == null))
                {
                    endDate = string.Format("{0:yyyyMMdd}", maxEventDate.AddYears(yearCheck));
                }
            }

            if (string.IsNullOrEmpty(endDate))
            {
                return tmpSearchResultDrugEvent;
            }

            ResetSearch();
            AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, new List<string>(new[]
                                                                                             {
                                                                                                 drugName
                                                                                             }), FilterCompairType.And);
            AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                {
                                                                                    beginDate,
                                                                                    endDate
                                                                                }), FilterCompairType.And);

            limit = AddResultLimit(100);
            apiUrl = BuildUrl(endPointType);
            searchResults = Execute(apiUrl + limit);
            dataSetSize = GetMetaResults().
                Total;

            do
            {
                // If there was not data in the 1 yr window the get all results.
                // Check date range window for results year 2 thur year 5.
                if (dataSetSize == 0)
                {
                    yearCheck--;
                    endDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck));

                    ResetSearch();
                    AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, new List<string>(new[]
                                                                                                     {
                                                                                                         drugName
                                                                                                     }), FilterCompairType.And);
                    AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                        {
                                                                                            beginDate,
                                                                                            endDate
                                                                                        }), FilterCompairType.And);

                    apiUrl = BuildUrl(endPointType);

                    searchResults = Execute(apiUrl + limit);
                    //OpenFdaApiHits += 1

                    dataSetSize = GetMetaResults().
                        Total;
                }
            }
            while (!(dataSetSize > 0 || Math.Abs(yearCheck) >= 5));

            //Search w/o Date range filter
            if (string.IsNullOrEmpty(searchResults))
            {
                ResetSearch();
                AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, new List<string>(new[]
                                                                                                 {
                                                                                                     drugName
                                                                                                 }), FilterCompairType.And);

                apiUrl = BuildUrl(endPointType);

                searchResults = Execute(apiUrl + limit);
                //OpenFdaApiHits += 1
                dataSetSize = GetMetaResults().
                    Total;
            }

            if (!string.IsNullOrEmpty(searchResults))
            {
                dataSetSize = GetMetaResults().
                    Total;
            }

            // LIMIT the number of page request to a MAX of 5
            var pageLimit = (int) (decimal.Ceiling((decimal) ((double) dataSetSize/100)));
            if (pageLimit > 5)
            {
                pageLimit = 5;
            }

            var skipValue = 0;
            if (dataSetSize > 0)
            {
                do
                {
                    pageLimit--;

                    if (!string.IsNullOrEmpty(searchResults))
                    {
                        var result = AdverseDrugEvent.CnvJsonDataToList(searchResults);
                        drugEventList.AddRange(result);
                    }

                    if (pageLimit > 0)
                    {
                        skipValue += 100;
                        //Dim newApiUrl As String = apiUrl.Replace("&limit=100", String.Format("&limit=100&skip={0}", skipValue))
                        limit = string.Format("&limit=100&skip={0}", skipValue);
                        searchResults = Execute(apiUrl + limit);
                        // OpenFdaApiHits += 1
                    }
                }
                while (!(pageLimit == 0));
            }

            tmpSearchResultDrugEvent = SearchResultDrugEvent.ConvertJsonData(drugEventList);

            //Dim sortedResult = (From el In tmpSearchResultDrugEvent Select el Order By el.ReportDate Descending).ToList()

            return tmpSearchResultDrugEvent;
        }

        public int GetDrugEventsByDrugNameCount(string drugName)
        {
            //Dim tmpAdverseDrugEventtList As List(Of AdverseDrugEvent)
            var endPointType = OpenFdaApiEndPoints.DrugEvent;
            var dataSetSize = 0;
            var yearCheck = -1;
            var apiUrl = "";
            var searchResults = "";

            //Limit first query to a 1 year window
            var beginDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1));
            var endDate = string.Empty;

            ResetSearch();
            AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, new List<string>(new[]
                                                                                             {
                                                                                                 drugName
                                                                                             }), FilterCompairType.And);
            AddCountField("receivedate");

            apiUrl = BuildUrl(endPointType);
            searchResults = Execute(apiUrl);

            if (!string.IsNullOrEmpty(searchResults))
            {
                var jo = JObject.Parse(searchResults);

                var maxEventDate = (from el in jo["results"]
                                    select Utilities.ConvertDateStringToDate(el["time"].ToString(), "yyyyMMdd")).Max();

                if (!(maxEventDate == null))
                {
                    endDate = string.Format("{0:yyyyMMdd}", maxEventDate.AddYears(yearCheck));
                }
            }

            if (string.IsNullOrEmpty(endDate))
            {
                return 0;
            }

            ResetSearch();
            AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, new List<string>(new[]
                                                                                             {
                                                                                                 drugName
                                                                                             }), FilterCompairType.And);
            AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                {
                                                                                    beginDate,
                                                                                    endDate
                                                                                }), FilterCompairType.And);
            //Dim limit As String = AddResultLimit(100)

            apiUrl = BuildUrl(endPointType);
            searchResults = Execute(apiUrl);
            dataSetSize = GetMetaResults().
                Total;

            do
            {
                // If there was not data in the 1 yr window the get all results.
                // Check date range window for results year 2 thur year 5.
                if (dataSetSize == 0)
                {
                    yearCheck--;
                    endDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(yearCheck));

                    ResetSearch();
                    AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, new List<string>(new[]
                                                                                                     {
                                                                                                         drugName
                                                                                                     }), FilterCompairType.And);
                    AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                        {
                                                                                            beginDate,
                                                                                            endDate
                                                                                        }), FilterCompairType.And);

                    apiUrl = BuildUrl(endPointType);

                    searchResults = Execute(apiUrl);
                    //OpenFdaApiHits += 1

                    dataSetSize = GetMetaResults().
                        Total;
                }
            }
            while (!(dataSetSize > 0 || Math.Abs(yearCheck) >= 5));

            //Search w/o Date range filter
            if (string.IsNullOrEmpty(searchResults))
            {
                ResetSearch();
                AddSearchFilter(endPointType, FdaFilterTypes.DrugEventDrugName, new List<string>(new[]
                                                                                                 {
                                                                                                     drugName
                                                                                                 }), FilterCompairType.And);

                apiUrl = BuildUrl(endPointType);

                searchResults = Execute(apiUrl);
            }

            if (!string.IsNullOrEmpty(searchResults))
            {
                dataSetSize = GetMetaResults().
                    Total;
            }

            return dataSetSize;
        }

        public void SearchFieldExists(string searchField)
        {
            SearchOnFieldByValue("_exists_", searchField);
        }

        public void SearchOnFieldByValue(string searchField, string searchFieldValue)
        {
            searchFieldValue = RemoveSpecialCharactersFromKeyword(searchFieldValue);
            searchFieldValue = searchFieldValue.Replace(" ", "+");

            if (searchFieldValue.Contains("+"))
            {
                searchFieldValue = "\"" + searchFieldValue + "\"";
            }

            _search = string.Format("{0}:{1}", searchField, searchFieldValue);
        }

        #endregion

        #region Private Methods

        private string RemoveSpecialCharactersFromKeyword(string keyword)
        {
            var tmpitm = Regex.Replace(keyword, "\\s+", " ");
            tmpitm = Regex.Replace(tmpitm, "[\\^\\[\\-\\$\\{\\*\\(\\\\\\+\\)\\|\\?\\<\\>!@#%&;]", " ");
            keyword = Regex.Replace(tmpitm, " {2,}", " ");

            return keyword;
        }

        #endregion

        internal string GetOpenFdaEndPoint(OpenFdaApiEndPoints endpoint)
        {
            _endPointType = endpoint;

            var result = string.Empty;
            var endPT = Utilities.GetEnumDefaultValue(endpoint);
            //var endPT = Enum.GetName(typeof (OpenFdaApiEndPoints), endpoint);

            result = Utilities.AddForwardSlash(HostUrl) + endPT + ".json?";

            return result;
        }

        internal MetaResults GetMetaResults()
        {
            var metaData = new MetaResults();

            if (_meta != null)
            {
                metaData = GetMetaResults(_meta);
            }

            return metaData;
        }

        internal MetaResults GetMetaResults(string searchResults)
        {
            var metaData = new MetaResults();

            if (!string.IsNullOrEmpty(searchResults))
            {
                var jo = JObject.Parse(searchResults);
                var meta = (JObject) (jo.GetValue("meta"));

                metaData = GetMetaResults(meta);
            }

            return metaData;
        }

        internal MetaResults GetMetaResults(JObject meta)
        {
            var metaData = new MetaResults();

            if (meta["results"] != null)
            {
                metaData.Limit = (int) meta["results"]["limit"];
                metaData.Skip = (int) meta["results"]["skip"];
                metaData.Total = (int) meta["results"]["total"];
            }

            return metaData;
        }

        internal void ResetSearch()
        {
            _search = string.Empty;
//_limit = String.Empty
            _count = string.Empty;
        }

        internal string AddResultLimit(int limit)
        {
            var parm = string.Empty;

            if (limit <= 0)
            {
                limit = 0;
            }
            else if (limit > 100)
            {
                limit = 100;
            }

            parm = string.Format("&limit={0}", limit);

            return parm;
        }
    }
}