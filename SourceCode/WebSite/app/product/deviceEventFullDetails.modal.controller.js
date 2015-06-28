angular.module("TotalRecall").controller("deviceeventdialogcontroller", DeviceEventDialogController);

function DeviceEventDialogController($scope, $modalInstance, item) {
    var vm = this;

    vm.EventItem = item;
}