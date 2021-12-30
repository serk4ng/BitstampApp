using Newtonsoft.Json;

namespace BitstampApp.Websocket.Requests
{
    public abstract class RequestBase
    {
        [JsonProperty("event")]
        public virtual string Event { get; set; }

        [JsonProperty("data")] public virtual RequestData RequestData { get; set; }
    }
}