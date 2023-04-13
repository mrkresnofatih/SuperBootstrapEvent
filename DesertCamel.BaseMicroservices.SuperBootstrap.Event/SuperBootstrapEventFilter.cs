using DotNetCore.CAP.Filter;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesertCamel.BaseMicroservices.SuperBootstrap.Event
{
    public class SuperBootstrapEventFilter : SubscribeFilter
    {
        public override Task OnSubscribeExecutingAsync(ExecutingContext context)
        {
            var message = (EventMessage) context.DeliverMessage.Value;
            LogContext.PushProperty("CorrelationId", message.CorrelationId);
            return base.OnSubscribeExecutingAsync(context);
        }
    }
}
