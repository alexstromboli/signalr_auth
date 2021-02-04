using System;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;

using SignalR_02_auth.Controllers;

namespace SignalR_02_auth
{
    public partial class Program
    {
        public static void Main (string[] args)
        {
            var Host = WebHost.CreateDefaultBuilder (new string[0])
                    .UseStartup<Startup> ()
                    .UseUrls ($"http://*:3795")
                    .Build ()
                ;

            // extract hub
            var BroadcastHub = (IHubContext<MessageHub>)Host.Services.GetService (typeof (IHubContext<MessageHub>));

            CancellationTokenSource CancelSrc = new CancellationTokenSource ();
            var HostTask = Host.RunAsync (CancelSrc.Token);

            // run a separate thread to broadcast messages
            Thread thTicker = new Thread (() => Ticker (BroadcastHub, CancelSrc.Token));
            thTicker.Start ();

            Console.ReadLine ();

            CancelSrc.Cancel ();
            thTicker.Join ();
            HostTask.Wait ();
        }
    }
}
