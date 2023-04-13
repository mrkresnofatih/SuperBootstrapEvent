using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesertCamel.BaseMicroservices.SuperBootstrap.Event
{
    public class SuperBootstrapEventOption
    {
        public class RabbitMQOption
        {
            public string HostName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int Port { get; set; }
            public string ExchangeName { get; set; }
        }

        public class ProviderOptions
        {
            public const string RABBITMQ = "RabbitMq";
            public const string AMAZONSQS = "AmazonSqs";
            public const string REDISSTREAMS = "RedisStreams";
        }
    }
}
