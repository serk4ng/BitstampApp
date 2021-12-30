using System;

namespace BitstampApp.Websocket
{
    /// <summary>
    /// Bitstamp Pro static urls
    /// </summary>
    public static class BitstampValues
    {
        /// <summary>
        /// Main Bitstamp url to websocket API
        /// </summary>
        public static readonly Uri ApiWebsocketUrl = new Uri("wss://ws.bitstamp.net");
    }
}