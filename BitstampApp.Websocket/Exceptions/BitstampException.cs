using System;

namespace BitstampApp.Websocket.Exceptions
{
    public class BitstampException : Exception
    {
        public BitstampException()
        {
        }

        public BitstampException(string message)
            : base(message)
        {
        }

        public BitstampException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}