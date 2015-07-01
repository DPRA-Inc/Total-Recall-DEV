
function config($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {
    $urlRouterProvider.otherwise("/index/landing");

    $ocLazyLoadProvider.config({
        // Set to true if you want to see what and when is dynamically loaded
        debug: false
    });

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
            data: { pageTitle: "Home" },
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'cgNotify',
                            files: ['css/angular-notify.min.css', 'js/angular/angular-notify.min.js']
                        }
                    ]);
                }
            }
        })
        .state("index.product", {
            url: "/product",
            templateUrl: "app/product/product.html",
            data: { pageTitle: "Product" },
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'angles',
                            files: ['plugins/chartJs/angles.js', 'plugins/chartJs/Chart.min.js']
                        },
                        {
                            name: 'angular-peity',
                            files: ['plugins/peity/jquery.peity.min.js', 'plugins/peity/angular-peity.js']
                        },
                        {
                            name: 'ui.checkbox',
                            files: ['js/bootstrap/angular-bootstrap-checkbox.js']
                        }
                    ]);
                }
            }
        })
        .state("index.about", {
            url: "/about",
            templateUrl: "app/about/about.html",
            data: { pageTitle: "About Us" }
        })
        .state("index.privacy", {
            url: "/privacy",
            templateUrl: "app/privacy/privacy.html",
            data: { pageTitle: "Privacy Policy" }
        });
}

angular
    .module("TotalRecall")
    .config(config)
    .run(function ($rootScope, $state)
    {
        $rootScope.$state = $state;
        $rootScope.facebookAppId = 'ShopAware.gov';
    });