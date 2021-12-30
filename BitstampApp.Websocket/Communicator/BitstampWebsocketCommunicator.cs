using System;
using System.Net.WebSockets;
using Websocket.Client;

namespace BitstampApp.Websocket.Communicator
{
    public class BitstampWebsocketCommunicator : WebsocketClient, IBitstampCommunicator
    {
        public BitstampWebsocketCommunicator(Uri url, Func<ClientWebSocket> clientFactory = null)
            : base(url, clientFactory)
        {
        }
    }
}