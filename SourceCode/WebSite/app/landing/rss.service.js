angular.module('TotalRecall').factory('feedLoader', FeedLoader);

function FeedLoader($http) {

    var service = {
        GetRSSFeed: GetRSSFeed
    };

    return service;

    //****************************************

    function GetRSSFeed(url) {
        return $http.jsonp('//ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=50&callback=JSON_CALLBACK&q=' + encodeURIComponent(url));
    }

}
