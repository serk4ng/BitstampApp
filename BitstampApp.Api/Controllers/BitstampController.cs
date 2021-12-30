using BitstampApp.Api.Helpers;
using BitstampApp.Websocket;
using BitstampApp.Websocket.Channels;
using BitstampApp.Websocket.Client;
using BitstampApp.Websocket.Communicator;
using BitstampApp.Websocket.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;

namespace BitstampApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class BitstampController : ControllerBase
    {
        public static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);

        private static readonly string API_KEY = "your api key";
        private static readonly string API_SECRET = "";
        public double hacim = 0;

        Stopwatch stopWatch = new Stopwatch();
        public BitstampController()
        {      
            stopWatch.Start();
        }
        [HttpPost]
        [Authorize]
        [Route("hacim")]
        public async Task<IActionResult> Hacim(BitstampData bsd)
        {


            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;


            var url = BitstampValues.ApiWebsocketUrl;

            using (var communicator = new BitstampWebsocketCommunicator(url))
            {
                communicator.Name = "Bitstamp-1";

                using (var client = new BitstampWebsocketClient(communicator))
                {
                    SubscribeToStreams(client);

                    communicator.ReconnectionHappened.Subscribe(async type =>
                    {  
                        await SendSubscriptionRequests(client,bsd.symbol);
                    });

                    await communicator.Start();
                  
                    ExitEvent.WaitOne();
                }
                await communicator.Stop(WebSocketCloseStatus.NormalClosure, "");

            }


            return Ok(hacim.ToString());
        }

        private static async Task SendSubscriptionRequests(BitstampWebsocketClient client,string symbol)
        {

            client.Send(new SubscribeRequest(symbol, Channel.Orders));
   
        }

        public double SubscribeToStreams(BitstampWebsocketClient client)
        {
            int adet = 0;
            double miktar = 0;

            ExitEvent.Reset();
            client.Streams.OrdersStream.Subscribe(x =>
            {
                adet++;
                miktar += x.Data.Price;

                TimeSpan ts = stopWatch.Elapsed;
                hacim = adet * miktar;
                if (ts.Minutes == 1)
                {                 
                    ExitEvent.Set();            
                }
 
            });

            return hacim;
        }

            private static void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            ExitEvent.Set();
        }

        private static void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            ExitEvent.Set();
        }
    }

}
