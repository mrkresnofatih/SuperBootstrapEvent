using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesertCamel.BaseMicroservices.SuperBootstrap.Event
{
    public class EventMessage
    {
        public string CorrelationId { get; set; }

        public string JsonMessage { get; set; }
    }
}
