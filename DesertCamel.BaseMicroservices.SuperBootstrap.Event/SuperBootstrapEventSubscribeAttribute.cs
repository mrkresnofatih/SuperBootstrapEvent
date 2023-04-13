using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesertCamel.BaseMicroservices.SuperBootstrap.Event
{
    public class SuperBootstrapEventSubscribeAttribute : CapSubscribeAttribute
    {
        public SuperBootstrapEventSubscribeAttribute(string name, bool isPartial = false) : base(name, isPartial)
        {
        }
    }
}
