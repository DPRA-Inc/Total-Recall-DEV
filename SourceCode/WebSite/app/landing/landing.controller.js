angular.module('TotalRecall').controller('landingcontroller', landingcontroller)

function landingcontroller($location, landingservice) {
    var vm = this;

    vm.textValue = null;
    vm.shoppingList = [];
           
    vm.AddToList = function() {
        var value = vm.textValue;

        // Make the new item to be added to our list.
        var item = [];

        item.KeyWord = value; // Product name
        item.ServiceData = null; // Holds the original Data from the Service.
        item.Rank = 'success'; // How Bad is it, Color Code.
        item.RecallCount = 0; // Number of Recalls.
        item.EventCount = 0; // Amount of Events.
        item.HealthCount = 0; // Amount of Health Events.
        item.IsLoading = true; // Indicates we are waiting on Return From Service.
        item.HasInfo = false; // Indicates there is some info to show.
        item.HasRecalls = false;
        vm.shoppingList.push(item);

        // Differnt Ranks.
        //Rank: 'warning'
        //Rank: 'success',
        //Rank: 'info',
        //Rank: 'danger',

        var data = landingservice.GetIssues(value,
            function (results) {
                
                // Search for the Keyword in our list.
                vm.shoppingList.forEach(function (product) {

                    if (results.length == 0) {
                        product.IsLoading = false;
                        product.HasInfo = false;
                    }

                    results.forEach(function (result) {

                        if (product.KeyWord === result.KeyWord) {                            
                            product.RecallCount = "R-" + result.Count;                        

                            if (result.Count > 10) item.Rank = 'info';
                            if (result.Count > 15) item.Rank = 'warning';
                            if (result.Count >= 20) item.Rank = 'danger';
                            
                            if (result.Count > 0) product.HasRecalls = true;                               
                                
                            // Here we need to identify the Alert Types.                            
                            product.ServiceData = result;
                            product.IsLoading = false;

                            if (result.Count > 0) product.HasInfo = true;

                        }
                    });
                });
            }
        );
    }

    vm.ViewProductDetails = function (product) {

        GlobalsModule.SelectedProduct = product;

        $location.path('/index/product');


    }

};



