$(function ()
{
    var pongHub = $.connection.pongHub,

    ClientTypeEnum = Object.freeze({
                        Spectator : 0,
                        Player1 : 1,
                        Player2 : 2
                     }),

    clientType,

    $player1 = $("#player1"),
    $player2 = $("#player2"),
    $ball = $("#ball"),

    // Send a maximum of 10 messages per second
    // (mouse movements trigger a lot of messages)
    messageFrequency = 10,

    // Determine how often to send messages in
    // time to abide by the messageFrequency
    updateRate = 1000 / messageFrequency,

    gameObjects =
    [
        player1GameObject = 
        {
            left: 100,
            top: 100,
            width: document.getElementById("player1").offsetWidth,
            height: document.getElementById("player1").offsetHeight,
            id: "player1",
            moved: false,
            element: $player1
        },

        player2GameObject =
        {
            left: 880,
            top: 100,
            width: document.getElementById("player2").offsetWidth,
            height: document.getElementById("player2").offsetHeight,
            id: "player2",
            moved: false,
            element: $player2
        },

        ballGameObject =
        {
            left: 500,
            top: 250,
            width: document.getElementById("ball").offsetWidth,
            height: document.getElementById("ball").offsetHeight,
            id: "ball",
            moved: false,
            element: $ball
        }
    ];

    // The PongHub on the server will be able to call this function
    pongHub.client.updateGameObjectPosition = function (gameObject)
    {
        for (var index in gameObjects)
        {
            if (gameObjects[index].id == gameObject.id)
            {
                gameObjects[index].left = gameObject.left;
                gameObjects[index].top = gameObject.top;

                // Gradually move the shape towards the new location (interpolate)
                // We also clear the animation queue so that we start a new 
                // animation and don't lag behind.
                gameObjects[index].element.animate({ left: gameObject.left, top: gameObject.top }, { duration: 100, queue: false });
            }
        }
    };

    // Enables logging (in the console, and shows what transport is being used)
    $.connection.hub.logging = true;

    // This will log when an error occurs on a method invocation to the server, but with no further details.
    // To have for details in the log, configure the hub configuration to send back more details on the error (not recommended for release)
    $.connection.hub.error(function (error)
    {
        console.log('SignalR error: ' + error)
    });

    // Start the connection
    $.connection.hub.start().done(function ()
    {
        // We call AddGameObject(string id) method on the server (in PongHub class)
        pongHub.server.addGameObject("player1");
        pongHub.server.addGameObject("player2");
        pongHub.server.addGameObject("ball");

        // Find out which player you are by calling GetClientType() on the server
        pongHub.server
                   .getClientType()
                   .done(function (result)
                   {
                       clientType = result;

                       if (clientType == ClientTypeEnum.Player1)
                       {
                           pongHub.state.playerName = "player1";

                           makeDraggable(player1GameObject);
                           document.getElementById("message").innerHTML = "You are PLAYER 1";
                       }
                       else if (clientType == ClientTypeEnum.Player2)
                       {
                           pongHub.state.playerName = "player2";

                           makeDraggable(player2GameObject);
                           document.getElementById("message").innerHTML = "You are PLAYER 2";
                       }
                       else 
                       {
                           document.getElementById("message").innerHTML = "You are a spectator";
                       }
                   });

        // Start the client side server update interval
        setInterval(updateServer, updateRate);
    });

    function updateServer()
    {
        // Send any data needed to the server...

        if (clientType == ClientTypeEnum.Spectator) // If you're a spectator, you don't update the server
            return;

        updateGameObjectPositions();

        playerGameObject = clientType == ClientTypeEnum.Player1
                                        ? player1GameObject
                                        : player2GameObject;

        checkForCollisionsWithPlayerAndBall(playerGameObject);
    }

    function checkForCollisionsWithPlayerAndBall(playerGameObject)
    {
        if (isCollide(playerGameObject, ballGameObject))
        {
            if (clientType == ClientTypeEnum.Player1)
                pongHub.server.onPlayer1Hit();
            else
                pongHub.server.onPlayer2Hit();
        }
    }

    function updateGameObjectPositions()
    {
        // Update the game object positions
        for (var index in gameObjects)
        {
            // Only update server if we have a new movement
            if (gameObjects[index].moved)
            {
                //Call UpdateGameObjectPosition(GameObject clientGameObject) on our server. GameObject will be serialized as JSON
                pongHub.server.updateGameObjectPosition(gameObjects[index]);
                gameObjects[index].moved = false;
            }
        }
    }

    function makeDraggable(gameObject)
    {
        gameObject.element.draggable(
        {
            axis: "y",
            containment: 'parent',
            drag: function ()
            {
                gameObject.left = gameObject.element.offset().left;
                gameObject.top = gameObject.element.offset().top;

                gameObject.moved = true;
            }
        });
    }

    function isCollide(gameObjectA, gameObjectB)
    {
        var aLeft = gameObjectA.left,
            aTop = gameObjectA.top,
            aWidth = gameObjectA.width,
            aHeight = gameObjectA.height,
            bLeft = gameObjectB.left,
            bTop = gameObjectB.top,
            bWidth = gameObjectB.width,
            bHeight = gameObjectB.height;

        return !(
                ((aTop + aHeight) < (bTop)) ||
                (aTop > (bTop + bHeight)) ||
                ((aLeft + aWidth) < bLeft) ||
                (aLeft > (bLeft + bWidth))
        );
    }
});