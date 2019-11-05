/*********
* 
*   from:https://www.cnblogs.com/artech/p/logging-for-net-core-02.html
*   
*********/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LoggingDemo1
{
    class ConsoleLoggerDemo
    {
        public void Execute()
        {
            IConfiguration loggerConfig = new ConfigurationBuilder()
                .AddJsonFile("logging.json")
                .Build();

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
