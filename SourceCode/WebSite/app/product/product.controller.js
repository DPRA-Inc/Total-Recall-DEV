angular.module("TotalRecall").controller("productcontroller", ProductController);

function ProductController($scope, $sessionStorage, $localStorage, $http, $modal, productservice) {
    var vm = this;

    vm.SearchResult = [];

    vm.fontSizeClass = "";
    vm.lineOptions = [];
    vm.lineData = [];
    vm.SearchSummary = $sessionStorage.SearchSummary;
    vm.DataLoading = true;
    vm.Markers = [];
    vm.CurrentIndex = 0;
    vm.VisibleResults = [];
    vm.IsChartReady = false;

    vm.Class1Visible = false;
    vm.Class2Visible = false;
    vm.Class3Visible = false;
    vm.EventsVisible = false;
  
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

    //if (!angular.isObject(GlobalsModule.SearchResult)) GlobalsModule.SearchResult = [];
    //vm.SearchResult = GlobalsModule.SearchResult;

    if (!angular.isObject(GlobalsModule.SearchResultItem)) GlobalsModule.SearchResultItem = [];
    vm.SearchResultItem = GlobalsModule.SearchResultItem;

    LoadChartInfo();
    LoadPageInfo();
   

    //*******************************************

    /*
     * Displays additional information about an reult item.
     */
    vm.ShowMoreInformation = function (item) {

        if (item.Classification.lastIndexOf("Class", 0) === 0) {
            $modal.open({
                templateUrl: "app/product/productFullDetails.modal.html",
                controller: "productdialogcontroller as vm",
                resolve: {
                    item: function () {
                        return item;
                    }
                }
            });
            return;
        }

        if (item.Classification.lastIndexOf("Event", 0) === 0) {

            $modal.open({
                templateUrl: "app/product/drugEventFullDetails.modal.html",
                controller: "drugeventdialogcontroller as vm",
                size: "lg",
                resolve: {
                    item: function () {
                        return item;
                    }
                }
            });
            return;
        }
        
        //if (item.Classification.lastIndexOf("Device", 0) === 0) {

        //    $modal.open({
        //        templateUrl: "app/product/drugEventFullDetails.modal.html",
        //        controller: "drugeventdialogcontroller as vm",
        //        size: "lg",
        //        resolve: {
        //            item: function () {
        //                return item;
        //            }
        //        }
        //    });
        //    return;
        //}

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

        var scrubText = vm.SearchSummary.ScrubedText;
        var productName = vm.SearchSummary.Keyword;
        var region = vm.SearchSummary.State;

        vm.EventsVisible = (vm.SearchSummary.EventCount);
        vm.Class1Visible = (vm.SearchSummary.ClassICount);
        vm.Class2Visible = (vm.SearchSummary.ClassIICount);
        vm.Class3Visible = (vm.SearchSummary.ClassIIICount);
      
        productservice.GetFDAResults(scrubText, region,
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
                vm.lineData.datasets[3].data = result.GraphObjects.DataE;

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
                    label: "Class 1",
                    fillColor: "rgba(237,85,101,0.3)",
                    strokeColor: "rgba(237,85,101,0.7)",
                    pointColor: "rgba(237,85,101,1)",
                    pointStrokeColor: "#222",
                    pointHighlightFill: "#DDD",
                    pointHighlightStroke: "rgba(237,85,101,1)",
                    data: []
                },
                {
                    label: "Class 2",
                    fillColor: "rgba(248,172,89,0.3)",
                    strokeColor: "rgba(248,172,89,0.7)",
                    pointColor: "rgba(248,172,89,1)",
                    pointStrokeColor: "#222",
                    pointHighlightFill: "#DDD",
                    pointHighlightStroke: "rgba(248,172,89,1)",
                    data: []
                },
                {
                    label: "Class 3",
                    fillColor: "rgba(28,132,198,0.3)",
                    strokeColor: "rgba(28,132,198,0.7)",
                    pointColor: "rgba(28,132,198,1)",
                    pointStrokeColor: "#222",
                    pointHighlightFill: "#DDD",
                    pointHighlightStroke: "rgba(28,132,198,1)",
                    data: []
                },
                {
                    label: "Events",
                    fillColor: "rgba(35,198,200,0.3)",
                    strokeColor: "rgba(35,198,200,0.7)",
                    pointColor: "rgba(35,198,200,1)",
                    pointStrokeColor: "#222",
                    pointHighlightFill: "#DDD",
                    pointHighlightStroke: "rgba(35,198,200,1)",
                    data: []
                }
            ]
        };

    }
}