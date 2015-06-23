angular.module('TotalRecall').controller('landingcontroller', landingcontroller)

function landingcontroller($location, landingservice) {
    var vm = this;

    vm.textValue = null;
    vm.shoppingList = [];
           
    LoadPageInfo();

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


    vm.AddToList = function() {
        var value = vm.textValue;
        var region = "TN";

        // Make the new item to be added to our list.
        var item = [];

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

        // Differnt Ranks.
        //Rank: 'warning'
        //Rank: 'success',
        //Rank: 'info',
        //Rank: 'danger',

        var searchStr = value + "|" + region;

        var data = landingservice.GetIssues(searchStr,
            function (result) {
                
                // Search for the Keyword in our list.
                vm.shoppingList.forEach(function (product) {

                    product.Rank = "";

                    if (product.Keyword == result.Keyword) {

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
                });
            }
        );
    }

    vm.ViewProductDetails = function (product) {
       
        if (!product.IsClean) {
            GlobalsModule.SelectedProduct = product;
            $location.path('/index/product');
        }
    }

};



