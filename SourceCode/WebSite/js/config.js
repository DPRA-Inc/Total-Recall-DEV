/**
 */
function config($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {
    $urlRouterProvider.otherwise("/index/landing");

    $ocLazyLoadProvider.config({
        // Set to true if you want to see what and when is dynamically loaded
        debug: false
    });

    // Tags: ROUTING, ADD PAGE
    $stateProvider

        .state('index', {
            abstract: true,
            url: "/index",
            templateUrl: "views/common/content.html",
        })
        .state('index.landing', {
            url: "/landing",
            templateUrl: "app/Landing/landing.html",
            data: { pageTitle: 'Landing Page' }
        })
        .state('index.product', {
            url: "/product",
            templateUrl: "app/product/product.html",
            data: { pageTitle: 'product view' }
        })
}
angular
    .module('TotalRecall')
    .config(config)
    .run(function($rootScope, $state) {
        $rootScope.$state = $state;
    });
