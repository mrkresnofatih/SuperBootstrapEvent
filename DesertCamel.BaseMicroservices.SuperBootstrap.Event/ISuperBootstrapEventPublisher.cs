using DotNetCore.CAP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesertCamel.BaseMicroservices.SuperBootstrap.Event
{
    public interface ISuperBootstrapEventPublisher
    {
        FuncResponse<SuperBootstrapEventPublishResponse> Publish(SuperBootstrapEventPublishRequest publishRequest);

        Task<FuncResponse<SuperBootstrapEventPublishResponse>> PublishAsync(SuperBootstrapEventPublishRequest publishRequest);
    }

    public class SuperBootstrapEventPublisher : ISuperBootstrapEventPublisher
    {
        private readonly ICapPublisher _capPublisher;

        public SuperBootstrapEventPublisher(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        public FuncResponse<SuperBootstrapEventPublishResponse> Publish(SuperBootstrapEventPublishRequest publishRequest)
        {
            try
            {
                _capPublisher.Publish(publishRequest.TopicName, new EventMessage
                {
                    CorrelationId = publishRequest.CorrelationId,
                    JsonMessage = JsonConvert.SerializeObject(publishRequest.ObjectMessage)
                });
                return new FuncResponse<SuperBootstrapEventPublishResponse>
                {
                    Data = new SuperBootstrapEventPublishResponse()
                };
            }
            catch(Exception e)
            {
                return new FuncResponse<SuperBootstrapEventPublishResponse>
                {
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<FuncResponse<SuperBootstrapEventPublishResponse>> PublishAsync(SuperBootstrapEventPublishRequest publishRequest)
        {
            try
            {
                await _capPublisher.PublishAsync(publishRequest.TopicName, new EventMessage
                {
                    CorrelationId = publishRequest.CorrelationId,
                    JsonMessage = JsonConvert.SerializeObject(publishRequest.ObjectMessage)
                });
                return new FuncResponse<SuperBootstrapEventPublishResponse>
                {
                    Data = new SuperBootstrapEventPublishResponse()
                };
            }
            catch (Exception e)
            {
                return new FuncResponse<SuperBootstrapEventPublishResponse>
                {
                    ErrorMessage = e.Message
                };
            }
        }
    }

    public class SuperBootstrapEventPublishRequest
    {
        public object ObjectMessage { get; set; }

        public string TopicName { get; set; }

        public string CorrelationId { get; set; }
    }

    public class SuperBootstrapEventPublishResponse
    {
    }
}
