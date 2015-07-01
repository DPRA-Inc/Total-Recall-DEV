/**
 * pageTitle - Directive for set Page title - mata title
 */
function pageTitle($rootScope, $timeout)
{
    return {
        link: function (scope, element)
        {
            var listener = function (event, toState, toParams, fromState, fromParams)
            {
                var title = 'ShopAware.gov';
                if (toState.data && toState.data.pageTitle) title = 'ShopAware.gov | ' + toState.data.pageTitle;
                $timeout(function ()
                {
                    element.text(title);
                });
            };
            $rootScope.$on('$stateChangeStart', listener);
        }
    }
};

/**
 * sideNavigation - Directive for run metsiMenu on sidebar navigation
 */
function sideNavigation($timeout)
{
    return {
        restrict: 'A',
        link: function (scope, element)
        {
            // Call the metsiMenu plugin and plug it to sidebar navigation
            $timeout(function ()
            {
                element.metisMenu();
            });
        }
    };
};

/**
 * iboxTools - Directive for iBox tools elements in right corner of ibox
 */
function iboxTools($timeout)
{
    return {
        restrict: 'A',
        scope: true,
        templateUrl: 'views/common/ibox_tools.html',
        controller: function ($scope, $element)
        {
            // Function for collapse ibox
            $scope.showhide = function ()
            {
                var ibox = $element.closest('div.ibox');
                var icon = $element.find('i:first');
                var content = ibox.find('div.ibox-content');
                content.slideToggle(200);
                // Toggle icon from up to down
                icon.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
                ibox.toggleClass('').toggleClass('border-bottom');
                $timeout(function ()
                {
                    ibox.resize();
                    ibox.find('[id^=map-]').resize();
                }, 50);
            },
            // Function for close ibox
                $scope.closebox = function ()
                {
                    var ibox = $element.closest('div.ibox');
                    ibox.remove();
                }
        }
    };
};

/**
 * minimalizaSidebar - Directive for minimalize sidebar
*/
function minimalizaSidebar($timeout)
{
    return {
        restrict: 'A',
        template: '<a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="" ng-click="minimalize()"><i class="fa fa-bars"></i></a>',
        controller: function ($scope, $element)
        {
            $scope.minimalize = function ()
            {
                $("body").toggleClass("mini-navbar");
                if (!$('body').hasClass('mini-navbar') || $('body').hasClass('body-small'))
                {
                    // Hide menu in order to smoothly turn on when maximize menu
                    $('#side-menu').hide();
                    // For smoothly turn on menu
                    setTimeout(
                        function ()
                        {
                            $('#side-menu').fadeIn(500);
                        }, 100);
                } else if ($('body').hasClass('fixed-sidebar'))
                {
                    $('#side-menu').hide();
                    setTimeout(
                        function ()
                        {
                            $('#side-menu').fadeIn(500);
                        }, 300);
                } else
                {
                    // Remove all inline style from jquery fadeIn function to reset menu state
                    $('#side-menu').removeAttr('style');
                }
            }
        }
    };
};

/**
 * ngEnter - Added to a input text box to execute a function on pressing enter.
 */
function ngEnter()
{
    return {
        link: function (scope, elements, attrs)
        {
            elements.bind('keydown keypress', function (event)
            {
                if (event.which === 13)
                {
                    scope.$apply(function ()
                    {
                        scope.$eval(attrs.ngEnter);
                    });
                    event.preventDefault();
                }
            });
        }
    };
};

/**
 * the HTML5 autofocus property can be finicky when it comes to dynamically loaded
 * templates and such with AngularJS. Use this simple directive to
 * tame this beast once and for all.
 *
 * Usage:
 * <input type="text" autofocus>
 */
function autoFocus($timeout)
{

    return {
        restrict: 'A',
        link: function ($scope, $element)
        {
            $timeout(function ()
            {
                $element[0].focus();
            });
        }
    }
}

function angulike($window, $rootScope) {
    return {
        restrict: 'A',
        scope: {
            fbLike: '=?'
        },
        link: function (scope, element, attrs) {
            if (!$window.FB) {
                // Load Facebook SDK if not already loaded
                $.getScript('//connect.facebook.net/en_US/sdk.js', function () {
                    $window.FB.init({
                        appId: $rootScope.facebookAppId,
                        xfbml: true,
                        version: 'v2.0'
                    });
                    renderLikeButton();
                });
            } else {
                renderLikeButton();
            }
 
            var watchAdded = false;
            function renderLikeButton() {
                if (!!attrs.fbLike && !scope.fbLike && !watchAdded) {
                    // wait for data if it hasn't loaded yet
                    watchAdded = true;
                    var unbindWatch = scope.$watch('fbLike', function (newValue, oldValue) {
                        if (newValue) {
                            renderLikeButton();
                                       
                            // only need to run once
                            unbindWatch();
                        }
                                   
                    });
                    return;
                } else {
                    element.html('<div class="fb-like"' + (!!scope.fbLike ? ' data-href="' + scope.fbLike + '"' : '') + ' data-layout="button_count" data-action="like" data-show-faces="true" data-share="true"></div>');
                    $window.FB.XFBML.parse(element.parent()[0]);
                }
            }
        }
    };
}




/**
 *
 * Pass all functions into module
 */

angular
    .module('TotalRecall')
    .directive('pageTitle', pageTitle)
    .directive('sideNavigation', sideNavigation)
    .directive('iboxTools', iboxTools)
    .directive('minimalizaSidebar', minimalizaSidebar)
    .directive('autoFocus', autoFocus)
    .directive('ngEnter', ngEnter);
