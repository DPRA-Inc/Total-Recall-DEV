angular.module('TotalRecall').controller('productcontroller', ProductController)

function ProductController($http, $modal, productservice) {
    var vm = this;
           
    vm.Product = GlobalsModule.SelectedProduct;
    vm.ProductName = vm.Product.Keyword;
    vm.DataLoading = true;
    
    vm.Details = null;

    LoadPageInfo();

    //*******************************************

    function LoadPageInfo() {

        var productName = vm.ProductName;
        var region = "TN";

        productservice.GetSearchResult(productName, region,
            function (result) {

                if (result != null) {

                    result.ClassI.forEach(function (item) {
                        item.ShowMoreInformation = false;
                    });

                }

                vm.Details = result;
                vm.DataLoading = false;
            }            
        );       
    }

    vm.SetupMap = function() {

        //vm.Details.MapObjects.forEach(function (obj) {

        //    vectorSource.addFeature(createIcon(obj.Latitude, obj.Longitude, obj.Tooltip, obj.Image));

        //});

        //vectorSource.addFeature(createIcon(36, -84, 'Knoxville, TN', 'images/check.png'));
        //vectorSource.addFeature(createIcon(35, -85.3, 'Chattanooga, TN', 'images/check.png'));

        //centerMapOnAllObjects();


    }

    vm.ShowMoreInformation = function (item) {
        var modalInstance = $modal.open({
            templateUrl: 'fulldetails.html',
            controller: ModalInstanceCtrl
        });

    }

};

