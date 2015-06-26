angular.module("TotalRecall").factory("landingservice", LandingService);

function LandingService($http, $log) {

    this.Feeds = [];

    var service = {
        QuickSearch: QuickSearch,
        SetupRSSFeed: SetupRSSFeed,
        GetStates: GetStates
    };

    return service;

    ///////////////////

    /*
     * Get the issues found for an item.
     */
    function QuickSearch(searchStr, region, callback) {

        var serviceUrl = "Api/ShopAware/QuickSearch/";
        serviceUrl += searchStr + "/";
        serviceUrl += region;                

        $http({
            method: "GET",
            url: serviceUrl            
        }).
            success(function (data) {
                if (!angular.isObject(data)) return;

                callback(data);
            }).
            error(function (data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });

    }

    ///*
    // * Get a list of states.
    // */
    function GetStates(callback) {
        $http.get("json/regions.txt").
            success(function(data) {
                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });
    }

    /*
     * Get a list of states from WebAPI.
     */
    //function GetStates(callback)
    //{

    //    var serviceUrl = "Api/ShopAware/GetStates/";

    //    $http({
    //        method: 'GET',
    //        url: serviceUrl
    //    }).
    //        success(function (data, status, headers, config)
    //        {
    //            if (!angular.isObject(data)) callback(null);

    //            callback(data);
    //        }).
    //        error(function (data, status, headers, config)
    //        {
    //            $log.warn(data, status, headers, config)
    //        });

    //}

    function SetupRSSFeed() {

        //    if (Feeds.length === 0) {

        //        for (var i = 0; i < GlobalsModule.RSSFeeds.length; i++) {

        //            FeedLoader.fetch({ q: feedSources[i].url, num: 10 }, {}, function (data) {
        //                var feed = data.responseData.feed;
        //                feeds.push(feed);
        //            });

        //        }
    }
}