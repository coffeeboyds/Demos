using Microsoft.AspNet.SignalR;
using PongSignalR.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PongSignalR
{
    // This class is the medium that the client JavaScript uses to signal the server
    public class PongHub : Hub
    {
        // Is set via the constructor on each creation
        private ServerGameController _gameController;

        // Created for every connected client (which is why ServerGameController needs to be a singleton)
        public PongHub()
            : this(ServerGameController.Instance)
        {
        }

        public PongHub(ServerGameController gameController)
        {
            _gameController = gameController;
        }

        public void AddGameObject(string id)
        {
            _gameController.AddGameObject(id);
        }

        public void UpdateGameObjectPosition(GameObject clientGameObject)
        {
            // The playerName state object was set in the Javascript so we can reference it here.
            string playerName = Clients.Caller.playerName;
            Trace.TraceInformation(playerName + " moved to position " + clientGameObject.Top);

            clientGameObject.LastUpdatedBy = Context.ConnectionId;

            _gameController.UpdateGameObjectPosition(clientGameObject);
        }

        public Enums.ClientType GetClientType()
        {
            if (_gameController.Player1ConnectionId == Context.ConnectionId)
                return Enums.ClientType.Player1;

            if (_gameController.Player2ConnectionId == Context.ConnectionId)
                return Enums.ClientType.Player2;

            return Enums.ClientType.Spectator;
        }

        public void OnPlayer1Hit()
        {
            _gameController.OnPlayer1Hit();
        }

        public void OnPlayer2Hit()
        {
            _gameController.OnPlayer2Hit();
        }

        public override Task OnConnected()
        {
            // Keep track of who is player1/player2
            if(_gameController.Player1ConnectionId == null)
            {
                _gameController.Player1ConnectionId = Context.ConnectionId;
            }
            else if (_gameController.Player2ConnectionId == null)
            {
                _gameController.Player2ConnectionId = Context.ConnectionId;
            }

            _gameController.UpdateGameObjectPositionsForClient(Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // stopCalled:
            //     true, if stop was called on the client closing the connection gracefully; false,
            //     if the connection has been lost for longer than the Microsoft.AspNet.SignalR.Configuration.IConfigurationManager.DisconnectTimeout.
            //     Timeouts can be caused by clients reconnecting to another SignalR server in scaleout.

            if (_gameController.Player1ConnectionId == Context.ConnectionId)
            {
                _gameController.Player1ConnectionId = null;
            }
            else if (_gameController.Player2ConnectionId == Context.ConnectionId)
            {
                _gameController.Player2ConnectionId = null;
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}