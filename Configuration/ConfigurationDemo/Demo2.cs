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
    class CurrencyDecimalFormatOptions
    {
        public int Digits { get; set; }
        public string Symbol { get; set; }

        public CurrencyDecimalFormatOptions(IConfiguration config)
        {
            this.Digits = int.Parse(config["Digits"]);
            this.Symbol = config["Symbol"];
        }
    }

    class FormatOptions
    {
        public DateTimeFormatOptions DateTime { get; set; }
        public CurrencyDecimalFormatOptions CurrencyDecimal { get; set; }

        public FormatOptions(IConfiguration config)
        {
            this.DateTime = new DateTimeFormatOptions(config.GetSection("DateTime"));
            this.CurrencyDecimal = new CurrencyDecimalFormatOptions(config.GetSection("CurrencyDecimal"));
        }
    }
    class Demo2
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

            FormatOptions options = new FormatOptions(config.GetSection("Format"));
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
