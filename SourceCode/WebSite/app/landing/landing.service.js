angular.module('TotalRecall').factory('landingservice', LandingService);

function LandingService($http) {

    this.Feeds = [];

    var service = {
        GetIssues: GetIssues,
        SetupRSSFeed: SetupRSSFeed
    };

    return service;

    ///////////////////

    function GetIssues(searchItem, callback) {

        var serviceUrl = 'QuickHandler.ashx?Command=GetIssues';

        $http({
            method: 'POST',
            url: serviceUrl,
            data: searchItem         
        }).
            success(function (data, status, headers, config) {
                if (!angular.isObject(data)) return;

                callback(data);
            }).
            error(function (data, status, headers, config) {
                $log.warn(data, status, headers, config)
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






    //}
    


}






