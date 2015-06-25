angular.module("TotalRecall").controller("privacycontroller", PrivacyController);

function PrivacyController($scope, $localStorage)
{
    var vm = this;
    var fontSizeClass = "";

    if (angular.isString($localStorage.fontSizeClass))
    {
        vm.fontSizeClass = $localStorage.fontSizeClass;
    }

    vm.ChangeFontSize = function (className)
    {
        vm.fontSizeClass = className;
        $localStorage.fontSizeClass = className;
    }
}