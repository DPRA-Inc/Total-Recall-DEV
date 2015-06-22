angular.module('TotalRecall').factory('productservice', ProductService);

function ProductService($http) {

    var service = {
        GetMoreInformation: GetMoreInformation
    };

    return service;

    ///////////////////

    function GetMoreInformation(searchItem, callback) {

        var serviceUrl = 'QuickHandler.ashx?Command=GetMoreInformation';

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






