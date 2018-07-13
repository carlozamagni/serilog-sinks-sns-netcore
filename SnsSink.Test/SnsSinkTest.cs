using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Xunit;

namespace SnsSink.Test
{
    public class SnsSinkTest
    {
        private const string PrivateConfigFilePostfix = "private";

		private AWSOptions LoadConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}/Configuration")
                .AddJsonFile("TestConfig.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"TestConfig_{PrivateConfigFilePostfix}.json", optional: false, reloadOnChange: true)
                .Build();

            return config.GetAWSOptions();
        }

        [Fact]
        public void ConfigurationTest()
        {
            var config = LoadConfiguration();
            var log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.SnsSink(config, "", LogEventLevel.Verbose)
                .CreateLogger();

            Assert.NotNull(log);
        }
	}
}
