angular.module("TotalRecall").controller("drugeventdialogcontroller", DrugEventDialogController);

function DrugEventDialogController($scope, $modalInstance, item) {
    var vm = this;

    vm.Patient = item;
}