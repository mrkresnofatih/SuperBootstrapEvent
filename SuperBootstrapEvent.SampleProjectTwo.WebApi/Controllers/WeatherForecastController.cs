using DesertCamel.BaseMicroservices.SuperBootstrap.Base;
using DesertCamel.BaseMicroservices.SuperBootstrap.Event;
using Microsoft.AspNetCore.Mvc;

namespace SuperBootstrapEvent.SampleProjectTwo.WebApi.Controllers;

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

    [HttpGet("publish1")]
    public async Task<ActionResult> PublishOne()
    {
        await _superBootstrapPublisher.PublishAsync(new SuperBootstrapEventPublishRequest
        {
            ObjectMessage = new DummyObject { Name = "cumi", Number = 123 },
            CorrelationId = _correlationIdUtility.Get(),
            TopicName = "event1"
        });
        return Ok();
    }

    [HttpGet("publish2")]
    public async Task<ActionResult> PublishTwo()
    {
        await _superBootstrapPublisher.PublishAsync(new SuperBootstrapEventPublishRequest
        {
            ObjectMessage = new DummyObject { Name = "udang", Number = 123 },
            CorrelationId = _correlationIdUtility.Get(),
            TopicName = "event2"
        });
        return Ok();
    }

    [NonAction]
    [SuperBootstrapEventSubscribe("event3")]
    public void SubscriberThree(EventMessage eventMessage)
    {
        _logger.LogInformation($"3 -> {eventMessage.JsonMessage}");
    }

    [NonAction]
    [SuperBootstrapEventSubscribe("event4")]
    public void SubscriberFour(EventMessage eventMessage)
    {
        _logger.LogInformation($"4 -> {eventMessage.JsonMessage}");
    }

    public class DummyObject
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
