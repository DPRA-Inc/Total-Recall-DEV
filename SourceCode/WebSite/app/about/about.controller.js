angular.module("TotalRecall").controller("aboutcontroller", AboutController);

function AboutController($scope, $localStorage) {
    var vm = this;
    var fontSizeClass = "";

    if (angular.isString($localStorage.fontSizeClass)) {
    	vm.fontSizeClass = $localStorage.fontSizeClass;
    }
	
    vm.ChangeFontSize = function (className) {
        vm.fontSizeClass = className;
		$localStorage.fontSizeClass = className;
    }
}