angular.module('TotalRecall').factory('productservice', ProductService);

function ProductService($http) {

    var service = {
        GetSearchResult: GetSearchResult
    };

    return service;

    ///////////////////

    function GetSearchResult(product, region, callback) {

        var searchStr = product + "|" + region;
        var serviceUrl = 'QuickHandler.ashx?Command=GetSearchResult';

        $http({
            method: 'POST',
            url: serviceUrl,
            data: searchStr
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






