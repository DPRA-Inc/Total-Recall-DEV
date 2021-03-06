﻿angular.module("TotalRecall").factory("productservice", ProductService);

function ProductService($http, $log) {

    var service = {
        GetProductResults: GetProductResults,
        GetFDAResults: GetFDAResults,
        GetReportData: GetReportData,
        GetRegionsJson: GetRegionsJson
    };

    return service;

    ///////////////////

    function GetProductResults(product, region, callback) {

        var serviceUrl = "Api/ShopAware/ProductResults/";
        serviceUrl += product + "/";
        serviceUrl += region;

        $http({
                method: "GET",
                url: serviceUrl
            }).
            success(function(data, status, headers, config) {
                if (!angular.isObject(data)) callback(null);

                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });
    }

    function GetFDAResults(product, region, callback) {

        var serviceUrl = "Api/ShopAware/FDAResults/";
        serviceUrl += product + "/";
        serviceUrl += region;

        $http({
                method: "GET",
                url: serviceUrl
            }).
            success(function(data, status, headers, config) {
                if (!angular.isObject(data)) callback(null);

                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });
    }

    function GetReportData(product, region, callback) {

        var serviceUrl = "Api/ShopAware/GetReportData/";
        serviceUrl += product + "/";
        serviceUrl += region;

        $http({
                method: "GET",
                url: serviceUrl
            }).
            success(function(data, status, headers, config) {
                if (!angular.isObject(data)) callback(null);

                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });
    }

    function GetRegionsJson(regions, callback) {

        var serviceUrl = "Api/ShopAware/Regions/GetStateJson/";
        serviceUrl += regions;

        $http({
                method: "GET",
                url: serviceUrl
            }).
            success(function(data, status, headers, config) {

                callback(data);
            }).
            error(function(data, status, headers, config) {
                $log.warn(data, status, headers, config);
            });


    }
}