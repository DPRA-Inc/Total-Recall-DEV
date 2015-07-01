angular.module("TotalRecall").controller("angulikecontroller", AngulikeController);

function AngulikeController($location, $sessionStorage)
{
    var vm = this;
    var region = "";
    var keyword = "";

    // Check if we have a query
    var links = $location.search();

    if (angular.isString(links.Keyword)) keyword = links.Keyword;
    if (angular.isString(links.Region)) region = links.Region;

    //var keyword = $sessionStorage.SearchSummary.ScrubedText;
    //var region = $sessionStorage.SearchSummary.State;

    //var mainUrl = "https://www.fdachallenge.com";
    var mainUrl = "https://dev.fdachallenge.com/dontextendmebro";
    
    var url = mainUrl + "/#/index/product?Keyword=" + keyword + "&Region=" + region;
    var imageURl = mainUrl + "/img/logo.png";

    $sessionStorage.AbsuloteURL = url;

    vm.Model1 = {
        // These need to set to our info.  So they click go straight to Product page for info.
        Url: url,
        Name: "ShopAware!", 
        ImageUrl: imageURl
    };
   
}
