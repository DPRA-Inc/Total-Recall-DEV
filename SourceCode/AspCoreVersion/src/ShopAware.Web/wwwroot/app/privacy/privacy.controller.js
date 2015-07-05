angular.module("TotalRecall").controller("privacycontroller", PrivacyController);

function PrivacyController($scope, $localStorage)
{
    var vm = this;
    var fontSizeClass = "";

    vm.PillNormal = "badge-plain";
    vm.PillBig = "badge-plain";
    vm.PillBigger = "badge-plain";

    // load last selection from local storage.
    if (angular.isString($localStorage.fontSizeClass))
    {
        vm.fontSizeClass = $localStorage.fontSizeClass;
    }

    AnalyzeFontSize(vm.fontSizeClass);

    /*
     * Used to change the fonts size.
     */
    vm.ChangeFontSize = function (className)
    {
        vm.fontSizeClass = className;
        $localStorage.fontSizeClass = className;

        AnalyzeFontSize(className);
    };

    /*
     * Used to analyze and properly set up a selector style for the font size.
     */
    function AnalyzeFontSize(fontSize)
    {
        vm.PillNormal = "badge-plain";
        vm.PillBig = "badge-plain";
        vm.PillBigger = "badge-plain";

        switch (fontSize)
        {
            case "big":
                vm.PillBig = "badge-primary";
                break;
            case "bigger":
                vm.PillBigger = "badge-primary";
                break;
            default:
                vm.PillNormal = "badge-primary";
                break;
        }
    }

}