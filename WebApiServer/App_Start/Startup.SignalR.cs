using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Owin;

namespace WebApiServer
{
	public partial class Startup
	{
        public void ConfigureSignalR(IAppBuilder app)
        {
            app.MapSignalR("/signalr", new HubConfiguration());
            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null;
        }
    }
}