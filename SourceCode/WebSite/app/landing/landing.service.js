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

        // Need to Scrub it here.
        var disallowedChars = /[^a-zA-Z0-9 :]/g;
        var scrubbed = searchStr.replace(disallowedChars, " ");
        scrubbed = scrubbed.replace("  ", "").trim();

        var serviceUrl = "Api/ShopAware/QuickSearch/";
        serviceUrl += scrubbed + "/";
        serviceUrl += region;                

        $http({
            method: "GET",
            url: serviceUrl            
        }).
            success(function (result) {

                result.IsLoading = true; // Indicates we are waiting on Return From Service.
                result.HasClassI = false; // Indicates there is some Class I Data to show.
                result.HasClassII = false;
                result.HasClassIII = false;

                if (!angular.isObject(result)) return;

                if (result.EventCount > 0) {
                    result.HasEvents = true;
                    result.IsClean = false;
                    result.Rank = "events";
                }

                if (result.ClassIIICount > 0) {
                    result.HasClassIII = true;                    
                    result.IsClean = false;
                    result.Rank = "classiii";                  
                }

                if (result.ClassIICount > 0) {
                    result.HasClassII = true;                    
                    result.IsClean = false;
                    result.Rank = "classii";                    
                }

                if (result.ClassICount > 0) {
                    result.HasClassI = true;                   
                    result.IsClean = false;
                    result.Rank = "classi";                    
                }

                result.ScrubedText = result.Keyword;
                result.Keyword = searchStr;

                callback(result);

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