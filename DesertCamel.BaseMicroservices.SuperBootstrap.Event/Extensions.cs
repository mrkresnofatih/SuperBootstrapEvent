using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesertCamel.BaseMicroservices.SuperBootstrap.Event.SuperBootstrapEventOption;

namespace DesertCamel.BaseMicroservices.SuperBootstrap.Event
{
    public static class Extensions
    {
        public static void AddBootstrapEvent(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCap(x =>
            {
                x.UseInMemoryStorage();

                var selectedProvider = configuration.GetSection("SuperBootstrap:Event:Selected").Value;
                switch(selectedProvider)
                {
                    case SuperBootstrapEventOption.ProviderOptions.RABBITMQ:
                        x.UseRabbitMQ(opt =>
                        {
                            var options = configuration.GetSection("SuperBootstrap:Event:Options:RabbitMq");
                            var rabbitMqOptions = new RabbitMQOption();
                            options.Bind(rabbitMqOptions);

                            opt.HostName = rabbitMqOptions.HostName;
                            opt.UserName = rabbitMqOptions.UserName;
                            opt.Password = rabbitMqOptions.Password;
                            opt.Port = rabbitMqOptions.Port;
                            opt.ExchangeName = rabbitMqOptions.ExchangeName;
                            opt.BasicQosOptions = new DotNetCore.CAP.RabbitMQOptions.BasicQos(3);
                        });
                        break;
                    case SuperBootstrapEventOption.ProviderOptions.REDISSTREAMS:
                    case SuperBootstrapEventOption.ProviderOptions.AMAZONSQS:
                    default:
                        throw new Exception("Not Implemented Yet!");

                }
            }).AddSubscribeFilter<SuperBootstrapEventFilter>();
            services.AddScoped<ISuperBootstrapEventPublisher, SuperBootstrapEventPublisher>();
        }
    }
}
