using System.Reactive.Subjects;
using BitstampApp.Websocket.Json;
using BitstampApp.Websocket.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BitstampApp.Websocket.Responses
{

    public class ErrorResponse : ResponseBase
    {
        public override MessageType Event => MessageType.Error;

        [JsonProperty("code")] public object Code { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        internal static bool TryHandle(JObject response, ISubject<ErrorResponse> subject)
        {
            if (response?["event"].Value<string>() != "bts:error") return false;

            var parsed = response.ToObject<ErrorResponse>(BitstampJsonSerializer.Serializer);
            subject.OnNext(parsed);
            return true;
        }
    }
}