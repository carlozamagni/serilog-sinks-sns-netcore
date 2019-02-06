using System;
using System.IO;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace SnsSink
{
    public class SnsSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly ITextFormatter _textFormatter;
        private readonly LogEventLevel _minimumLevel;
        private readonly IAmazonSimpleNotificationService _snsClient;
        private readonly string _logTopicArn;

        public SnsSink(IFormatProvider formatProvider, AWSOptions awsOptions, string logTopicArn, string outputTemplate) : this(formatProvider, awsOptions, logTopicArn, LogEventLevel.Verbose, outputTemplate)
        {
        }

        public SnsSink(IFormatProvider formatProvider, AWSOptions awsOptions, string logTopicArn, LogEventLevel minimumLevel, string outputTemplate)
        {
            _minimumLevel = minimumLevel;
            _formatProvider = formatProvider;
            _textFormatter = new Serilog.Formatting.Display.MessageTemplateTextFormatter(outputTemplate, formatProvider);
            _logTopicArn = logTopicArn;
            _snsClient = awsOptions.CreateServiceClient<IAmazonSimpleNotificationService>();
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Level < _minimumLevel) return;

            var writer = new StringWriter();
            _textFormatter.Format(logEvent, writer);

            var renderedString = writer.ToString();

            var request = new PublishRequest
            {
                TopicArn = _logTopicArn,
                Message = renderedString
            };

            _snsClient.PublishAsync(request).GetAwaiter().GetResult();
        }
    }
}