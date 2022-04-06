(function () {
    var app = angular.module('Assessment', ['ngRoute']);
    app.controller('AssessmentCtrl', function ($scope, $http, $window) {
        $scope.Message = null;
        $scope.AssessmentList = null;

        //$scope.ButtonClick = function () { //Chamada para ação de click do método ButtonClick()
            $http({
                method: "Post",
                //url: "/Assessments/GetByIdAsync/" + $scope.Id, //Retorna um item.
                url: "/Assessments/GetAllAsync", //Retorna uma lista.
                dataType: 'json',
                //data: { name: $scope.Id },
                headers: { "Content-Type": "application/json" }
            }).then(function successCallback(response) {
                $scope.AssessmentList = JSON.parse(response.data);
                
            }, function errorCallback(response) {
                $scope.Message = "Não foi encontrado resgistro.";
            });
        //};
        
    });
})();
