var app = angular.module('giftexchangeApp', [
    'ngRoute',
    'ngResource'
]);

app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: 'main.html',
        controller: 'MainController'
    })
    .when('/exchange/:id', {
        templateUrl: 'exchange.html',
        controller: 'ExchangeController'
    });
});


/***
 * Data services to communicate with the backend
 ***/
app.factory('GiftExchange', function ($resource) {
    return $resource('/api/GiftExchange/:id/:action', { id: '@id', action: '@action' }, {
        update: {
            method: 'PUT'
        },
        shuffle: {
            method: 'GET',
            params: {
                action: 'shuffle'
            }
        }
    });
});

app.factory('Participant', function ($resource) {
    return $resource('/api/Participant/:id', { id: '@id' }, {
        update: {
            method: 'PUT'
        }
    });
});


/***
 * Controllers
 ***/
app.controller('MainController', function ($scope, $location, GiftExchange) {
    $scope.new_exchange = {
        name: '',
        description: '',
        maxPurchasePrice: 0
    };

    // Fetch all GiftExchange objects from the API
    GiftExchange.query(function (data) {
        $scope.giftexchanges = data;
    });

    // Allow the view to delete GiftExchange objects
    $scope.delete = function (exchange) {
        GiftExchange.delete({ id: exchange.id });

        // Apply the changes to the scope as well
        var index = $scope.giftexchanges.indexOf(exchange);
        $scope.giftexchanges.splice(index, 1);
    };

    $scope.createExchange = function (exchange) {
        var new_exchange = new GiftExchange();
        new_exchange.name = exchange.name;
        new_exchange.description = exchange.description;
        new_exchange.maxPurchasePrice = exchange.maxPurchasePrice;

        new_exchange.$save().then(function (data) {
            // There's a weird bug where modals won't hide their backgrounds if we redirect since they are created outside the
            // controller. So we can do this manually here as a workaround.
            $('.modal-backdrop').remove();

            $location.path('exchange/' + data.id);
        });
    };
});

app.controller('ExchangeController', function ($scope, $routeParams, GiftExchange, Participant) {
    // Fetch a single GiftExchange object from the API
    GiftExchange.get({ id: $routeParams['id'] }, function (data) {
        $scope.giftexchange = data;
    });

    // Allow the view to manipulate data via the API
    $scope.update = function (data) {
        GiftExchange.update({ id: data.id }, data);
        $scope.apply(function () {
            $scope.giftexchange = data;
        });
    };

    $scope.shuffle = function () {
        GiftExchange.shuffle({ id: $routeParams['id'] }, function (data) {
            $scope.apply(function () {
                $scope.giftexchange = data;
            });
        });
    };

    $scope.saveParticipant = function (participant) {
        if (participant.id) {
            // If this is an existing Participant then we just update it
            Participant.update({ id: participant.id }, participant);
        } else {
            // New Participants we handle differently
            var new_participant = new Participant();
            new_participant.giftExchangeId = $scope.giftexchange.id;
            new_participant.name = participant.name;
            new_participant.email = participant.email;
            new_participant.giftAssignmentId = null;

            // Save to the API and update the scope so we know that it's existing (for when the user clicks 'save' again)
            new_participant.$save().then(function (data) {
                participant.id = data.id;
            });
        }
    };

    $scope.addParticipant = function () {
        if ($scope.giftexchange.participants === null)
            $scope.giftexchange.participants = [];
        $scope.giftexchange.participants.push({});
    };

    $scope.deleteParticipant = function (participant) {
        // If the Participant has an ID then we need to round trip to the API to delete them
        if (participant.id != null) {
            Participant.delete({ id: participant.id });
        }

        var index = $scope.giftexchange.participants.indexOf(participant);
        $scope.giftexchange.participants.splice(index, 1);
    };

    $scope.getParticipantName = function (participant_id) {
        for (var i = 0; i < $scope.giftexchange.participants.length; i++)
            if ($scope.giftexchange.participants[i].id == participant_id)
                return $scope.giftexchange.participants[i].name;
        return null
    };
});