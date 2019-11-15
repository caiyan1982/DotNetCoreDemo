/*********
* 
*   from:https://www.cnblogs.com/artech/p/logging-for-net-core-05.html
*   
*********/
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LoggingDemo1
{
    class TraceSourceLoggerDemo
    {
        public void Execute()
        {
            ILogger logger = LoggerFactory.Create(builder
                => builder.AddTraceSource(
                    new SourceSwitch(nameof(TraceSourceLoggerDemo), "Warning"), 
                    new ConsoleTraceListener())).CreateLogger(nameof(TraceSourceLoggerDemo));

            int eventId = 3721;

            logger.LogInformation(eventId, "升级到最新.Net Core版本({0})", "3.0");
            logger.LogWarning(eventId, "并发量接近上限({0})", 200);
            logger.LogError(eventId, "数据库连接失败(数据库：{0}，用户名：{1})", "TestDb", "sa");
        }
    }
}
