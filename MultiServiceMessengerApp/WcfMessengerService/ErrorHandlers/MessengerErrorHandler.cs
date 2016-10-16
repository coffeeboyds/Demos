using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WcfMessengerService.ErrorHandlers
{
    public class MessengerErrorHandler : IErrorHandler
    {
        // This gets called for every unhandeled exception in your operation contracts.
        // Don't forget to register it by adding it as an [ErrorBehavior] annotation on your service.
        // Return true if the error is considered as already handled, false to throw it to the client.
        public bool HandleError(Exception error)
        {
            Trace.TraceError(error.Message);
            return false;
        }

        // Provide a fault. The Message fault parameter can be replaced, or set to
        // null to suppress reporting a fault.
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var fe = error as FaultException ?? new FaultException(error.Message);

            var faultMsg = fe.CreateMessageFault();
            fault = Message.CreateMessage(version, faultMsg, fe.Action);
        }
    }
}
