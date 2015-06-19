   
    function landingservice() {

        var service = {
            GetPeopleListing: GetPeopleListing
        };
       
        function GetPeopleListing(callback) {

            var serviceUrl = 'QuickHandler.ashx?Command=GetPeopleListing';

            //$http({
            //    method: 'POST',
            //    url: serviceUrl,
            //    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            //}).
            //    success(function (data, status, headers, config) {
            //        if (data == undefined || data == "null") return;


            //        callback(data);
            //    }).
            //    error(function (data, status, headers, config) {
            //        $log.warn(data, status, headers, config)
            //    });

        }

        return service;

    }

    angular.module('TotalRecall').factory('landingservice', landingservice);

