using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RestMessengerService.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                // Get my custom error messages that I wrote my the model class
                var errors = actionContext.ModelState
                                          .Values
                                          .SelectMany(m => m.Errors
                                                            .Select(e => e.ErrorMessage));

                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);

                actionContext.Response.ReasonPhrase = string.Join("\n", errors);
            }
        }
    }
}