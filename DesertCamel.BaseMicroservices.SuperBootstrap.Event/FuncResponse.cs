using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesertCamel.BaseMicroservices.SuperBootstrap.Event
{
    public class FuncResponse<T>
    {
        public T Data { get; set; }

        public string ErrorMessage { get; set; }
    }
}
