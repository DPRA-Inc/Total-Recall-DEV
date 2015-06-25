angular.module("TotalRecall").controller("productcontroller", ProductController);

function ProductController($scope, $sessionStorage, $localStorage, $http, $modal, productservice) {
    var vm = this;

    vm.fontSizeClass = "";
    vm.lineOptions = [];
    vm.lineData = [];

    if (angular.isString($localStorage.fontSizeClass)) {
        vm.fontSizeClass = $localStorage.fontSizeClass;
    }

    vm.ChangeFontSize = function(className) {
        vm.fontSizeClass = className;
        $localStorage.fontSizeClass = className;
    };
    vm.SearchSummary = $sessionStorage.SearchSummary;
    vm.DataLoading = true;

    vm.Markers = [];

    // default map configuration
    angular.extend($scope, {
        center: {
            lat: 39.50,
            lon: -98.35,
            zoom: 3
        },
        defaults: {
            interactions: {
                mouseWheelZoom: true
            },
            controls: {
                zoom: true,
                rotate: true,
                attribution: true
            }
        },
        osm: {
            source: {
                type: "OSM"
            }
        }
    });

    if (!angular.isObject(GlobalsModule.SearchResult)) GlobalsModule.SearchResult = [];
    vm.SearchResult = GlobalsModule.SearchResult;

    if (!angular.isObject(GlobalsModule.SearchResultItem)) GlobalsModule.SearchResultItem = [];
    vm.SearchResultItem = GlobalsModule.SearchResultItem;

    LoadPageInfo();
    LoadChartInfo();

    //*******************************************

    function LoadPageInfo() {

        var productName = vm.SearchSummary.Keyword;
        var region = vm.SearchSummary.State;

        productservice.GetFDAResults(productName, region,
            function(result) {

                if (angular.isObject(result) && angular.isObject(result.MapObjects)) {

                    result.MapObjects.forEach(function(mapItem) {

                        vm.Markers.push(
                            {
                                lat: parseFloat(mapItem.Latitude),
                                lon: parseFloat(mapItem.Longitude),
                                label: {
                                    message: "",
                                    show: false,
                                    showOnMouseOver: true

                                },
                                style: {
                                    image: {
                                        icon: mapItem.icon
                                    }
                                }
                            }
                        );
                    });
                }

                GlobalsModule.SearchResult = result;
                vm.SearchResult = result;
                vm.DataLoading = false;

            }
        );

    }

    //function LoadPageInfo()
    //{

    //    var productName = vm.SearchSummary.Keyword;
    //    var region = vm.SearchSummary.State;

    //    productservice.GetProductResults(productName, region,
    //        function (result)
    //        {

    //            if (angular.isObject(result))
    //            {

    //                result.MapObjects.forEach(function (mapItem)
    //                {

    //                    vm.Markers.push(
    //                       {
    //                           lat: parseFloat(mapItem.Latitude),
    //                           lon: parseFloat(mapItem.Longitude),
    //                           label: {

    //                               message: '',
    //                               show: false,
    //                               showOnMouseOver: true

    //                           },
    //                           style: {
    //                               image: {
    //                                   icon: mapItem.icon
    //                               }
    //                           }
    //                       }
    //                    )
    //                })

    //            }

    //            GlobalsModule.SearchResult = result;
    //            vm.SearchResult = result;
    //            vm.DataLoading = false;

    //        }
    //    );

    //}

    vm.ShowMoreInformation = function(item) {

        GlobalsModule.SearchResultItem = item;

        var modalInstance = $modal.open({
            templateUrl: "app/product/productFullDetails.modal.html",
            controller: "productcontroller as vm"

        });
    };

    function LoadChartInfo() {

        vm.lineOptions = {
            scaleShowGridLines: true,
            scaleGridLineColor: "rgba(0,0,0,.05)",
            scaleGridLineWidth: 1,
            bezierCurve: true,
            bezierCurveTension: 0.4,
            pointDot: true,
            pointDotRadius: 4,
            pointDotStrokeWidth: 1,
            pointHitDetectionRadius: 20,
            datasetStroke: true,
            datasetStrokeWidth: 2,
            datasetFill: true
        };


        vm.lineData = {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [
                {
                    label: "Example dataset",
                    fillColor: "rgba(220,220,220,0.5)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    data: [65, 59, 80, 81, 56, 55, 40]
                },
                {
                    label: "Example dataset",
                    fillColor: "rgba(26,179,148,0.5)",
                    strokeColor: "rgba(26,179,148,0.7)",
                    pointColor: "rgba(26,179,148,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(26,179,148,1)",
                    data: [28, 48, 40, 19, 86, 27, 90]
                }
            ]
        };

    }
}