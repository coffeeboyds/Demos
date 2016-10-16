using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace RestMessengerService.Services
{
    public class UnhandledExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            string errorMessage = "Unhandled exception was thrown. " +
                                  "Exception message: " + context.Exception.Message +
                                  " - CatchBlock: " + context.CatchBlock.Name; // CatchBlock is a string indicating which catch block saw the exception

            // Note: The reason phrase must not contain new-line characters!.

            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError);

            response.ReasonPhrase = errorMessage;
            context.Result = new ResponseMessageResult(response);
        }

        // Determines if the above Handle(ExceptionHandlerContext context) method gets called.
        // Typically, you would only want to call it once, at the last exception thrown.
        // You can write your own condition to ignore certain exceptions here too.
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return context.CatchBlock.IsTopLevel;
        }
    }
}