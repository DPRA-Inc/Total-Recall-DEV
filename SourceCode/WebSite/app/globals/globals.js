angular.module('TotalRecall').controller('GlobalsModule', GlobalsModule)

function GlobalsModule() {

    this.ServiceUrl = 'http://LocalHost:33335/';
    this.userName = 'User Name goes Here';
    this.ApplicationDescription = 'It is an application skeleton for the GSA Project.';

    this.SelectedProduct = null;
    
};