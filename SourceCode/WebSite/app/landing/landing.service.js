angular.module('TotalRecall').factory('landingservice', LandingService);

function LandingService($http) {

    var service = {
        GetIssues: GetIssues
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

        

}






