angular.module("TotalRecall").factory("landingservice", LandingService);

function LandingService($http, $log) {

    this.Feeds = [];

    var service = {
        GetIssues: GetIssues,
        SetupRSSFeed: SetupRSSFeed,
        GetStates: GetStates
    };

    return service;

    ///////////////////

    /*
     * Get the issues found for an item.
     */
    function GetIssues(searchStr, region, callback) {

        var searchItem = searchStr + "|" + region;

        var serviceUrl = "QuickHandler.ashx?Command=GetIssues";

        $http({
                method: "POST",
                url: serviceUrl,
                data: searchItem
            }).
            success(function(data) {
                if (!angular.isObject(data)) return;

                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });
    }

    /*
     * Get a list of states.
     */
    function GetStates(callback) {
        $http.get("json/regions.json").
            success(function(data) {
                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });
    }

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