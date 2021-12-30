namespace BitstampApp.Websocket.Messages
{
    public class MessageBase
    {
        public virtual MessageType Event { get; set; }
    }
}