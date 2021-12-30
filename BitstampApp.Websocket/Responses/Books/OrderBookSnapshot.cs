using System.Linq;
using System.Reactive.Subjects;
using BitstampApp.Websocket.Communicator;
using BitstampApp.Websocket.Json;
using BitstampApp.Websocket.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Websocket.Client;

namespace BitstampApp.Websocket.Responses.Books
{
    public class OrderBookSnapshot
    {
        public long? Timestamp { get; set; }
        public string Microtimestamp { get; set; }

        /// <summary>
        /// The symbol the update is for
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Order book bid levels
        /// </summary>
        //[JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Buy)]
        [JsonProperty("bids")]
        public SnapshotBookLevel[] Bids { get; set; }

        /// <summary>
        /// Order book ask levels
        /// </summary>
        //[JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Sell)]
        [JsonProperty("asks")]
        public SnapshotBookLevel[] Asks { get; set; }
    }
}