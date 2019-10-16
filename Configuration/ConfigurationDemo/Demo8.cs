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
    class Demo8
    {
        public void Execute()
        {
            Dictionary<string, string> source = new Dictionary<string, string>
            {
                ["foo:gender"] = "Male",
                ["foo:age"] = "18",
                ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
                ["foo:contactInfo:phoneNo"] = "123",
                ["bar:gender"] = "Male",
                ["bar:age"] = "25",
                ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
                ["bar:contactInfo:phoneNo"] = "456",
                ["baz:gender"] = "Female",
                ["baz:age"] = "36",
                ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
                ["baz:contactInfo:phoneNo"] = "789"
            };

            IConfiguration config = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();

            Dictionary<string, Profile> profiles = new ServiceCollection()
                .AddOptions()
                .Configure<Dictionary<string, Profile>>(config)
                .BuildServiceProvider()
                .GetService<IOptions<Dictionary<string, Profile>>>()
                .Value;

            foreach (var profile in profiles.Values)
            {
                Console.WriteLine(profile.Gender);
                Console.WriteLine(profile.Age);
                Console.WriteLine(profile.ContactInfo.EmailAddress);
                Console.WriteLine(profile.ContactInfo.PhoneNo);
            }
        }
    }
}
