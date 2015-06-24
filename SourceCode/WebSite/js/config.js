function config($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/index/landing");

    // Tags: ROUTING, ADD PAGE
    $stateProvider
        .state("index", {
            abstract: true,
            url: "/index",
            templateUrl: "views/common/content.html"
        })
        .state("index.landing", {
            url: "/landing",
            templateUrl: "app/Landing/landing.html",
            data: { pageTitle: "Home" }
        })
        .state("index.product", {
            url: "/product",
            templateUrl: "app/product/product.html",
            data: { pageTitle: "Product" }
        })
        .state("index.about", {
            url: "/about",
            templateUrl: "app/about/about.html",
            data: { pageTitle: "About Us" }
        });
}

angular
    .module("TotalRecall")
    .config(config)
    .run(function($rootScope, $state) {
        $rootScope.$state = $state;
    });