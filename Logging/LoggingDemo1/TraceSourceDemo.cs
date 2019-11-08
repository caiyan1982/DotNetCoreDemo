/*********
* 
*   from:https://www.cnblogs.com/artech/p/logging-for-net-core-05.html
*   
*********/
using System;
using System.Diagnostics;

namespace LoggingDemo1
{
    public class ConsoleTraceListener : TraceListener
    {
        public override void Write(string message) => Console.Write(message);

        public override void WriteLine(string message) => Console.WriteLine(message);
    }

    class TraceSourceDemo
    {
        public void Execute()
        {
            TraceSource traceSource = new TraceSource(nameof(TraceSourceDemo), SourceLevels.Warning);
            traceSource.Listeners.Add(new ConsoleTraceListener());

            int eventId = 3721;

            traceSource.TraceEvent(TraceEventType.Information, eventId, "升级到最新.Net Core版本({0})", "3.0");
            traceSource.TraceEvent(TraceEventType.Warning, eventId, "并发量接近上限({0})", 200);
            traceSource.TraceEvent(TraceEventType.Error, eventId, "数据库连接失败(数据库：{0}，用户名：{1})", "TestDb", "sa");
        }
    }
}
