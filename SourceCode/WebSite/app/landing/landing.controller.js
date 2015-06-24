angular.module('TotalRecall').controller('landingcontroller', landingcontroller);

function landingcontroller($scope, $window, $location, $localStorage, landingservice, feedLoader) {
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

    vm.feeds = [];

    vm.IsRSSLoading = true;

    vm.textValue = null;

    if (!angular.isObject(GlobalsModule.ShoppingList)) {
        if (angular.isString($localStorage.cart)) {
            try {
                GlobalsModule.ShoppingList = angular.fromJson($localStorage.cart); // Load from local storage
            } catch(ex) {
                GlobalsModule.ShoppingList = [];
            }
        } else {
            GlobalsModule.ShoppingList = [];
        }
    }

    vm.shoppingList = GlobalsModule.ShoppingList;

    if (vm.shoppingList.length == 0) LoadPageInfo();

    //********************************

    function LoadPageInfo() {

        // Here, lets go ahead and ping the service once. 
        // this make all searches 10x faster as there will be no loadup time.
        landingservice.GetIssues("TEST|TN",
            function (result) {

                // Do nothing.  Just warm it up!


            }
        );

    }

    vm.AddToList = function () {

        var disallowedChars = /[^a-zA-Z0-9 :]/g;

        if (angular.isString(vm.textValue) && vm.textValue.replace(disallowedChars, "").length > 0) {

            var value = vm.textValue.replace(disallowedChars, "");
            var region = "TN";

            // Check for duplicates
            vm.shoppingList.forEach(function (checkItem)
            {
                if (checkItem.Keyword == value)
                {
                    alert('Duplicate (todo: change this to ui style timeout message)');
                    throw new Error('Duplicate');
                }
            })

            // Make the new item to be added to our list.
            var item = {};

            item.Keyword = value; // Product name
            item.Rank = 'success'; // How Bad is it, Color Code.
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

            var searchStr = value + "|" + region;

            var data = landingservice.GetIssues(searchStr,
                function (result) {                                      

                    // Search for the Keyword in our list.
                    vm.shoppingList.forEach(function (product) {

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
    }

    vm.RemoveFromList= function(cartItem) {

        var itemIndex = vm.shoppingList.indexOf(cartItem);
        vm.shoppingList.splice(itemIndex, 1);

        $localStorage.cart = angular.toJson(vm.shoppingList); // Save cart to local storage.

    }

    vm.ViewProductDetails = function (product) {

        // set our shopping list to use later.
        GlobalsModule.ShoppingList = vm.shoppingList;

        if (!product.IsClean) {
            GlobalsModule.SearchSummary = product;
            $location.path('/index/product');
        }
    }

    vm.ViewFeed = function (feed)
    {
        $window.open(feed.link);
    }

    vm.StartRSS = function () {

        
        feedLoader.GetRSSFeed("http://www.fda.gov/AboutFDA/ContactFDA/StayInformed/RSSFeeds/Consumers/rss.xml").then(function(res){
            vm.feeds = res.data.responseData.feed.entries;
            vm.IsRSSLoading = false;
        });
           
    }

};



