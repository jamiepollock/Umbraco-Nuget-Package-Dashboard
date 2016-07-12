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

    $scope.enterSearch = function ($event) {
        $($event.target).next().focus();
    };

    $scope.search = function () {
        doSearch($scope.totalItems, $scope.filter);
    };

    $scope.refreshData();
});