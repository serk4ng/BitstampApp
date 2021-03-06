using BitstampApp.Websocket.Channels;
using BitstampApp.Websocket.Utils;

namespace BitstampApp.Websocket.Requests
{
    public class UnsubscribeRequest : RequestBase
    {
   
        public UnsubscribeRequest()
        {
        }

      
        public UnsubscribeRequest(string product, Channel channel)
        {
            Pair = product;
            Channel = channel;
        }

        private string Pair { get; }
        private Channel Channel { get; }

      
        public override string Event => "bts:unsubscribe";

    
        public override RequestData RequestData => new RequestData {Channel = AddSymbolToChannel()};

        private string AddSymbolToChannel()
        {
            var x = string.Join("_", GetChannelType(), CryptoPairsHelper.Clean(Pair));

            return x;
        }

        private string GetChannelType()
        {
            switch (Channel)
            {
                case Channel.Ticker:
                    return "live_trades";
                case Channel.Orders:
                    return "live_orders";
                case Channel.OrderBook:
                    return "order_book";
                case Channel.OrderBookDetail:
                    return "detail_order_book";
                case Channel.OrderBookDiff:
                    return "diff_order_book";
            }

            return null;
        }
    }
}