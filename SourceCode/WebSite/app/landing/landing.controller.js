angular.module("TotalRecall").controller("landingcontroller", landingcontroller);

function landingcontroller($scope, $window, $location, $sessionStorage, $localStorage, landingservice, feedLoader) {
    var vm = this;

    vm.fontSizeClass = "";
    vm.feeds = [];
    vm.IsRSSLoading = true;
    vm.textValue = null;
    vm.states = [];
    vm.selectedState = null;

    if (angular.isString($localStorage.fontSizeClass)) {
        vm.fontSizeClass = $localStorage.fontSizeClass;
    }

    vm.ChangeFontSize = function(className) {
        vm.fontSizeClass = className;
        $localStorage.fontSizeClass = className;
    };

    if (angular.isString($localStorage.cart)) {
        try {
            vm.shoppingList = angular.fromJson($localStorage.cart); // Load from local storage
        } catch (ex) {
            vm.shoppingList = [];
        }
    } else {
        vm.shoppingList = [];
    }

    /*
     * Load the initial page data.
     */
    function LoadPageInfo() {
        landingservice.GetStates(
            function(result) {
                vm.states = result;

                if (angular.isString($localStorage.selectedState)) {
                    // Set to the previously selected value if available.
                    vm.selectedState = $localStorage.selectedState;
                } else {
                    vm.selectedState = "All";
                }

            }
        );
    }

    /*
     * Workaround. For some reason the first call can be much slower so we will call it the first time 
     * behind the scenes to make it faster when the user add an item.
     */
    function WarmUp() {
        landingservice.QuickSearch("TEST", "TN",
            function(result) {
                // Do nothing.  Just warm it up!
            }
        );
    }

    if (vm.shoppingList.length === 0) WarmUp();

    LoadPageInfo();

    //********************************

    /*
     * Adds an item to the shipping cart as well as getting recall detail.
     */
    vm.AddToList = function() {

        $localStorage.selectedState = vm.selectedState; // Remember the state that was selected.

        var disallowedChars = /[^a-zA-Z0-9 :]/g;

        if (angular.isString(vm.textValue) && vm.textValue.replace(disallowedChars, "").length > 0) {

            var value = vm.textValue.replace(disallowedChars, "");
            var region = vm.selectedState;

            // Check for duplicates
            vm.shoppingList.forEach(function(checkItem) {
                if (checkItem.Keyword === value) {
                    alert("Duplicate (todo: change this to ui style timeout message)");
                    throw new Error("Duplicate");
                }
            }); // Make the new item to be added to our list.
            var item = {};

            item.Keyword = value; // Product name
            item.State = region;
            item.Rank = "success"; // How Bad is it, Color Code.
            item.IsLoading = true; // Indicates we are waiting on Return From Service.
            item.HasClassI = false; // Indicates there is some Class I Data to show.
            item.HasClassII = false;
            item.HasClassIII = false;
            item.HasEvents = false;
            item.ClassICount = 0;
            item.ClassIICount = 0;
            item.ClassIIICount = 0;
            item.EventCount = 0;
            item.IsClean = true;

            vm.shoppingList.push(item);

            item.IsLoading = true; // Set to true to show that information about the item is loading.

            vm.textValue = "";
           
            landingservice.QuickSearch(value, region,
                function(result) {

                    // Search for the Keyword in our list.
                    vm.shoppingList.forEach(function(product) {

                        if (product.Keyword === result.Keyword) {

                            product.ClassIDescription = result.ClassIDescription;
                            product.ClassIIDescription = result.ClassIIDescription;
                            product.ClassIIIDescription = result.ClassIIIDescription;

                            if (result.EventCount > 0) {
                                product.HasEvents = true;
                                product.EventCount = result.EventCount;
                                product.IsClean = false;
                                product.Rank = "success";
                            }

                            if (result.ClassIIICount > 0) {
                                product.HasClassIII = true;
                                product.ClassIIICount = result.ClassIIICount;
                                product.IsClean = false;
                                product.Rank = "info";
                            }

                            if (result.ClassIICount > 0) {
                                product.HasClassII = true;
                                product.ClassIICount = result.ClassIICount;
                                product.IsClean = false;
                                product.Rank = "warning";
                            }

                            if (result.ClassICount > 0) {
                                product.HasClassI = true;
                                product.ClassICount = result.ClassICount;
                                product.IsClean = false;
                                product.Rank = "danger";
                            }

                            product.IsLoading = false;
                        }

                        $localStorage.cart = angular.toJson(vm.shoppingList); // Save cart to local storage.
                    });
                }
            );
        }
    };

    /*
     * Removed a item from the shopping cart.
     */
    vm.RemoveFromList = function(cartItem) {
        var itemIndex = vm.shoppingList.indexOf(cartItem);
        vm.shoppingList.splice(itemIndex, 1);

        $localStorage.cart = angular.toJson(vm.shoppingList); // Save cart to local storage.
    };

    /*
     *  Navigates to details related to the selected product
     */
    vm.ViewProductDetails = function(product) {
        if (!product.IsClean) {
            $sessionStorage.SearchSummary = product;
            $location.path("/index/product");
        }
    };

    /*
     * Navigates to the FDA site for additional details on the selected feed.
     */
    vm.ViewFeed = function(feed) {
        $window.open(feed.link);
    };

    /*
     * Loads the feed data.
     */
    vm.StartRSS = function() {
        feedLoader.GetRSSFeed("http://www.fda.gov/AboutFDA/ContactFDA/StayInformed/RSSFeeds/Consumers/rss.xml").then(function(res) {
            vm.feeds = res.data.responseData.feed.entries;
            vm.IsRSSLoading = false;
        });
    };
}