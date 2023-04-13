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