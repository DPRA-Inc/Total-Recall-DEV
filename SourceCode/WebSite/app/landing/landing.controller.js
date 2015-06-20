angular.module('TotalRecall').controller('landingcontroller', landingcontroller)

function landingcontroller($http) {
    var vm = this;

    vm.shoppingList = {};
           
    vm.AddToList = function (itemToAdd) {

        var item;

        item.
        vm.shoppingList.push(itemToAdd);

        //var serviceUrl = 'QuickHandler.ashx?Command=person';

        //$http({
        //    method: 'GET',
        //    url: serviceUrl,
        //    headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
        //}).
        //    success(function (data, status, headers, config) {
        //        if (data == undefined || data == "null") return;


        //        callback(data);
        //    }).
        //    error(function (data, status, headers, config) {
        //        $log.warn(data, status, headers, config)
        //    });

	    //var data = landingservice.GetPeopleListing(function (value) {

	    //    this.PeopleListing = value;
            
	    //});
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