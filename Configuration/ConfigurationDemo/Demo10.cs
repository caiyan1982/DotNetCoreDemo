/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-05.html
*   
*********/
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo
{
    class Demo10
    {
        public void Execute()
        {
            while(true)
            {
                try
                {
                    Console.Write("Enter command line switches:");
                    string arguments = Console.ReadLine();
                    Dictionary<string, string> mapping = new Dictionary<string, string>
                    {
                        ["--a"] = "architecture ",
                        ["-a"] = "architecture ",
                        ["--r"] = "runtime",
                        ["-r"] = "runtime"
                    };

                    IConfiguration config = new ConfigurationBuilder()
                        .AddCommandLine(arguments.Split(' '), mapping)
                        .Build();

                    foreach (var section in config.GetChildren())
                    {
                        Console.WriteLine($"{section.Key}:{section.Value}");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
