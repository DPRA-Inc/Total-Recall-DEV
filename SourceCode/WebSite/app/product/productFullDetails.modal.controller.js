angular.module("TotalRecall").controller("productdialogcontroller", ProductDialogController);

function ProductDialogController($scope, $modalInstance, item) {
    var vm = this;

    vm.SearchResultItem = item;
}