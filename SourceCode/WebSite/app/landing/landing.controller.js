angular.module("TotalRecall").controller("landingcontroller", landingcontroller);

function landingcontroller($location, $sessionStorage, $localStorage, landingservice, feedLoader, notify)
{
    var vm = this;

    vm.fontSizeClass = "";
    vm.feeds = [];
    vm.IsRSSLoading = true;
    vm.textValue = null;
    vm.states = [];
    vm.selectedState = null;

    vm.HasItems = false;

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

    if (angular.isString($localStorage.cart))
    {
        try
        {
            vm.shoppingList = angular.fromJson($localStorage.cart); // Load from local storage
        } catch (ex)
        {
            vm.shoppingList = [];
        }
    } else
    {
        vm.shoppingList = [];
    }

    /*
     * Load the initial page data.
     */
    function LoadPageInfo()
    {
        WarmUp();

        landingservice.GetStates(
            function (result)
            {
                vm.states = result;

                if (angular.isString($localStorage.selectedState))
                {
                    // Set to the previously selected value if available.
                    vm.selectedState = $localStorage.selectedState;
                } else
                {
                    vm.selectedState = "All";
                }

            }
        );

        StartupRSS();
        BuildLegend();

    }

    /*
     * Workaround. For some reason the first call can be much slower so we will call it the first time 
     * behind the scenes to make it faster when the user add an item. (TEST2 so no hits)
     */
    function WarmUp()
    {
        landingservice.QuickSearch("TEST2", "TN",
            function (result)
            {
                // Do nothing.  Just warm it up!
            }
        );
    }

    LoadPageInfo();

    //********************************

    /*
     * Adds an item to the shipping cart as well as getting recall detail.
     */
    vm.AddToList = function ()
    {

        $localStorage.selectedState = vm.selectedState; // Remember the state that was selected.

        var disallowedChars = /[^a-zA-Z0-9 :]/g;

        if (angular.isString(vm.textValue) && vm.textValue.replace(disallowedChars, "").trim().length > 0)
        {

            var value = vm.textValue;
            var region = vm.selectedState;

            var doesExist = false;

            // Check for duplicates
            vm.shoppingList.forEach(function (checkItem)
            {
                if ((checkItem.Keyword.toLowerCase() === vm.textValue.toLowerCase()) && (checkItem.State === region))
                {
                    toastr.options = {
                        "closeButton": true,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": true,
                        "positionClass": "toast-bottom-left",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }

                    toastr["warning"]("This item already exists in your shopping list.", "Duplicate item!");

                    doesExist = true;
                    return;
                }
            }); // Make the new item to be added to our list.

            if (doesExist) return;

            var item = {};

            item.IsLoading = true; // Set to true to show that information about the item is loading.

            item.ScrubedText = value; // Product name
            item.Keyword = vm.textValue;
            item.State = region;
            item.Rank = ""; // How Bad is it, Color Code.
            item.IsLoading = true; // Indicates we are waiting on Return From Service.
            item.HasClassI = false; // Indicates there is some Class I Data to show.
            item.HasClassII = false;
            item.HasClassIII = false;
            item.HasEvents = false;
            item.ClassICount = 0;
            item.ClassIICount = 0;
            item.ClassIIICount = 0;
            item.EventCount = 0;
            item.IsClean = false;

            vm.shoppingList.push(item);

            vm.HasItems = true;

            vm.textValue = ""; // reset input

            landingservice.QuickSearch(value, region,
                function (result)
                {

                    var isClean = true;

                    if (result.EventCount > 0)
                    {
                        vm.EventsVisible = true;
                        isClean = false;
                    }

                    if (result.ClassIIICount > 0)
                    {
                        vm.Class3Visible = true;
                        isClean = false;
                    }

                    if (result.ClassIICount > 0)
                    {
                        vm.Class2Visible = true;
                        isClean = false;
                    }

                    if (result.ClassICount > 0)
                    {
                        vm.Class1Visible = true;
                        isClean = false;
                    }

                    result.IsLoading = false;

                    result.IsClean = isClean;

                    var index = 0;

                    while (vm.shoppingList.length > index)
                    {
                        var product = vm.shoppingList[index];

                        if (product.Keyword === result.Keyword && product.State === result.State)
                        {
                            vm.shoppingList[index] = result;
                        }

                        index++;
                    }

                    $localStorage.cart = angular.toJson(vm.shoppingList); // Save cart to local storage.

                    BuildLegend();
                }
            );
        }
    };

    /*
     * Removed a item from the shopping cart.
     */
    vm.RemoveFromList = function (cartItem)
    {
        var itemIndex = vm.shoppingList.indexOf(cartItem);
        vm.shoppingList.splice(itemIndex, 1);

        $localStorage.cart = angular.toJson(vm.shoppingList); // Save cart to local storage.

        BuildLegend();
    };

    /*
     *  Navigates to details related to the selected product
     */
    vm.ViewProductDetails = function (product)
    {
        if (!product.IsClean)
        {
            $sessionStorage.SearchSummary = product;
            $location.path("/index/product");
        }
    };

    /*
     * Loads the feed data.
     */
    vm.StartRSS = function ()
    {
        StartupRSS();
    };

    function StartupRSS()
    {

        feedLoader.GetRSSFeed("http://www.fda.gov/AboutFDA/ContactFDA/StayInformed/RSSFeeds/Recalls/rss.xml").then(function (res)
        {
            var data = angular.toJson(res.data.responseData.feed.entries);

            vm.feeds = angular.fromJson(data);
            vm.IsRSSLoading = false;
        });

    }

    vm.ClearList = function ()
    {

        vm.shoppingList = [];
        $localStorage.cart = angular.toJson(vm.shoppingList);
        BuildLegend();

    }

    function BuildLegend()
    {

        vm.Class1Visible = false;
        vm.Class2Visible = false;
        vm.Class3Visible = false;
        vm.EventsVisible = false;
        vm.HasItems = false;
        vm.CleanVisible = false;

        vm.shoppingList.forEach(function (product)
        {

            vm.HasItems = true;

            if (product.HasClassI)
            {
                vm.Class1Visible = true;
            }
            if (product.HasClassII)
            {
                vm.Class2Visible = true;
            }
            if (product.HasClassIII)
            {
                vm.Class3Visible = true;
            }
            if (product.HasEvents)
            {
                vm.EventsVisible = true;
            }
            if (product.IsClean)
            {
                vm.CleanVisible = true;
            }

        });
    }

    vm.ThumbsUp = function ()
    {

        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-left",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        toastr["success"]("Your feedback is valuable to us.  Thank you for submitting your opinion!", "Thank You!");

    }

    vm.ThumbsDown = function ()
    {

        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-left",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        toastr["success"]("Your feedback is valuable to us.  Thank you for submitting your opinion!", "Thanks!");

    }

}