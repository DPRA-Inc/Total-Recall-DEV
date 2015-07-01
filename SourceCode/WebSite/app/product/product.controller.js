angular.module("TotalRecall").controller("productcontroller", ProductController);

function ProductController($scope, $location, $sessionStorage, $localStorage, $http, $modal, productservice, olData, landingservice)
{
    var vm = this;

    vm.DataLoading = true;

    vm.SearchSummary = [];
    vm.SearchResult = [];
    vm.Keyword = [];
    vm.Region = [];
    vm.fontSizeClass = "";
    vm.lineOptions = [];
    vm.lineData1 = [];
    vm.lineData2 = [];
    vm.lineData3 = [];
    vm.lineDataE = [];

    vm.Markers = [];
    vm.CurrentIndex = 0;
    vm.VisibleResults = [];
    vm.IsChartReady = false;

    vm.Class1Visible = false;
    vm.Class2Visible = false;
    vm.Class3Visible = false;
    vm.EventsVisible = false;

    vm.PillNormal = "badge-plain";
    vm.PillBig = "badge-plain";
    vm.PillBigger = "badge-plain";

    // load last selection from local storage.
    if (angular.isString($localStorage.fontSizeClass))
    {
        vm.fontSizeClass = $localStorage.fontSizeClass;
    }

    AnalyzeFontSize(vm.fontSizeClass);

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
        class1: {
            visible: true,
            opacity: 1,
            source: {
                type: 'GeoJSON',
                geojson: {
                    object: []
                }
            },
            style: {
                fill: {
                    color: 'rgba(237, 85, 101, 1)'
                },
                stroke: {
                    color: 'white',
                    width: 1
                }
            }
        },
        class2: {
            visible: true,
            opacity: 1,
            source: {
                type: 'GeoJSON',
                geojson: {
                    object: []
                }
            },
            style: {
                fill: {
                    color: 'rgba(248, 172, 89, 1)'
                },
                stroke: {
                    color: 'white',
                    width: 1
                }
            }
        },
        class3: {
            visible: true,
            opacity: 1,
            source: {
                type: 'GeoJSON',
                geojson: {
                    object: []
                }
            },
            style: {
                fill: {
                    color: 'rgba(28, 132, 198, 1)'
                },
                stroke: {
                    color: 'white',
                    width: 1
                }
            }
        },
        events: {
            visible: true,
            opacity: 1,
            source: {
                type: 'GeoJSON',
                geojson: {
                    object: []
                }
            },
            style: {
                fill: {
                    color: 'rgba(35, 198, 200, 1)'
                },
                stroke: {
                    color: 'white',
                    width: 1
                }
            }
        },
        osm: {
            source: {
                type: "OSM"
            },
            opacity: .33
        }

    });

    LoadDefaultSummary();    
    LoadChartInfo();

    // Check if we have a query
    var links = $location.search();

    if (angular.isString(links.Keyword)) vm.Keyword = links.Keyword;
    if (angular.isString(links.Region)) vm.Region = links.Region;

    if (vm.Keyword.length > 0) {
        vm.SearchSummary = $sessionStorage.SearchSummary;

        var doSummary = true;

        if (angular.isObject(vm.SearchSummary)) {
            if (vm.SearchSummary.Keyword === vm.Keyword) {
                if (vm.SearchSummary.State === vm.Region) {
                    doSummary = false;
                }
            }
        }

        if (doSummary) {

            landingservice.QuickSearch(vm.Keyword, vm.Region,
                function (summary) {
                    summary.IsLoading = false;
                    vm.SearchSummary = summary;
                    $sessionStorage.SearchSummary = vm.SearchSummary;                    
                    LoadPageInfo();
                }
            );
        }
        else {
            LoadPageInfo();
        }
     }
    else {
        vm.SearchSummary = $sessionStorage.SearchSummary;


        if (!angular.isObject(vm.SearchSummary)) {
            $location.path("/index");
            return;
        }
        else {
            vm.Keyword = vm.SearchSummary.Keyword;
            vm.Region = vm.SearchSummary.State;
        }

        LoadPageInfo();
    }
   
    //*******************************************

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
            function (result) {
                var classiStates = [];
                var classiiStates = [];
                var classiiiStates = [];
                var eventStates = [];

                if (angular.isObject(result) && angular.isObject(result.MapObjects)) {
                    result.MapObjects.forEach(function (mapItem) {
                        switch (mapItem.Rank) {
                            case 1:
                                classiStates.push(mapItem.State)
                                break;
                            case 2:
                                classiiStates.push(mapItem.State)
                                break;
                            case 3:
                                classiiiStates.push(mapItem.State)
                                break;
                            default:
                                eventStates.push(mapItem.State)
                                break;
                        }

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

                vm.lineData1.labels = result.GraphObjects.Labels;
                vm.lineData1.datasets[0].data = result.GraphObjects.Data1;

                vm.lineData2.labels = result.GraphObjects.Labels;
                vm.lineData2.datasets[0].data = result.GraphObjects.Data2;

                vm.lineData3.labels = result.GraphObjects.Labels;
                vm.lineData3.datasets[0].data = result.GraphObjects.Data3;

                vm.lineDataE.labels = result.GraphObjects.Labels;
                vm.lineDataE.datasets[0].data = result.GraphObjects.DataE;

                GlobalsModule.SearchResult = result;

                vm.SearchResult = result;
                vm.DisplayNext(5);
                vm.DataLoading = false;
                vm.IsChartReady = true;

                productservice.GetRegionsJson(classiStates, function (jsonData) {
                    $scope.class1.source.geojson = {
                        object: jsonData
                    }
                });

                productservice.GetRegionsJson(classiiStates, function (jsonData) {
                    $scope.class2.source.geojson = {
                        object: jsonData
                    }
                });

                productservice.GetRegionsJson(classiiiStates, function (jsonData) {
                    $scope.class3.source.geojson = {
                        object: jsonData
                    }
                });

                productservice.GetRegionsJson(eventStates, function (jsonData) {
                    $scope.events.source.geojson = {
                        object: jsonData
                    }
                });

                // re-zoom and center based on the objects on the map
                olData.getMap().then(function (map) {

                    var size = map.getSize();

                    size = [size[0] * 2, size[1] * 2];

                    var extent = map.getView().calculateExtent(map.getSize());
                    map.getView().fitExtent(extent, size);

                });

            }
        );

    }

    /*
     * Displays additional information about an reult item.
     */
    vm.ShowMoreInformation = function (item)
    {

        if (item.Classification.lastIndexOf("Class", 0) === 0)
        {
            $modal.open({
                templateUrl: "app/product/productFullDetails.modal.html",
                controller: "productdialogcontroller as vm",
                windowClass: "animated fadeInLeft",
                resolve: {
                    item: function ()
                    {
                        return item;
                    },
                    keyword: function ()
                    {
                        return vm.SearchSummary.Keyword;
                    }
                }
            });
            return;
        }

        if (item.Classification.lastIndexOf("Event", 0) === 0)
        {

            $modal.open({
                templateUrl: "app/product/drugEventFullDetails.modal.html",
                controller: "drugeventdialogcontroller as vm",
                size: "lg",
                windowClass: "animated fadeInLeft",
                resolve: {
                    item: function ()
                    {
                        return item;
                    },
                    keyword: function ()
                    {
                        return vm.SearchSummary.Keyword;
                    }
                }
            });
            return;
        }

        if (item.Classification.lastIndexOf("Device", 0) === 0)
        {

            $modal.open({
                templateUrl: "app/product/deviceEventFullDetails.modal.html",
                controller: "deviceeventdialogcontroller as vm",
                size: "lg",
                windowClass: "animated fadeInLeft",
                resolve: {
                    item: function ()
                    {
                        return item;
                    },
                    keyword: function ()
                    {
                        return vm.SearchSummary.Keyword;
                    }
                }
            });
            return;
        }

    };

    /*
     * Used to change the fonts size.
     */
    vm.ChangeFontSize = function (className)
    {
        vm.fontSizeClass = className;
        $localStorage.fontSizeClass = className;

        AnalyzeFontSize(className);
    };

    /*
     * Used to analyze and properly set up a selector style for the font size.
     */
    function AnalyzeFontSize(fontSize)
    {
        vm.PillNormal = "badge-plain";
        vm.PillBig = "badge-plain";
        vm.PillBigger = "badge-plain";

        switch (fontSize)
        {
            case "big":
                vm.PillBig = "badge-primary";
                break;
            case "bigger":
                vm.PillBigger = "badge-primary";
                break;
            default:
                vm.PillNormal = "badge-primary";
                break;
        }
    }

    /*
     * Used to load only visible results to increase render performance.
     */
    vm.DisplayNext = function (numberToLoad)
    {
        for (var i = 0; i < numberToLoad; i++)
        {
            if (angular.isObject(vm.SearchResult) && angular.isObject(vm.SearchResult.Results))
            {
                var allResults = vm.SearchResult.Results;

                if (vm.VisibleResults.length < allResults.length)
                {
                    vm.VisibleResults.push(allResults[vm.CurrentIndex]);
                    vm.CurrentIndex++;
                }
            }
        }
    };

    /*
     * Initialize the chart.
     */
    function LoadChartInfo()
    {

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

        vm.lineData1 = {
            labels: [""],
            datasets: [
                {
                    label: "Class 1",
                    fillColor: "rgba(237,85,101,1)",
                    strokeColor: "rgba(237,85,101,1)",
                    pointColor: "rgba(237,85,101,1)",
                    pointStrokeColor: "#222",
                    pointHighlightFill: "#DDD",
                    pointHighlightStroke: "rgba(237,85,101,1)",
                    data: []
                }
            ]
        };

        vm.lineData2 = {
            labels: [""],
            datasets: [
                {
                    label: "Class 2",
                    fillColor: "rgba(248,172,89,1)",
                    strokeColor: "rgba(248,172,89,1)",
                    pointColor: "rgba(248,172,89,1)",
                    pointStrokeColor: "#222",
                    pointHighlightFill: "#DDD",
                    pointHighlightStroke: "rgba(248,172,89,1)",
                    data: []
                }
            ]
        };

        vm.lineData3 = {
            labels: [""],
            datasets: [
                {
                    label: "Class 3",
                    fillColor: "rgba(28,132,198,1)",
                    strokeColor: "rgba(28,132,198,1)",
                    pointColor: "rgba(28,132,198,1)",
                    pointStrokeColor: "#222",
                    pointHighlightFill: "#DDD",
                    pointHighlightStroke: "rgba(28,132,198,1)",
                    data: []
                }
            ]
        };

        vm.lineDataE = {
            labels: [""],
            datasets: [
                {
                    label: "Events",
                    fillColor: "rgba(35,198,200,1)",
                    strokeColor: "rgba(35,198,200,1)",
                    pointColor: "rgba(35,198,200,1)",
                    pointStrokeColor: "#222",
                    pointHighlightFill: "#DDD",
                    pointHighlightStroke: "rgba(35,198,200,1)",
                    data: []
                }
            ]
        };

    }

    function LoadDefaultSummary() {

        var item = [];
        item.ScrubedText = ""; // Product name
        item.Keyword = "";
        item.State = "";
        item.Rank = ""; // How Bad is it, Color Code.
        item.IsLoading = true; // Indicates we are waiting on Return From Service.
        item.HasClassI = false; // Indicates there is some Class I Data to show.
        item.HasClassII = false;
        item.HasClassIII = false;
        item.HasEvents = false;
        item.ClassICount = 0;
        item.ClassIICount = 0;
        item.ClassIIICount = 0;
        item.EventCount = 0;
        item.IsClean = false;

        vm.SearchSummary = item;
    }
}