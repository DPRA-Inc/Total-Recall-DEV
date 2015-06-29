angular.module("TotalRecall").factory("landingservice", LandingService);

function LandingService($http, $log) {

    this.Feeds = [];

    var service = {
        QuickSearch: QuickSearch,   
        GetStates: GetStates
    };

    return service;

    //////////////////

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

}