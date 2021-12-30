using System.Runtime.Serialization;

namespace BitstampApp.Websocket.Messages
{
    public enum MessageType
    {
        [DataMember(Name = "bts:subscribe")] Subscribe,
        [DataMember(Name = "bts:unsubscribe")] Unsubscribe,

        Error,
        Info,
        Trade,
        OrderBook,
        Wallet,
        Order,
        Position,
        Quote,
        Instrument,
        Margin,
        Execution,
        Funding,
        OrderBookDiff,
        OrderBookDetail,
        Snapshot
    }
}