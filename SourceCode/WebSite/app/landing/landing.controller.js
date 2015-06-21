angular.module('TotalRecall').controller('landingcontroller', landingcontroller)

function landingcontroller($http, landingservice) {
    var vm = this;

    vm.textValue = null;
    vm.shoppingList = [];
           
    vm.AddToList = function() {
        var value = vm.textValue;

        // Make the new item to be added to our list.
        var item = [];

        item.KeyWord = value;
        item.Count = 0;
        item.Type = "";
        item.ServiceData = null;
        item.IsLoading = true;
        item.ShowCount = false;
        vm.shoppingList.push(item);

        var data = landingservice.GetIssues(value,
            function (results) {
                
                // Search for the Keyword in our list.
                vm.shoppingList.forEach(function (product) {

                    results.forEach(function (result) {

                        if (product.KeyWord === result.KeyWord) {
                            product.Count = result.Count;
                            product.ShowCount = true;

                            // Here we need to identify the Alert Types.


                            product.ServiceData = result;
                            product.IsLoading = false;
                        }
                    });
                });
            }
        );
    }

};



//function landingcontroller() {

//	this.userName = 'UserName goes here';
//	this.helloText = 'Landing Page';
//	this.descriptionText = 'It is an application skeleton for a typical AngularJS web app. You can use it to quickly bootstrap your angular webapp projects and dev environment for these projects.';

//	/**
// * alerts - used for dynamic alerts in Notifications and Tooltips view
// */
//	this.alerts = [
//        { type: 'danger', msg: 'Oh snap! Change a few things up and try submitting again.' },
//        { type: 'success', msg: 'Well done! You successfully read this important alert message.' },
//        { type: 'info', msg: 'OK, You are done a great job man.' }
//	];

//	this.GetPersonList = function () {
//		this.alerts.push({ msg: 'Another alert!' });
//	};



//};