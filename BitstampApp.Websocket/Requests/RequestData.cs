using Newtonsoft.Json;

namespace BitstampApp.Websocket.Requests
{
    public class RequestData
    {
        [JsonProperty("channel")] public string Channel { get; set; }
    }
}