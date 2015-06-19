angular.module('TotalRecall').factory('dataService', DataService);

function DataService($http) {

    var service = {
        ServiceUrl: ServiceUrl,
        GetPeopleListing: GetPeopleListing
    };

    return service;

    function ServiceUrl() {
        return "http://localhost:33335/api/";
    }

    function GetPeopleListing(callback) {
        var serviceUrl = ServiceUrl() + 'person';

        var test = GlobalsModule.userName;
        $http({
            method: 'GET',
            url: serviceUrl,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        }).
            success(function (data, status, headers, config) {
                if (data == undefined || data == "null") return;


                callback(data);
            }).
            error(function (data, status, headers, config) {
                $log.warn(data, status, headers, config)
            });


    }



}
