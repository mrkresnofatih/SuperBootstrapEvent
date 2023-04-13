using DesertCamel.BaseMicroservices.SuperBootstrap.Base;
using DesertCamel.BaseMicroservices.SuperBootstrap.Event;
using Microsoft.AspNetCore.Mvc;

namespace SuperBootstrapEvent.SampleProjectOne.WebApi.Controllers;

[ApiController]
[Route("weather")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ISuperBootstrapEventPublisher _superBootstrapPublisher;
    private readonly ICorrelationIdUtility _correlationIdUtility;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ISuperBootstrapEventPublisher superBootstrapPublisher, ICorrelationIdUtility correlationIdUtility)
    {
        _logger = logger;
        _superBootstrapPublisher = superBootstrapPublisher;
        _correlationIdUtility = correlationIdUtility;
    }

    [HttpGet("publish3")]
    public async Task<ActionResult> PublishThree()
    {
        await _superBootstrapPublisher.PublishAsync(new SuperBootstrapEventPublishRequest
        {
            ObjectMessage = new DummyObject { Name = "ikan", Number = 123 },
            CorrelationId = _correlationIdUtility.Get(),
            TopicName = "event3"
        });
        return Ok();
    }

    [HttpGet("publish4")]
    public async Task<ActionResult> PublishFour()
    {
        await _superBootstrapPublisher.PublishAsync(new SuperBootstrapEventPublishRequest
        {
            ObjectMessage = new DummyObject { Name = "kucing", Number = 123 },
            CorrelationId = _correlationIdUtility.Get(),
            TopicName = "event4"
        });
        return Ok();
    }

    [NonAction]
    [SuperBootstrapEventSubscribe("event1")]
    public void SubscriberOne(EventMessage eventMessage)
    {
        _logger.LogInformation($"1 -> {eventMessage.JsonMessage}");
    }

    [NonAction]
    [SuperBootstrapEventSubscribe("event2")]
    public void SubscriberTwo(EventMessage eventMessage)
    {
        _logger.LogInformation($"2 -> {eventMessage.JsonMessage}");
    }

    public class DummyObject
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
