using System;
using System.Threading;
using Microsoft.AspNetCore.SignalR;

using SignalR_02_auth.Controllers;

namespace SignalR_02_auth
{
    public partial class Program
    {
        // regularly send different messages to different users

        public static void Ticker (IHubContext<MessageHub> BroadcastHub, CancellationToken Cancel)
        {
            while (!Cancel.IsCancellationRequested)
            {
                Cancel.WaitHandle.WaitOne (1000);

                foreach (var Package in new[]
                {
                    // different prefixes for different users
                    new {UserId = "1", Prefix = "square"},
                    new {UserId = "2", Prefix = "circle"}
                })
                {
                    BroadcastHub.Clients.Users (Package.UserId).SendAsync ("update_01",
                            Package.Prefix + ": " + DateTime.Now.ToString ("s"), cancellationToken: Cancel)
                        .GetAwaiter ()
                        .GetResult ()
                        ;
                }
            }
        }
    }
}
