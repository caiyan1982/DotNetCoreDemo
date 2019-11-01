/*********
* 
*   from:https://www.cnblogs.com/artech/p/logging-for-net-core-01.html
*   
*********/
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace LoggingDemo1
{
    class Demo1
    {
        public void Execute()
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            loggerFactory.AddProvider(new DebugLoggerProvider());
            ILogger logger = loggerFactory.CreateLogger(nameof(Program));

            int eventId = 123;

            logger.LogInformation(eventId, "升级到最新.Net Core版本({version})", "1.0.0");
            logger.LogWarning(eventId, "并发量接近上限({maximum})", 200);
            logger.LogError(eventId, "数据库连接失败(数据库：{Database}，用户名：{User})", "TestDb", "sa");
        }
    }
}
