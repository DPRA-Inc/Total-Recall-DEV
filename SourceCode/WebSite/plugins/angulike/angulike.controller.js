angular.module("TotalRecall").controller("angulikecontroller", AngulikeController);

function AngulikeController($location, $sessionStorage)
{
    var vm = this;

    var keyword = $sessionStorage.SearchSummary.Keyword;
    var region = $sessionStorage.SearchSummary.State;

    var url = "https://dev.fdachallenge.com/dontextendmebro/#/index/product?Keyword=" + keyword + "&Region=" + region;

    vm.Model1 = {
        // These need to set to our info.  So they click go straight to Product page for info.
        Url: url,
        Name: "ShopAware Developed by DPRA.", 
        ImageUrl: ''
    };
   
}
