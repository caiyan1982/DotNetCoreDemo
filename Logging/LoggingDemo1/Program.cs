﻿using System;

namespace LoggingDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Demo1 demo1 = new Demo1();
            //demo1.Execute();

            ConsoleLoggerDemo consoleLoggerDemo = new ConsoleLoggerDemo();
            consoleLoggerDemo.Execute();

            Console.Read();
        }
    }
}