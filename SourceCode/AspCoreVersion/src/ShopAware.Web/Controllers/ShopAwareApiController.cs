using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Caching.Memory;
using Microsoft.Framework.Runtime;
using ShopAware.Core;
using ShopAware.Core.DataObjects;

namespace ShopAware.Web.Controllers
{
    [Route("api/shopaware")]
    public class ShopAwareApiController : Controller
    {
        private readonly IApplicationEnvironment _appEnvironment;
        private readonly IMemoryCache _cache;

        public ShopAwareApiController(IApplicationEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;          
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        [HttpGet()]
        [Route("QuickSearch/{product}/{region}")]
        public SearchSummary QuickSearch(string product, string region)
        {
            var key = string.Format("QuickSearch-{0}-{1}", product, region);
            SearchSummary result;

            if (!_cache.TryGetValue(key, out result))
            {
                var wrapper = new ShopAwareService();
                result = wrapper.GetSearchSummary(product, region);

                _cache.Set(key, result);
            }
            
            return result;
        }

        [HttpGet()]
        [Route("FDAResults/{product}/{region}")]
        public FdaResult FdaResults(string product, string region)
        {
            var key = string.Format("FdaResults-{0}-{1}", product, region);
            FdaResult result;

            if (!_cache.TryGetValue(key, out result))
            {
                var wrapper = new ShopAwareService();
                result = wrapper.GetFdaResult(product, region);

                _cache.Set(key, result);
            }

            return result;
        }

        [HttpGet()]
        [Route("Regions/GetStateJson/{selected}")]
        public string GetStateJson(string selected)
        {
            var rootPath = _appEnvironment.ApplicationBasePath;
            var jsonPath = System.IO.Path.Combine(rootPath, "wwwroot\\json");

            var regions = new List<string>();
            regions.AddRange(selected.Split(Convert.ToChar(",")));

            var statePolys = new List<string>();

            foreach (var state in regions)
            {
                var folder = string.Format("{0}\\{1}.geo.json.txt", jsonPath, state);

                var data = System.IO.File.ReadAllLines(folder);

                statePolys.Add(data[1]);

            }

            var result = new List<string>
                         {
                             "{\"type\":\"FeatureCollection\",\"features\":[",
                             string.Join(",", statePolys.ToArray()),
                             "]}"
                         };

            return string.Join("", result.ToArray());
        }
    }
}
