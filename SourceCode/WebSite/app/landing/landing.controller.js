angular.module('TotalRecall').controller('landingcontroller', landingcontroller)

function landingcontroller($location, landingservice) {
    var vm = this;

    vm.textValue = null;
    vm.shoppingList = [];
           
    vm.AddToList = function() {
        var value = vm.textValue;

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

        vm.shoppingList.push(item);

        // Differnt Ranks.
        //Rank: 'warning'
        //Rank: 'success',
        //Rank: 'info',
        //Rank: 'danger',

        var data = landingservice.GetIssues(value,
            function (result) {
                
                // Search for the Keyword in our list.
                vm.shoppingList.forEach(function (product) {

                    product.Rank = "";

                    if (product.Keyword == result.Keyword) {

                        if (result.EventCount > 0) {
                            product.HasEvents = true;
                            product.EventCount = result.EventCount;
                            product.Rank = "success";
                        }
                            
                        if (result.ClassIIICount > 0) {
                            product.HasClassIII = true;
                            product.ClassIIICount = result.ClassIIICount;
                            product.Rank = "info";
                        }

                        if (result.ClassIICount > 0) {
                            product.HasClassII = true;
                            product.ClassIICount = result.ClassIICount;
                            product.Rank = "warning";
                        }

                        if (result.ClassICount > 0) {
                            product.HasClassI = true;
                            product.ClassICount = result.ClassICount;
                            product.Rank = "danger";
                        }

                        product.IsLoading = false;
                    }                                     
                });
            }
        );
    }

    vm.ViewProductDetails = function (product) {

        GlobalsModule.SelectedProduct = product;
        $location.path('/index/product');

    }

};



