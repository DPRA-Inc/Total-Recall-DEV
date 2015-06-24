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

