# DesertCamel.BaseMicroservices.SuperBootstrap.Event

## Get Started

1. Setup a working [SuperBootstrapBase](https://www.nuget.org/packages/DesertCamel.BaseMicroservices.SuperBootstrap.Base) NuGet Package and configuration for your WebAPI project. [Learn more](https://github.com/mrkresnofatih/SuperBootstrapBase#readme)
2. Install the [SuperBootstrapEvent](https://www.nuget.org/packages/DesertCamel.BaseMicroservices.SuperBootstrap.Event) NuGet Package.
3. Add the following configuration in your `appsettings.json` file:
```json
{
    // other settings
    "SuperBootstrap": {
        // other superbootstrap settings
        "Event": {
            "Selected": "RabbitMq",
            "Options": {
                "RabbitMq": {
                    "HostName": "localhost",
                    "UserName": "guest",
                    "Password": "guest",
                    "Port": 5672,
                    "ExchangeName": "ex"
                }
            }
        }
    }
}
```

4. In your controller class, you implement your publishers and/or your subscribers as follows:

```c#
    public WeatherForecastController(ILogger<WeatherForecastController> logger, 
        ISuperBootstrapEventPublisher superBootstrapPublisher, 
        ICorrelationIdUtility correlationIdUtility)
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
            ObjectMessage = new DummyObject { Name = "ikan", Number = 123 },
            CorrelationId = _correlationIdUtility.Get(),
            TopicName = "event1"
        });
        return Ok();
    }

    [NonAction]
    [SuperBootstrapEventSubscribe("event1")]
    public void SubscriberOne(EventMessage eventMessage)
    {
        _logger.LogInformation($"1 -> {eventMessage.JsonMessage}");
    }
```