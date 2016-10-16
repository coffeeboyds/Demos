using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(PongSignalR.Startup))]

namespace PongSignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here

            GlobalHost.HubPipeline.AddModule(new LoggingPipelineModule());
            app.MapSignalR();
        }
    }
}
