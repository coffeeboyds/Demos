using System.Diagnostics;
using System.Web.Http.ExceptionHandling;

namespace RestMessengerService.Services
{
    public class TraceExceptionLogger : ExceptionLogger
    {
        // This gets called for every unhandeled exception caught by Web API 2.
        // It's good to log exceptions that you have no control of.
        // Don't forget to register it in WebApiConfig
        public override void Log(ExceptionLoggerContext context)
        {
            var exceptionMessage = context.ExceptionContext.Exception.Message;
            Trace.TraceError(exceptionMessage);
        }
    }
}