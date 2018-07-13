# Serilog.Sinks.Sns.NETCore

Writes [Serilog](https://serilog.net/) events to a given AWS SnS topic. Please **note** that the configuration uses a .NET Core specific extension of the AWS SDK.

# Install
```

Install-Package Serilog.Sinks.Sns.NETCore
```

# Configuration
To configure the sink simply add "SnsSink" using the "WriteTo" method on the Serilog logger configuration.

```c#

new LoggerConfiguration()
	.MinimumLevel.Information()
    .WriteTo.SnsSink(config, "destination-topic-arn", LogEventLevel.Verbose)
    .CreateLogger();

```

The configuration requires an ```AWSOptions``` object, it can be configured using the .NET Core dependency injection to load the AWS credentials from your application configuration or directly from the profiles defined on the machine the application runs on.
For a complete overview of the configuration options please refer to the [official AWS SDK documentation](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-netcore.html).


### How can i run unit tests?

Just place your configuration parameters on *Configuration/TestConfig.json* config file or (better solution) you can add another config file named */Configuration/TestConfig_private.json* and use it to store you secrets. This is the preferred solution if you want to make a pull request or fork & push the code to another repo since that path is already ignored.