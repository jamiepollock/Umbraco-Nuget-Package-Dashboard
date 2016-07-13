angular.module("umbraco").controller("Our.Umbraco.NuGetPackageDashboard.DashboardController", function ($scope, $http) {
    $scope.filter = "";
    $scope.totalItems = [];
    $scope.items = [];

    var doSearch = function (items, filter) {
        var cleanFilter = filter.trim().toLowerCase();

        if (cleanFilter.length === 0) {
            $scope.items = items;
        }

        $scope.items = _.filter(items,
            function (item) {
                return item.Id.toLowerCase().indexOf(cleanFilter) > -1 ||
                    item.Version.toLowerCase().indexOf(cleanFilter) > -1 ||
                    item.TargetFramework.toLowerCase() === cleanFilter;
            });
    };

    $scope.refreshData = function () {
        $http.get("/umbraco/backoffice/api/NuGetPackageDashboardApi/Get")
             .then(function (response) {
                 $scope.totalItems = response.data;
                 doSearch($scope.totalItems, $scope.filter);
             }, function (response) {
                 $scope.dashboardException = {
                     Title: response.data.Message,
                     Message: response.data.ExceptionMessage
                 };
             }
        );
    };
    var makeSearch = function () {
        doSearch($scope.totalItems, $scope.filter);
    };
    var searchListView = _.debounce(function () {
        $scope.$apply(function () {
            makeSearch();
        });
    }, 500);

    $scope.forceSearch = function (ev) {
        //13: enter
        switch (ev.keyCode) {
            case 13:
                makeSearch();
                break;
        }
    };
    $scope.enterSearch = function () {
        searchListView();
    };

    $scope.refreshData();
});