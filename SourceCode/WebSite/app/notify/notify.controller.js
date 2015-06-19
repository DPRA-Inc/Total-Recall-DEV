function notifyCtrl($scope, notify) {
    $scope.msg = 'Hello! This is a sample message!';
    $scope.demo = function () {
        notify({
            message: $scope.msg,
            classes: $scope.classes,
            templateUrl: $scope.template
        });
    };
    $scope.closeAll = function () {
        notify.closeAll();
    };

    $scope.notifyTemplate = 'views/common/notify.html';
    $scope.PopNotify = function (message) {
        notify({ message: message, classes: 'alert-info', templateUrl: $scope.inspiniaTemplate });
    }

}
