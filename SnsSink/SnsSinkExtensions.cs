using System;
using System.Collections.Generic;
using System.Text;
using Amazon.Extensions.NETCore.Setup;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace SnsSink
{
    public static class SnsSinkExtensions
    {
        public static LoggerConfiguration SnsSink(this LoggerSinkConfiguration config, AWSOptions configuration, string logTopicArn, LogEventLevel minimumLevel = LogEventLevel.Verbose, IFormatProvider formatProvider = null)
        {
            return config.Sink(new SnsSink(formatProvider, configuration, logTopicArn, minimumLevel));
        }
	}
}
