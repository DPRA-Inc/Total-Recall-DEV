using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using ShopAware.Core.DataObjects;
using ShopAware.Core.Enumerations;

namespace ShopAware.Core
{
    public class ShopAwareService
    {
        #region Member Variables

        private const int MaxResultSetSize = 100;
        private readonly IRestClient _restClient;
        private OpenFda _fda;

        #endregion

        #region Properties

        public int OpenFdaApiHits { get; set; }

        #endregion

        #region Constructors

        public ShopAwareService()
        {
            _restClient = new RestClient();
        }

        public ShopAwareService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets a detailed list of issues. Issues are enforcements (Recalls) of food, drug and device (identified by
        ///     classification) and events of drug and devices
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="state"></param>
        /// <returns>FDA Result object</returns>
        /// <remarks></remarks>
        public FdaResult GetFdaResult(string keyWord, string state)
        {
            var dataYearsBack = 1;

            var searchResultLocal = new FdaResult()
                                    {
                                        Keyword = keyWord,
                                        Results = new List<SearchResultItemBase>()
                                    };

            var mapList = new Dictionary<string, SearchResultMapData>();
            var graphData = new ReportData();

            var tmp = GetRecallInfo(keyWord, state);

            var dateInit = DateTime.Now.AddYears(-dataYearsBack);

            // Initialize graph data
            for (var i = 1; i <= 12*dataYearsBack; i++)
            {
                dateInit = dateInit.AddMonths(1);

                graphData.Labels.Add(dateInit.ToString("MMM-yyyy"));
                graphData.Data1.Add(0);
                graphData.Data2.Add(0);
                graphData.Data3.Add(0);
                graphData.DataE.Add(0);
            }

            // Reconstruct recall data as searchresultitem object
            foreach (var itm in tmp) // should be in order by newest date
            {
                var newItemDate = DateTime.ParseExact(itm.Recall_Initiation_Date, "yyyyMMdd", CultureInfo.InvariantCulture);
                var tmpReportDate = DateTime.ParseExact(itm.Report_Date, "yyyyMMdd", CultureInfo.InvariantCulture);

                var tmpSearchResultItem = new SearchResultItem()
                                          {
                                              City = itm.City,
                                              DateStarted = newItemDate.ToString("ddMMMyyyy"),
                                              Content = string.Format("{0} {1}", itm.Reason_For_Recall, itm.Code_info),
                                              DistributionPattern = itm.Distribution_Pattern,
                                              ProductDescription = itm.Product_Description,
                                              State = itm.State,
                                              Status = itm.Status,
                                              Country = itm.Country,
                                              RecallNumber = itm.Recall_Number,
                                              ProductQuantity = itm.Product_Quantity,
                                              EventId = itm.Event_Id,
                                              RecallingFirm = itm.Recalling_Firm,
                                              ReportDate = tmpReportDate.ToString("ddMMMyyyy"),
                                              CodeInfo = itm.Code_info,
                                              Classification = itm.Classification,
                                              Voluntary = itm.Voluntary_Mandated
                                          };

                searchResultLocal.Results.Add(tmpSearchResultItem);
            }

            // Lets Get the Events And Mix them In.
            var drugee = new OpenFda();

            //Get Drug Events
            var drugs = drugee.GetDrugEventsByDrugName(keyWord);
            searchResultLocal.Results.AddRange(drugs);

            //Get Device Events
            drugs = drugee.GetDeviceEventByDescription(keyWord);
            searchResultLocal.Results.AddRange(drugs);

            //'Sort for most recient at the top of the list

            // searchResultLocal.Results cannot be sorted because
            searchResultLocal.Results = ((from el in searchResultLocal.Results
                                          select el).OrderByDescending(el => el.SortDate)).ToList();

            // Now that the data is combined and sorted, Process data for maps and graphs
            foreach (var itm in searchResultLocal.Results)
            {
                var classification = string.Empty;
                var reportDate = string.Empty;

                // Map Processing first
                switch (itm.GetType().
                            ToString())
                {
                    case "ShopAware.Core.DataObjects.SearchResultItem":
                        ProcessResultRecordForRecall((SearchResultItem) itm, mapList);
                        classification = ((SearchResultItemBase) itm).Classification;
                        reportDate = ((SearchResultItemBase) itm).ReportDate;
                        break;

                    case "ShopAware.Core.DataObjects.SearchResultDrugEvent":
                        ProcessResultRecordForDrugEvent((SearchResultDrugEvent) itm, mapList);
                        classification = ((SearchResultItemBase) itm).Classification;
                        reportDate = ((SearchResultItemBase) itm).ReportDate;
                        break;
                }

                // Now process graph data
                var tmpReportDate = DateTime.ParseExact(reportDate, "ddMMMyyyy", CultureInfo.InvariantCulture);
                var dateForReport = tmpReportDate.ToString("MMM-yyyy");
                var found = false;

                switch (classification)
                {
                    case "Class I":

                        for (var i = 0; i <= graphData.Labels.Count - 1; i++)
                        {
                            if (graphData.Labels[i] == dateForReport)
                            {
                                found = true;
                                graphData.Data1[i] += 1;

                                break;
                            }
                        }

                        if (!found)
                        {
                            graphData.Labels.Insert(0, dateForReport);
                            graphData.Data1.Insert(0, 1);
                            graphData.Data2.Insert(0, 0);
                            graphData.Data3.Insert(0, 0);
                            graphData.DataE.Insert(0, 0);
                        }
                        break;

                    case "Class II":

                        for (var i = 0; i <= graphData.Labels.Count - 1; i++)
                        {
                            if (graphData.Labels[i] == dateForReport)
                            {
                                found = true;
                                graphData.Data2[i] += 1;
                            }
                        }

                        if (!found)
                        {
                            graphData.Labels.Insert(0, dateForReport);
                            graphData.Data1.Insert(0, 0);
                            graphData.Data2.Insert(0, 1);
                            graphData.Data3.Insert(0, 0);
                            graphData.DataE.Insert(0, 0);
                        }
                        break;

                    case "Class III":

                        for (var i = 0; i <= graphData.Labels.Count - 1; i++)
                        {
                            if (graphData.Labels[i] == dateForReport)
                            {
                                found = true;
                                graphData.Data3[i] += 1;
                            }
                        }

                        if (!found)
                        {
                            graphData.Labels.Insert(0, dateForReport);
                            graphData.Data1.Insert(0, 0);
                            graphData.Data2.Insert(0, 0);
                            graphData.Data3.Insert(0, 1);
                            graphData.DataE.Insert(0, 0);
                        }
                        break;

                    case "Device Event":
                    case "Drug Event":
                    case "Event":

                        for (var i = 0; i <= graphData.Labels.Count - 1; i++)
                        {
                            if (graphData.Labels[i] == dateForReport)
                            {
                                found = true;
                                graphData.DataE[i] += 1;
                            }
                        }

                        if (!found)
                        {
                            graphData.Labels.Insert(0, dateForReport);
                            graphData.Data1.Insert(0, 0);
                            graphData.Data2.Insert(0, 0);
                            graphData.Data3.Insert(0, 0);
                            graphData.DataE.Insert(0, 1);
                        }
                        break;
                }
            }

            searchResultLocal.MapObjects = ConvertDictionaryMapObjectsToSearchResult(mapList);
            searchResultLocal.GraphObjects = graphData;

            return searchResultLocal;
        }

        /// <summary>
        ///     Gets a count of issues. Issues are enforcements (Recalls) of food, drug and device (identified by classification)
        ///     and events of drug and devices
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public SearchSummary GetSearchSummary(string keyWord, string state)
        {
            SearchSummary results = null;

            results = GetRecallInfoCounts(keyWord, state);

            return results;
        }

        #endregion

        #region Private Methods

        private List<SearchResultMapData> ConvertDictionaryMapObjectsToSearchResult(Dictionary<string, SearchResultMapData> mapList)
        {
            var result = new List<SearchResultMapData>();

            foreach (var mapData in mapList)
            {
                result.Add(mapData.Value);
            }

            return result;
        }

        private SearchSummary ExecuteSearchCounts(OpenFdaApiEndPoints endPointType,
                                                  FdaFilterTypes filterType,
                                                  List<string> filterList,
                                                  int maxresultsize,
                                                  string state,
                                                  string cntField)
        {
            var apiUrl = string.Empty;
            var tmpRecallResultList = new List<ResultRecall>();

            var searchSummary = new SearchSummary()
                                {
                                    Keyword = filterList[0],
                                    State = state
                                };
            var searchResults = "";

            //Limit first query to a 1 year window
            var beginDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1));
            var endDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(-1));

            _fda.ResetSearch();
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, new List<string>(new[]
                                                                                       {
                                                                                           state
                                                                                       }), FilterCompairType.And);
            _fda.AddSearchFilter(endPointType, filterType, filterList, FilterCompairType.And);
            _fda.AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                     {
                                                                                         beginDate,
                                                                                         endDate
                                                                                     }), FilterCompairType.And);

            _fda.AddCountField(string.Format("{0}.exact", cntField.ToLower()));
            apiUrl = _fda.BuildUrl(endPointType);

            searchResults = _fda.Execute(apiUrl);

            // If there was not data in the 1 yr window the get all results.
            // Check a 2 yr window for results.
            if (string.IsNullOrEmpty(searchResults))
            {
                endDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(-2));

                _fda.ResetSearch();
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, new List<string>(new[]
                                                                                           {
                                                                                               state
                                                                                           }), FilterCompairType.And);
                _fda.AddSearchFilter(endPointType, filterType, filterList, FilterCompairType.And);
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                         {
                                                                                             beginDate,
                                                                                             endDate
                                                                                         }), FilterCompairType.And);

                _fda.AddCountField(string.Format("{0}.exact", cntField.ToLower()));
                apiUrl = _fda.BuildUrl(endPointType);

                searchResults = _fda.Execute(apiUrl);
            }

            // If there was not data in the 2 yr window the get all results.
            if (string.IsNullOrEmpty(searchResults))
            {
                _fda.ResetSearch();
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, new List<string>(new[]
                                                                                           {
                                                                                               state
                                                                                           }), FilterCompairType.And);
                _fda.AddSearchFilter(endPointType, filterType, filterList, FilterCompairType.And);

                _fda.AddCountField(string.Format("{0}.exact", cntField.ToLower()));
                apiUrl = _fda.BuildUrl(endPointType);

                searchResults = _fda.Execute(apiUrl);
            }

            if (!string.IsNullOrEmpty(searchResults))
            {
                var jo = JObject.Parse(searchResults);
                var countResults = (JArray) (jo["results"]);

                var termCountFound = false;
                var termCount = 0;

                foreach (var itm in countResults)
                {
                    termCount = (int) itm["count"];

                    var termClassification = (itm["term"]).ToString();

                    switch (termClassification)
                    {
                        case "Class I":

                            searchSummary.ClassICount = termCount;
                            termCountFound = true;
                            break;

                        case "Class II":

                            searchSummary.ClassIICount = termCount;
                            termCountFound = true;
                            break;

                        case "Class III":
                            searchSummary.ClassIIICount = termCount;
                            termCountFound = true;
                            break;
                    }
                }

                if (!termCountFound)
                {
                    searchSummary = null;
                }
            }

            return searchSummary;
        }

        private IEnumerable<ResultRecall> GetRecallInfo(string keyWord, string state)
        {
            _fda = new OpenFda(_restClient);

            OpenFdaApiHits = 0;

            var resultList = new List<ResultRecall>();
            var endPointList = new List<OpenFdaApiEndPoints>(new[]
                                                             {
                                                                 OpenFdaApiEndPoints.FoodRecall,
                                                                 OpenFdaApiEndPoints.DrugRecall,
                                                                 OpenFdaApiEndPoints.DeviceRecall
                                                             });

            foreach (var endPointType in endPointList)
            {
                var filterList = new List<string>(new[]
                                                  {
                                                      state
                                                  });

                //Limit first query to a 1 year window
                var beginDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(1));
                var endDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(-1));

                _fda.ResetSearch();
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, filterList, FilterCompairType.And);
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.RecallReason, new List<string>(new[]
                                                                                                 {
                                                                                                     keyWord
                                                                                                 }), FilterCompairType.And);
                _fda.AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                         {
                                                                                             beginDate,
                                                                                             endDate
                                                                                         }), FilterCompairType.And);

                var apiUrl = _fda.BuildUrl(endPointType, MaxResultSetSize);

                var searchResults = _fda.Execute(apiUrl);
                OpenFdaApiHits++;

                var dataSetSize = _fda.GetMetaResults().
                                       Total;

                // If there was not data in the 1 yr window the get all results.
                // Check a 2 yr window for results.
                if (dataSetSize == 0)
                {
                    endDate = string.Format("{0:yyyyMMdd}", DateTime.Now.AddYears(-2));

                    _fda.ResetSearch();
                    _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, filterList, FilterCompairType.And);
                    _fda.AddSearchFilter(endPointType, FdaFilterTypes.RecallReason, new List<string>(new[]
                                                                                                     {
                                                                                                         keyWord
                                                                                                     }), FilterCompairType.And);
                    _fda.AddSearchFilter(endPointType, FdaFilterTypes.Date, new List<string>(new[]
                                                                                             {
                                                                                                 beginDate,
                                                                                                 endDate
                                                                                             }), FilterCompairType.And);

                    apiUrl = _fda.BuildUrl(endPointType, MaxResultSetSize);

                    searchResults = _fda.Execute(apiUrl);
                    OpenFdaApiHits++;

                    dataSetSize = _fda.GetMetaResults().
                                       Total;
                }

                // If there was not data in the 2 yr window the get all results.
                if (dataSetSize == 0)
                {
                    _fda.ResetSearch();
                    _fda.AddSearchFilter(endPointType, FdaFilterTypes.Region, filterList, FilterCompairType.And);
                    _fda.AddSearchFilter(endPointType, FdaFilterTypes.RecallReason, new List<string>(new[]
                                                                                                     {
                                                                                                         keyWord
                                                                                                     }), FilterCompairType.And);

                    apiUrl = _fda.BuildUrl(endPointType, MaxResultSetSize);

                    searchResults = _fda.Execute(apiUrl);
                    OpenFdaApiHits++;

                    dataSetSize = _fda.GetMetaResults().
                                       Total;
                }

                // if total records int the Search request exceeds the max of 100 records per request
                // then page through the data
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
                            var result = ResultRecall.CnvJsonDataToList(searchResults);
                            resultList.AddRange(result);
                        }

                        if (pageLimit > 0)
                        {
                            skipValue += 100;
                            var newApiUrl = apiUrl.Replace("&limit=100", string.Format("&limit=100&skip={0}", skipValue));
                            searchResults = _fda.Execute(apiUrl);
                            OpenFdaApiHits++;
                        }
                    }
                    while (!(pageLimit == 0));
                }
            }

            var sortedResultList = ((from el in resultList
                                     select el).OrderByDescending(el => el.Recall_Initiation_Date)).ToList();

            return sortedResultList;

            //Return resultList
        }

        private SearchSummary GetRecallInfoCounts(string keyWord, string state)
        {
            _fda = new OpenFda(_restClient);

            var searchSummaryForKeyword = new SearchSummary()
                                          {
                                              Keyword = keyWord,
                                              State = state
                                          };
            var filterType = default(FdaFilterTypes);
            filterType = FdaFilterTypes.RecallReason;

            var endPointList = new List<OpenFdaApiEndPoints>(new[]
                                                             {
                                                                 OpenFdaApiEndPoints.FoodRecall,
                                                                 OpenFdaApiEndPoints.DrugRecall,
                                                                 OpenFdaApiEndPoints.DeviceRecall
                                                             });

            const int maxresultsize = 0;

            var filterList = new List<string>();
            filterList.Add(keyWord);

            foreach (var endPoint in endPointList)
            {
                var endpointSearchSummary = ExecuteSearchCounts(endPoint, filterType, filterList, maxresultsize, state, "classification");

                if (endpointSearchSummary != null)
                {
                    searchSummaryForKeyword.ClassICount += endpointSearchSummary.ClassICount;
                    searchSummaryForKeyword.ClassIICount += endpointSearchSummary.ClassIICount;
                    searchSummaryForKeyword.ClassIIICount += endpointSearchSummary.ClassIIICount;
                }
            }

            searchSummaryForKeyword.EventCount = 0;
            searchSummaryForKeyword.EventCount += _fda.GetDrugEventsByDrugNameCount(keyWord);
            searchSummaryForKeyword.EventCount += _fda.GetDeviceEventsByDescriptionCount(keyWord);

            return searchSummaryForKeyword;
        }

        private void ProcessResultRecordForDrugEvent(SearchResultDrugEvent data, Dictionary<string, SearchResultMapData> list)
        {
            const string check = "nationwide"; // add state field data.
            var states = Enum.GetNames(typeof (States)).
                              ToList();
            var nationwide = false;

            if (check.ToLower().
                      Contains("nationwide"))
            {
                nationwide = true;
            }

            foreach (var state in states)
            {
                var stateEnum = Enum.Parse(typeof (States), state);
                var stateName = Utilities.GetEnumDescription(stateEnum);
                var stateCoords = Utilities.GetEnumCoordinates(stateEnum);

                if (check.Contains(state) || check.ToUpper().
                                                   Contains(stateName.ToUpper()) || nationwide)
                {
                    SearchResultMapData listCheck = null;

                    if (list.ContainsKey(state))
                    {
                        listCheck = list[state];
                    }
                    else
                    {
                        listCheck = new SearchResultMapData
                                    {
                                        State = state,
                                        Latitude = stateCoords.Latitude.ToString(),
                                        Longitude = stateCoords.Longitude.ToString()
                                    };
                        list.Add(state, listCheck);
                    }

                    var tooltip = string.Concat(data.Rank);

                    listCheck.IconSet = listCheck.IconSet | IconSet.Event;

                    if (!listCheck.Tooltip.Contains(tooltip))
                    {
                        if (listCheck.Tooltip.Length > 0)
                        {
                            listCheck.Tooltip += ", ";
                        }

                        listCheck.Tooltip += tooltip;
                    }

                    // Get the Rank
                    var rank = 9;
                    switch (data.Rank.ToLower())
                    {
                        case "classi":
                            rank = 1;
                            break;
                        case "classii":
                            rank = 2;
                            break;
                        case "classiii":
                            rank = 3;
                            break;
                        case "events":
                            rank = 4;
                            break;
                        case "device event":
                            rank = 5;
                            break;
                        default:
                            rank = 5;
                            break;
                    }

                    if (listCheck.Rank > rank)
                    {
                        listCheck.Rank = rank;
                    }

                    list[state] = listCheck;

                    list[state] = listCheck;
                }
            }
        }

        private void ProcessResultRecordForRecall(SearchResultItem data, Dictionary<string, SearchResultMapData> list)
        {
            var check = data.DistributionPattern;
            var states = Enum.GetNames(typeof (States)).
                              ToList();
            var nationwide = check.ToLower().
                                   Contains("nationwide");

            foreach (var state in states)
            {
                var stateEnum = Enum.Parse(typeof (States), state);
                var stateName = Utilities.GetEnumDescription(stateEnum);
                var stateCoords = Utilities.GetEnumCoordinates(stateEnum);

                if (check.Contains(state) || check.ToUpper().
                                                   Contains(stateName.ToUpper()) || nationwide)
                {
                    SearchResultMapData listCheck = null;

                    if (list.ContainsKey(state))
                    {
                        listCheck = list[state];
                    }
                    else
                    {
                        listCheck = new SearchResultMapData
                                    {
                                        State = state,
                                        Latitude = stateCoords.Latitude.ToString(),
                                        Longitude = stateCoords.Longitude.ToString()
                                    };

                        list.Add(state, listCheck);
                    }

                    var tooltip = string.Concat(data.Classification, " {0}");

                    switch (data.Classification.ToLower())
                    {
                        case "class i":

                            tooltip = string.Format(tooltip, " Class-1");
                            listCheck.IconSet = listCheck.IconSet | IconSet.Class1;
                            break;

                        case "class ii":

                            tooltip = string.Format(tooltip, " Class-2");
                            listCheck.IconSet = listCheck.IconSet | IconSet.Class2;
                            break;

                        case "class iii":

                            tooltip = string.Format(tooltip, " Class-3");
                            listCheck.IconSet = listCheck.IconSet | IconSet.Class3;
                            break;
                    }

                    if (!listCheck.Tooltip.Contains(tooltip))
                    {
                        if (listCheck.Tooltip.Length > 0)
                        {
                            listCheck.Tooltip += ", ";
                        }

                        listCheck.Tooltip += tooltip;
                    }

                    // Get the Rank
                    var rank = 9;
                    switch (data.Rank.ToLower())
                    {
                        case "classi":
                            rank = 1;
                            break;
                        case "classii":
                            rank = 2;
                            break;
                        case "classiii":
                            rank = 3;
                            break;
                        case "events":
                            rank = 4;
                            break;
                        case "device event":
                            rank = 5;
                            break;
                        default:
                            rank = 5;
                            break;
                    }

                    if (listCheck.Rank > rank)
                    {
                        listCheck.Rank = rank;
                    }

                    list[state] = listCheck;
                }
            }
        }

        #endregion
    }
}