using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;

namespace PongSignalR
{
    public class LoggingPipelineModule : HubPipelineModule
    {
        // We trace any unhandled error here. Don't forget to register this class to the pipeline.
        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            Trace.TraceError("=> Exception " + exceptionContext.Error.Message);
            if (exceptionContext.Error.InnerException != null)
            {
                Trace.TraceError("=> Inner Exception " + exceptionContext.Error.InnerException.Message);
            }

            base.OnIncomingError(exceptionContext, invokerContext);
        }
    }
}