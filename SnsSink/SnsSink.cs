using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Serilog.Core;
using Serilog.Events;

namespace SnsSink
{
    public class SnsSink : ILogEventSink
	{
        private readonly IFormatProvider _formatProvider;
        private readonly LogEventLevel _minimumLevel;
        private readonly IAmazonSimpleNotificationService _snsClient;
		private readonly string _logTopicArn;

		public SnsSink(IFormatProvider formatProvider, AWSOptions awsOptions, string logTopicArn) : this(formatProvider, awsOptions, logTopicArn, LogEventLevel.Verbose) {}

        public SnsSink(IFormatProvider formatProvider, AWSOptions awsOptions, string logTopicArn, LogEventLevel minimumLevel)
        {
	        _minimumLevel = minimumLevel;
			_formatProvider = formatProvider;
	        _logTopicArn = logTopicArn;
	        _snsClient = awsOptions.CreateServiceClient<IAmazonSimpleNotificationService>();
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Level < _minimumLevel) return;

	        var request = new PublishRequest
	        {
		        TopicArn = _logTopicArn,
		        Message = logEvent.RenderMessage(_formatProvider)
			};

	        _snsClient.PublishAsync(request).GetAwaiter().GetResult();
        }
	}
}
