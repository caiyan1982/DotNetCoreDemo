/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-04.html
*   
*********/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo
{
    public class Options
    {
        public Profile[] Profiles { get; set; }
    }

    class Demo7
    {
        public void Execute()
        {
            Dictionary<string, string> source = new Dictionary<string, string>
            {
                ["profiles:foo:gender"] = "Male",
                ["profiles:foo:age"] = "18",
                ["profiles:foo:contactInfo:emailAddress"] = "foo@outlook.com",
                ["profiles:foo:contactInfo:phoneNo"] = "123",
                ["profiles:bar:gender"] = "Male",
                ["profiles:bar:age"] = "25",
                ["profiles:bar:contactInfo:emailAddress"] = "bar@outlook.com",
                ["profiles:bar:contactInfo:phoneNo"] = "456",
                ["profiles:baz:gender"] = "Female",
                ["profiles:baz:age"] = "36",
                ["profiles:baz:contactInfo:emailAddress"] = "baz@outlook.com",
                ["profiles:baz:contactInfo:phoneNo"] = "789"
            };

            IConfiguration config = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();

            Options options = new ServiceCollection()
                .AddOptions()
                .Configure<Options>(config)
                .BuildServiceProvider()
                .GetService<IOptions<Options>>()
                .Value;

            foreach (var profile in options.Profiles)
            {
                Console.WriteLine(profile.Gender);
                Console.WriteLine(profile.Age);
                Console.WriteLine(profile.ContactInfo.EmailAddress);
                Console.WriteLine(profile.ContactInfo.PhoneNo);
            }
        }
    }
}
