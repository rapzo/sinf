'use strict';
/* App Controllers */

function ArtigosCtrl($scope, $http) {

    $scope.artigos = [];
    $scope.status = '';

    $http.get('http://localhost:49191/api/artigos/').success(function (data) {
        $scope.users = data;

    }).error(function (data) {
        $scope.status = 'ERROR on get!';
    });
}
MyCtrl1.$inject = [];


function MyCtrl2() {
}
MyCtrl2.$inject = [];
