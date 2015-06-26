angular.module("TotalRecall").controller("productcontroller", ProductController);

function ProductController($scope, $sessionStorage, $localStorage, $http, $modal, productservice) {
    var vm = this;

    vm.fontSizeClass = "";
    vm.lineOptions = [];
    vm.lineData = [];
    vm.SearchSummary = $sessionStorage.SearchSummary;
    vm.DataLoading = true;
    vm.Markers = [];
    vm.CurrentIndex = 0;
    vm.VisibleResults = [];
    vm.IsChartReady = false;
  
    // load last selection from local storage.
    if (angular.isString($localStorage.fontSizeClass)) {
        vm.fontSizeClass = $localStorage.fontSizeClass;
    }

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

    LoadChartInfo();
    LoadPageInfo();
   

    //*******************************************

    /*
     * Displays additional information about an reult item.
     */
    vm.ShowMoreInformation = function(item) {

        GlobalsModule.SearchResultItem = item;

        var modalInstance = $modal.open({
            templateUrl: "app/product/productFullDetails.modal.html",
            controller: "productcontroller as vm"

        });
    };

    /*
     * Used changes the fonts size.
     */
    vm.ChangeFontSize = function(className) {
        vm.fontSizeClass = className;
        $localStorage.fontSizeClass = className;
    };

    /*
     * Used to load only visible results to increase render performance.
     */
    vm.DisplayNext = function(numberToLoad) {
        for (var i = 0; i < numberToLoad; i++) {
            if (angular.isObject(vm.SearchResult) && angular.isObject(vm.SearchResult.Results)) {
                var allResults = vm.SearchResult.Results;

                if (vm.VisibleResults.length < allResults.length) {
                    vm.VisibleResults.push(allResults[vm.CurrentIndex]);
                    vm.CurrentIndex++;
                }
            }
        }
    };

    /*
     * Loads the page initial data.
     */
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

                vm.lineData.labels = result.GraphObjects.Labels;
                vm.lineData.datasets[0].data = result.GraphObjects.Data1;
                vm.lineData.datasets[1].data = result.GraphObjects.Data2;
                vm.lineData.datasets[2].data = result.GraphObjects.Data3;

                GlobalsModule.SearchResult = result;

                vm.SearchResult = result;
                vm.DisplayNext(5);
                vm.DataLoading = false;
                vm.IsChartReady = true;

            }
        );

    }

    /*
     * Initialize the chart.
     */
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
                    label: "Events in the Year",
                    fillColor: "rgba(255,179,148,0.5)",
                    strokeColor: "rgba(255,179,148,0.7)",
                    pointColor: "rgba(255,179,148,1)",
                    pointStrokeColor: "#222",
                    pointHighlightFill: "#333",
                    pointHighlightStroke: "rgba(0,220,220,1)",
                    data: []
                },
                {
                    label: "Events in the Year",
                    fillColor: "rgba(26,0,148,0.5)",
                    strokeColor: "rgba(26,0,148,0.7)",
                    pointColor: "rgba(26,0,148,1)",
                    pointStrokeColor: "#555",
                    pointHighlightFill: "#777",
                    pointHighlightStroke: "rgba(220,0,220,1)",
                    data: []
                },
                {
                    label: "Events in the Year",
                    fillColor: "rgba(26,179,0,0.5)",
                    strokeColor: "rgba(26,179,0,0.7)",
                    pointColor: "rgba(26,179,0,1)",
                    pointStrokeColor: "#aaa",
                    pointHighlightFill: "#bbb",
                    pointHighlightStroke: "rgba(220,220,0,1)",
                    data: []
                }
            ]
        };

    }
}