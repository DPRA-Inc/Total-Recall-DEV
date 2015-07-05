angular.module("TotalRecall").controller("deviceeventdialogcontroller", DeviceEventDialogController);

function DeviceEventDialogController($scope, $modalInstance, item, keyword) {
    var vm = this;

    vm.Keyword = keyword;
    vm.EventItem = item;
}