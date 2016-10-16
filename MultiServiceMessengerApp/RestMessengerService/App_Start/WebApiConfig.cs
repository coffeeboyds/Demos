using RestMessengerService.Filters;
using RestMessengerService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace RestMessengerService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalConfiguration.Configuration
                               .Services
                               .Add(typeof(IExceptionLogger), new TraceExceptionLogger());

            // You can only have 1 exception handler for the unhandeled exceptions.
            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler());

            config.Filters.Add(new ValidateModelAttribute());

            config.MapHttpAttributeRoutes();
        }
    }
}
