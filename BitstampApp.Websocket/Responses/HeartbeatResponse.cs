using System.Reactive.Subjects;
using BitstampApp.Websocket.Json;
using Newtonsoft.Json.Linq;

namespace BitstampApp.Websocket.Responses
{
    /// <summary>
    /// Heartbeat response
    /// </summary>
    public class HeartbeatResponse : ResponseBase
    {
        internal static bool TryHandle(JObject response, ISubject<HeartbeatResponse> subject)
        {
            //if (response?["channel"].Value<string>() != "heartbeat") return false;
            if (response != null && (bool) !response?["channel"].Value<string>().StartsWith("heartbeat"))
                return false;

            var parsed = response.ToObject<HeartbeatResponse>(BitstampJsonSerializer.Serializer);
            subject.OnNext(parsed);
            return true;
        }
    }
}