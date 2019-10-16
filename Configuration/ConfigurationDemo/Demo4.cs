/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-04.html
*   
*********/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace ConfigurationDemo
{
    public enum Gender
    {
        Male,
        Female
    }

    [TypeConverter(typeof(PointTypeConventer))]
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class PointTypeConventer : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string[] splite = value.ToString().Split(',');
            double x = double.Parse(splite[0].Trim().TrimStart('('));
            double y = double.Parse(splite[1].Trim().TrimEnd(')'));
            return new Point { X = x, Y = y };
        }
    }
    class Demo4
    {
        public void Execute()
        {
            Dictionary<string, string> source = new Dictionary<string, string>
            {
                ["foo"] = "3.1415926",
                ["bar"] = "Female",
                ["baz"] = "(1.1, 2.2)"
            };

            IConfiguration config = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();

            //Debug.Assert(config.GetValue<double>("foo") == 3.1415926);
            //Debug.Assert(config.GetValue<Gender>("bar") == Gender.Female);
            //Debug.Assert(config.GetValue<Point>("baz").X == 1.1);
            //Debug.Assert(config.GetValue<Point>("baz").Y == 2.2);
            Console.WriteLine($"foo:{config.GetValue<double>("foo")}");
            Console.WriteLine($"bar:{config.GetValue<Gender>("bar")}");
            Console.WriteLine($"baz.x:{config.GetValue<Point>("baz").X}");
            Console.WriteLine($"baz.y:{config.GetValue<Point>("baz").Y}");
        }
    }
}
