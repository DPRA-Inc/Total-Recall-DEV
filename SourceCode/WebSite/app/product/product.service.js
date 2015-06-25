angular.module('TotalRecall').factory('productservice', ProductService);

function ProductService($http) {

    var service = {
        GetProductResults: GetProductResults,
        GetFDAResults: GetFDAResults
    };

    return service;

    ///////////////////

    function GetProductResults(product, region, callback) {

        var serviceUrl = "Api/ShopAware/ProductResults/";
        serviceUrl += product + "/";
        serviceUrl += region;

        $http({
            method: 'GET',
            url: serviceUrl
        }).
            success(function (data, status, headers, config) {
                if (!angular.isObject(data)) callback(null);

                callback(data);
            }).
            error(function (data, status, headers, config) {
                $log.warn(data, status, headers, config)
            });                 
    }

    function GetFDAResults(product, region, callback) {

        var serviceUrl = "Api/ShopAware/FDAResults/";
        serviceUrl += product + "/";
        serviceUrl += region;

        $http({
            method: 'GET',
            url: serviceUrl
        }).
            success(function (data, status, headers, config) {
                if (!angular.isObject(data)) callback(null);

                callback(data);
            }).
            error(function (data, status, headers, config) {
                $log.warn(data, status, headers, config)
            });
    }

}






