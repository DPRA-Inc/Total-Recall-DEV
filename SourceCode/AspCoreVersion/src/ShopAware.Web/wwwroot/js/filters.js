/*
 * Allows for highlighting a keyword(s) in a phrase.
 */
angular.module("TotalRecall").filter("highlight", function ($sce) {
    return function (text, phrase) {
        if (angular.isString(text) && angular.isString(phrase)) {
            var keywords = phrase.split(" ");

            for (var index = 0; index < keywords.length; index++) {
                text = text.replace(new RegExp("(" + keywords[index] + ")", "gi"), "<span class='highlight'>$1</span>");
            }
        }
        return $sce.trustAsHtml(text);
    };
})