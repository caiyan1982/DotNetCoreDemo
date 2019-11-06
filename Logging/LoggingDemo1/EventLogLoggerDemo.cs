/*********
* 
*   from:https://www.cnblogs.com/artech/p/logging-for-net-core-04.html
*   
*********/
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using System.Diagnostics;

namespace LoggingDemo1
{
    class EventLogLoggerDemo
    {
        public void Execute()
        {
            if (EventLog.SourceExists("Demo"))
            {
                EventLog.DeleteEventSource("Demo");
            }

            EventLog.CreateEventSource("Demo", "Application");

            ILogger logger = LoggerFactory.Create(builder
                => builder.AddEventLog(new EventLogSettings { SourceName = "Demo" })).CreateLogger<EventLogLoggerDemo>();

            logger.LogError("数据库连接失败（数据库：{Database}，用户名：{User}）", "TestDb", "sa");
        }
    }
}
