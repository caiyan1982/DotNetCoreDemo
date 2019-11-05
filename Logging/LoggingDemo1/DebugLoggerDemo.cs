/*********
* 
*   from:https://www.cnblogs.com/artech/p/logging-for-net-core-03.html
*   
*********/
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace LoggingDemo1
{
    class DebugLoggerDemo
    {
        public void Execute()
        {
            ILogger logger = LoggerFactory.Create(builder => builder.AddDebug()).CreateLogger(nameof(DebugLoggerDemo));

            logger.LogDebug("这是一个等级为Debug的日志");
            logger.LogInformation("这是一个等级为Information的日志");
            logger.Log(LogLevel.Error, 3721, "这是一个等级为Error的日志", new FileNotFoundException("目标文件不存在"),
                (state, exception) => $"{state}{Environment.NewLine}{exception}");
        }
    }
}
