/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-01.html
*   
*********/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo
{
    class DateTimeFormatOptions
    {
        public string LongDatePattern { get; set; }
        public string LongTimePattern { get; set; }
        public string ShortDatePattern { get; set; }
        public string ShortTimePattern { get; set; }

        public DateTimeFormatOptions(IConfiguration config)
        {
            this.LongDatePattern = config["longDatePattern"];
            this.LongTimePattern = config["longTimePattern"];
            this.ShortDatePattern = config["shortDatePattern"];
            this.ShortTimePattern = config["shortTimePattern"];
        }
    }
    class Demo1
    {
        public void Execute()
        {
            Dictionary<string, string> source = new Dictionary<string, string>()
            {
                ["longDatePattern"] = "dddd, MMMM d, yyyy",
                ["longTimePattern"] = "h:mm:ss tt",
                ["shortDatePattern"] = "M/d/yyyy",
                ["shortTimePattern"] = "h:mm tt"
            };

            IConfiguration config = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();

            DateTimeFormatOptions options = new DateTimeFormatOptions(config);

            Console.WriteLine($"LongDatePattern:{options.LongDatePattern}");
            Console.WriteLine($"LongTimePattern:{options.LongTimePattern}");
            Console.WriteLine($"ShortDatePattern:{options.ShortDatePattern}");
            Console.WriteLine($"ShortTimePattern:{options.ShortTimePattern}");
        }
    }
}
