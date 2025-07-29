// Startup.cs
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(TodoServer.Startup))]

namespace TodoServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(); // Rutas de SignalR
        }
    }
}
