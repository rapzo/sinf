'use strict';


// Declare app level module which depends on filters, and services
angular.module('myApp', ['myApp.filters', 'myApp.services', 'myApp.directives', 'myApp.controllers', 'ui.bootstrap']).
  config(['$routeProvider', function ($routeProvider) {
      $routeProvider.when('/artigos', { templateUrl: '/partials/artigos.html', controller: ArtigosCtrl });
      $routeProvider.when('/view2', { template: '/partials/partial2.html', controller: MyCtrl2 });
      $routeProvider.otherwise({ redirectTo: '/view1' });
  }]);
