angular.module("TotalRecall").controller("GlobalsModule", GlobalsModule);

function GlobalsModule($modal) {
    this.userName = "User Name goes Here";
    this.ApplicationDescription = "It is an application skeleton for the GSA Project.";
    this.RSSFeeds = "http://rss.cnn.com/rss/cnn_topstories.rss";

    this.OpenCommentsDialog = function() {
        $modal.open({
            templateUrl: "app/globals/sitecomments.html",
            controller: "sitecommentscontroller as vm"
        });
    };
};