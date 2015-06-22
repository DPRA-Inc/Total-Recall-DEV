angular.module('TotalRecall').controller('productcontroller', ProductController)

function ProductController($http, productservice) {
    var vm = this;
           
    vm.Product = GlobalsModule.SelectedProduct;
    vm.ProductName = GlobalsModule.SelectedProduct.KeyWord;
    vm.DataLoading = true;

    vm.GetMoreInformation = function() {
      
    }

};

