angular.module("TotalRecall").controller("productdialogcontroller", ProductDialogController);

function ProductDialogController($scope, $modalInstance, item, keyword) {
    var vm = this;

    vm.Keyword = keyword;
    vm.SearchResultItem = item;
}