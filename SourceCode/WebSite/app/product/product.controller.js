angular.module("TotalRecall").controller("productcontroller", ProductController);

function ProductController($scope, $location, $sessionStorage, $localStorage, $http, $modal, productservice, olData)
{
    var vm = this;

    vm.SearchResult = [];

    vm.SearchSummary = $sessionStorage.SearchSummary;

    if (!angular.isObject(vm.SearchSummary))
    {
        $location.path("/index");
        return;
    }

    vm.fontSizeClass = "";
    vm.lineOptions = [];
    vm.lineData1 = [];
    vm.lineData2 = [];
    vm.lineData3 = [];
    vm.lineDataE = [];
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
    if (angular.isString($localStorage.fontSizeClass))
    {
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
            },
            opacity: 0.5
        },
        class1: {
            visible: true,
            opacity: 0.5,
            source: {
                type: 'GeoJSON',
                geojson: {
                    object: []
                }
            },
            style: {
                fill: {
                    color: 'rgba(237, 85, 101, 0.9)'
                },
                stroke: {
                    color: 'white',
                    width: 1
                }
            }
        },
        class2: {
            visible: true,
            opacity: 0.5,
            source: {
                type: 'GeoJSON',
                geojson: {
                    object: []
                }
            },
            style: {
                fill: {
                    color: 'rgba(248, 172, 89, 0.9)'
                },
                stroke: {
                    color: 'white',
                    width: 1
                }
            }
        },
        class3: {
            visible: true,
            opacity: 0.5,
            source: {
                type: 'GeoJSON',
                geojson: {
                    object: []
                }
            },
            style: {
                fill: {
                    color: 'rgba(28, 132, 198, 0.9)'
                },
                stroke: {
                    color: 'white',
                    width: 1
                }
            }
        },
        events: {
            visible: true,
            opacity: 0.5,
            source: {
                type: 'GeoJSON',
                geojson: {
                    object: []
                }
            },
            style: {
                fill: {
                    color: 'rgba(35, 198, 200, 0.9)'
                },
                stroke: {
                    color: 'white',
                    width: 1
                }
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
     * Used changes the fonts size.
     */
    vm.ChangeFontSize = function (className)
    {
        vm.fontSizeClass = className;
        $localStorage.fontSizeClass = className;
    };

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
     * Loads the page initial data.
     */
    function LoadPageInfo()
    {

        var scrubText = vm.SearchSummary.ScrubedText;
        var productName = vm.SearchSummary.Keyword;
        var region = vm.SearchSummary.State;

        vm.EventsVisible = (vm.SearchSummary.EventCount);
        vm.Class1Visible = (vm.SearchSummary.ClassICount);
        vm.Class2Visible = (vm.SearchSummary.ClassIICount);
        vm.Class3Visible = (vm.SearchSummary.ClassIIICount);

        productservice.GetFDAResults(scrubText, region,
            function (result)
            {
                var states = [];

                if (angular.isObject(result) && angular.isObject(result.MapObjects))
                {
                    result.MapObjects.forEach(function (mapItem)
                    {
                        states.push(mapItem.State);
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

                //var link = "json/" + mapItem.State + ".geo.json.txt";
                //var link = "json/" + "Test" + ".geo.json.txt";
                //// load json test
                //$http.get(link).success(function (data) {

                //    var poly = data;

                //    $scope.class2.source.geojson = {
                //        object: poly
                //    }

                //});

                productservice.GetRegionsJson(states, function (jsonData)
                {

                    $scope.class2.source.geojson = {
                        object: jsonData
                    }

                    // re-zoom and center based on the objects on the map
                    olData.getMap().then(function (map)
                    {

                        var size = map.getSize();

                        size = [size[0] * 2, size[1] * 2];

                        var extent = map.getView().calculateExtent(map.getSize());
                        map.getView().fitExtent(extent, size);



                        //// load json test
                        //$http.get('json/tn.txt').success(function (data)
                        //{

                        //    var tn = data;
                        //    $scope.class2.source.geojson = {
                        //        object: tn
                        //    }

                        //});

                    });

                });

            }
        );

    }

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
}