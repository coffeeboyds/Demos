using System;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using PongSignalR.Models;

namespace PongSignalR
{
    // This class signals the clients
    public class Broadcaster
    {
        private readonly static Lazy<Broadcaster> _instance = new Lazy<Broadcaster>(() => new Broadcaster());

        private readonly IHubContext _hubContext;

        public Broadcaster()
        {
            // Save our hub context so we can easily use it 
            // to send to its connected clients
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<PongHub>();
        }

        public void Broadcast(IEnumerable<GameObject> gameObjectsMoved)
        {
            // Tell the clients the new position of moved objects
            foreach(var gameObject in gameObjectsMoved)
            {
                // This is how we can access the Clients property 
                // in a static hub method or outside of the hub entirely. Use AllExcept for a performance tweak
                _hubContext.Clients.AllExcept(gameObject.LastUpdatedBy).updateGameObjectPosition(gameObject);

                gameObject.Moved = false;
            }
        }

        public void Broadcast(IEnumerable<GameObject> gameObjects, string connectionId)
        {
            // Signal only the client with connectionId

            foreach (var gameObject in gameObjects)
                _hubContext.Clients.Client(connectionId).updateGameObjectPosition(gameObject);
        }
    }
}