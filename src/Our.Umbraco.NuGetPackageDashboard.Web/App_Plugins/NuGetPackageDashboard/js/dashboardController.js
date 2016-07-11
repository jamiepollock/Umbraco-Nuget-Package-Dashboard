angular.module("umbraco").controller("Our.Umbraco.NuGetPackageDashboard.DashboardController", function ($scope, $http) {
    $scope.refreshData = function () {
        $http.get("/umbraco/backoffice/api/NuGetPackageDashboardApi/Get")
            .then(function (response) {
                $scope.items = response.data;
            });
    };
    $scope.refreshData();
});