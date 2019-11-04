using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingDemo1
{
    class ConsoleLoggerDemo
    {
        public void Execute()
        {
            IConfiguration loggerConfig = new ConfigurationBuilder()
                .AddJsonFile("logging.json")
                .Build();

            ConsoleLoggerOptions loggerOptions = new ServiceCollection()
                .AddOptions()
                .Configure<ConsoleLoggerOptions>(loggerConfig)
                .BuildServiceProvider()
                .GetService<IOptions<ConsoleLoggerOptions>>()
                .Value;

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder 
                => builder.AddConfiguration(loggerConfig).AddConsole());
            ILogger logger = loggerFactory.CreateLogger("App4");
            int eventId = 123;

            logger.LogInformation(eventId, "升级到最新.Net Core版本({version})", "1.0.0");
            logger.LogWarning(eventId, "并发量接近上限({maximum})", 200);
            logger.LogError(eventId, "数据库连接失败(数据库：{Database}，用户名：{User})", "TestDb", "sa");
        }
    }
}
