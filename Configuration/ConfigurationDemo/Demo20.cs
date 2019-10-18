/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-06.html
*   
*********/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo
{
    class Demo20
    {
        public void Execute()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddIniFile("init4.ini")
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
