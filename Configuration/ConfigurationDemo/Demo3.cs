/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-01.html
*   
*********/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo.Demo3
{
    class DateTimeFormatOptions
    {
        public string LongDatePattern { get; set; }
        public string LongTimePattern { get; set; }
        public string ShortDatePattern { get; set; }
        public string ShortTimePattern { get; set; }
    }

    class CurrencyDecimalFormatOptions
    {
        public int Digits { get; set; }
        public string Symbol { get; set; }
    }

    class FormatOptions
    {
        public DateTimeFormatOptions DateTime { get; set; }
        public CurrencyDecimalFormatOptions CurrencyDecimal { get; set; }
    }

    class Demo3
    {
        public void Execute()
        {
            Dictionary<string, string> source = new Dictionary<string, string>
            {
                ["format:dateTime:longDatePattern"] = "dddd, MMMM d, yyyy",
                ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
                ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
                ["format:dateTime:shortTimePattern"] = "h:mm tt",
                ["format:currencyDecimal:digits"] = "2",
                ["format:currencyDecimal:symbol"] = "$"
            };

            IConfiguration config = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();

            FormatOptions options = new ServiceCollection()
                .AddOptions()
                .Configure<FormatOptions>(config.GetSection("Format"))
                .BuildServiceProvider()
                .GetService<IOptions<FormatOptions>>()
                .Value;
            DateTimeFormatOptions dateTime = options.DateTime;
            CurrencyDecimalFormatOptions currencyDecimal = options.CurrencyDecimal;

            Console.WriteLine("DateTime:");
            Console.WriteLine($"\tLongDatePattern:{dateTime.LongDatePattern}");
            Console.WriteLine($"\tLongTimePattern:{dateTime.LongTimePattern}");
            Console.WriteLine($"\tShortDatePattern:{dateTime.ShortDatePattern}");
            Console.WriteLine($"\tShortTimePattern:{dateTime.ShortTimePattern}");

            Console.WriteLine("CurrencyDecimal:");
            Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
            Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");
        }
    }
}
