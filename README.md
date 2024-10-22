# Setup

To run this webapp, you will need a `.\src\DEFRA.Trade.API.DAERA.RFL\appsettings.Development.json` file. The file will need the following structure:

```jsonc 
{
  "DetailedErrors": true,
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "DaeraRflApiSettings": {
    "BaseUrl": "<secret>",
    "DaeraRflApiPathV1": "<secret>"
    "TestApiUri": "<secret>"
  },
    "ConfigurationServer": {
        "ConnectionString": "<secret>",
        "TenantId": "<secret>"
    },
  "SocSettings": {
    "EventHubName": "<secret>",
    "EventHubConnectionString": "<secret>"
  },
  "ProtectiveMonitoringSettings": {
    "Enabled": true,
    "Environment": "DEV"
  }
}
```

Secrets reference can be found here: https://dev.azure.com/defragovuk/DEFRA-TRADE-APIS/_wiki/wikis/DEFRA-TRADE-APIS.wiki/26086
