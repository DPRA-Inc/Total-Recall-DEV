angular.module("TotalRecall").factory("productservice", ProductService);

function ProductService($http, $log, $sessionStorage) {

    var service = {
        GetProductResults: GetProductResults,
        GetFDAResults: GetFDAResults,
        GetReportData: GetReportData,
        GetRegionsJson: GetRegionsJson
    };

    return service;

    ///////////////////

    function GetProductResults(product, region, callback) {

        var serviceUrl = "Api/ShopAware/ProductResults/";
        serviceUrl += product + "/";
        serviceUrl += region;

        $http({
                method: "GET",
                url: serviceUrl
            }).
            success(function(data, status, headers, config) {
                if (!angular.isObject(data)) callback(null);

                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });
    }

    function GetFDAResults(product, region, callback) {

        var serviceUrl = "Api/ShopAware/FDAResults/";
        serviceUrl += product + "/";
        serviceUrl += region;

        $http({
                method: "GET",
                url: serviceUrl
            }).
            success(function(data, status, headers, config) {
                if (!angular.isObject(data)) callback(null);

                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });
    }

    function GetReportData(product, region, callback) {

        var serviceUrl = "Api/ShopAware/GetReportData/";
        serviceUrl += product + "/";
        serviceUrl += region;

        $http({
                method: "GET",
                url: serviceUrl
            }).
            success(function(data, status, headers, config) {
                if (!angular.isObject(data)) callback(null);

                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });
    }

    function GetRegionsJson(regions, callback) {

        var serviceUrl = "Api/ShopAware/Regions/GetStateJson/";
        serviceUrl += regions;

        $http({
                method: "GET",
                url: serviceUrl
            }).
            success(function(data, status, headers, config) {

                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });


    }

    //function GetSummaryData(callback) {

    //    var links = $location.search();
    //    var keyword = "";
    //    var region = "";

    //    if (angular.isString(links.Keyword)) keyword = links.Keyword;
    //    if (angular.isString(links.Region)) region = links.Region;

    //    if (keyword.length > 0) {
    //        landingservice.QuickSearch(keyword, region,
    //            function (summary) {
    //                $sessionStorage.SearchSummary = summary;
    //                callback(summary);                        
    //            }
    //        );
    //    }

    //}

    //function LoadPageInfo(callback) {

    //    // See if we have a query.
    //    var links = $location.search();
    //    var keyword = "";
    //    var region = "";

    //    if (angular.isString(links.Keyword)) keyword = links.Keyword;
    //    if (angular.isString(links.Region)) region = links.Region;

    //    if (keyword.length > 0) {
    //        landingservice.QuickSearch(keyword, region,
    //            function (summary) {
    //                $sessionStorage.SearchSummary = summary;
    //                callback(summary);
    //            }
    //        );
    //    }
    //    else {

    //    }



    //}

}