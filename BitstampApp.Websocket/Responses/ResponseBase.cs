using System;
using BitstampApp.Websocket.Messages;
using Newtonsoft.Json;

namespace BitstampApp.Websocket.Responses
{
    public abstract class ResponseBase : MessageBase
    {
        public string Channel { get; set; }
        public string Symbol { get; set; }
    }
}