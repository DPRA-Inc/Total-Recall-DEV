angular.module("TotalRecall").controller('productcontroller', ProductController);

function ProductController($scope, $http, $modal, productservice)
{
    var vm = this;

    vm.SearchSummary = GlobalsModule.SearchSummary;
    vm.DataLoading = true;

    vm.Markers = [];

    // default map configuration
    angular.extend($scope, {
        center: {
            lat: 39.50,
            lon: -98.35,
            zoom: 2
        },
        defaults: {
            interactions: {
                mouseWheelZoom: true
            },
            controls: {
                zoom: false,
                rotate: false,
                attribution: false
            }
        },
        mapquest: {
            source: {
                type: 'OSM'
            }
        }
    });

    if (!angular.isObject(GlobalsModule.SearchResult)) GlobalsModule.SearchResult = [];
    vm.SearchResult = GlobalsModule.SearchResult;

    if (!angular.isObject(GlobalsModule.SearchResultItem)) GlobalsModule.SearchResultItem = [];
    vm.SearchResultItem = GlobalsModule.SearchResultItem;
    if (GlobalsModule.SearchResult.length === 0) LoadPageInfo();

    //*******************************************

    function LoadPageInfo()
    {

        var productName = vm.SearchSummary.Keyword;
        var region = "TN";

        //icon: {
        //        anchor: [0.5, 0.5],
        //        anchorXUnits: 'fraction',
        //        anchorYUnits: 'fraction',
        //        opacity: 0.85,
        //        src: 'img/mapIcon/' + mapIcon + '.png'
        //}

        productservice.GetSearchResult(productName, region,
            function (result)
            {

                if (angular.isObject(result))
                {

                    result.ClassI.forEach(function (item)
                    {
                        item.ShowMoreInformation = false;
                    });



                    result.MapObjects.forEach(function (mapItem)
                    {

                        //  var transLoc = ol.proj.transform([mapItem.Longitude, mapItem.Latitude], 'EPSG:4326', 'EPSG:3857');

                        vm.Markers.push(
                           {
                               lat: parseFloat(mapItem.Latitude),
                               lon: parseFloat(mapItem.Longitude),
                               label: {
                                   message: '',
                                   show: false,
                                   showOnMouseOver: true
                               },
                               style: {
                                   image: {
                                       icon: mapItem.icon
                                   }
                               }
                           }
                        )
                    })

                }

                GlobalsModule.SearchResult = result;
                vm.SearchResult = result;
                vm.DataLoading = false;
            }
        );

    }

    vm.ShowMoreInformation = function (item)
    {

        GlobalsModule.SearchResultItem = item;

        var modalInstance = $modal.open({
            templateUrl: 'app/product/productFullDetails.modal.html',
            controller: 'productcontroller as vm'
        });


    };
};

