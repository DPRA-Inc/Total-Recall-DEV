angular.module('TotalRecall').controller('productcontroller', ProductController)

function ProductController($http, productservice) {
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

                if (result == null) {

                    


                }

                vm.Details = result;
                vm.DataLoading = false;
            }            
        );       
    }



    vm.GetMoreInformation = function (item) {

        item.ShowMoreInformation = true;
      


    }

};

