﻿angular.module("TotalRecall").controller('productcontroller', ProductController);

function ProductController($scope, $http, $modal, productservice) {
    var vm = this;

    vm.SearchSummary = GlobalsModule.SearchSummary;
    vm.DataLoading = true;

    // default map configuration
    angular.extend($scope, {
        center: {
            lat: 39.50,
            lon: -98.35,
            zoom: 2
        }
    });

    if (!angular.isObject(GlobalsModule.SearchResult)) GlobalsModule.SearchResult = [];
    vm.SearchResult = GlobalsModule.SearchResult;

    if (!angular.isObject(GlobalsModule.SearchResultItem)) GlobalsModule.SearchResultItem = [];
    vm.SearchResultItem = GlobalsModule.SearchResultItem;
    if (GlobalsModule.SearchResult.length === 0) LoadPageInfo();

    //*******************************************

    function LoadPageInfo() {

        var productName = vm.SearchSummary.Keyword;
        var region = "TN";

        productservice.GetSearchResult(productName, region,
            function (result) {

                if (angular.isObject(result)) {

                    result.ClassI.forEach(function (item) {
                        item.ShowMoreInformation = false;
                    });

                }

                GlobalsModule.SearchResult = result;
                vm.SearchResult = result;
                vm.DataLoading = false;
            }
        );
    }

    vm.SetupMap = function () {

        //var junk = angular.element('test');
        //var mapControl = angular.element('map');
        //var popupControl = angular.element('popup');

        //var map = new ol.Map({
        //    layers: [rasterLayer, vectorLayer],
        //    target: mapControl,
        //    proj: new ol.proj.Projection({
        //        code: 'EPSG:4326'
        //    }),
        //    view: view
        //});        

        //var popup = new ol.Overlay({
        //    element: popupControl,
        //    positioning: 'bottom-center',
        //    stopEvent: false
        //});

        //map.addOverlay(popup);

        //// display popup on click
        //map.on('click', function (evt) {
        //    var feature = map.forEachFeatureAtPixel(evt.pixel,
        //        function (feature, layer) {
        //            return feature;
        //        });
        //    if (feature) {
        //        var geometry = feature.getGeometry();
        //        var coord = geometry.getCoordinates();
        //        popup.setPosition(coord);
        //        $(popupControl).popover({
        //            'placement': 'top',
        //            'html': true,
        //            'content': feature.get('name')
        //        });
        //        $(popupControl).popover('show');
        //    } else {
        //        $(popupControl).popover('destroy');
        //    }
        //});

        //// change mouse cursor when over marker
        //map.on('pointermove', function (e) {
        //    try {

        //        if (e.dragging) {
        //            $(popupControl).popover('destroy');
        //            return;
        //        }
        //        var pixel = map.getEventPixel(e.originalEvent);
        //        var hit = map.hasFeatureAtPixel(pixel);
        //        map.getTarget().style.cursor = hit ? 'pointer' : '';
        //    } catch (ex) {
        //    };

        //});




        //vectorSource.addFeature(createIcon(36, -84, 'Knoxville, TN', 'images/mapIcon/1.png'));
        //vectorSource.addFeature(createIcon(35, -85.3, 'Chattanooga, TN', 'images/mapIcon/2.png'));

        ////vm.Details.MapObjects.forEach(function (obj) {

        ////    vectorSource.addFeature(createIcon(obj.Latitude, obj.Longitude, obj.Tooltip, obj.Image));

        ////});

        //centerMapOnAllObjects();
    };

    vm.ShowMoreInformation = function (item) {

        GlobalsModule.SearchResultItem = item;

        var modalInstance = $modal.open({
            templateUrl: 'app/product/productFullDetails.modal.html',
            controller: 'productcontroller as vm'
        });


    };
};

