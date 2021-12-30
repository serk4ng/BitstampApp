using System;

namespace BitstampApp.Websocket.Exceptions
{
    public class BitstampBadInputException : BitstampException
    {
        public BitstampBadInputException()
        {
        }

        public BitstampBadInputException(string message) : base(message)
        {
        }

        public BitstampBadInputException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}