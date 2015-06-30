angular.module("TotalRecall").controller("drugeventdialogcontroller", DrugEventDialogController);

function DrugEventDialogController($scope, $modalInstance, item, keyword) {
    var vm = this;

    vm.Keyword = keyword;
    vm.Patient = item;
}