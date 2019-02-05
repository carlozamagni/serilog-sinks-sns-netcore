using System;
using Amazon.Extensions.NETCore.Setup;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace SnsSink
{
    public static class SnsSinkExtensions
    {
        private const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}";

        public static LoggerConfiguration SnsSink(this LoggerSinkConfiguration config, AWSOptions configuration, string logTopicArn, LogEventLevel minimumLevel = LogEventLevel.Verbose, IFormatProvider formatProvider = null)
        {
            return config.Sink(new SnsSink(formatProvider, configuration, logTopicArn, minimumLevel, DefaultOutputTemplate));
        }

        public static LoggerConfiguration SnsSink(this LoggerSinkConfiguration config, AWSOptions configuration, string logTopicArn, LogEventLevel minimumLevel = LogEventLevel.Verbose, IFormatProvider formatProvider = null, string outputTemplate = DefaultOutputTemplate)
        {
            return config.Sink(new SnsSink(formatProvider, configuration, logTopicArn, minimumLevel, outputTemplate));
        }
    }
}