
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
using System.Text;

namespace ConfigurationDemo
{
    class Demo14
    {
        public void Execute()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddXmlFile("xml1.xml")
                .Build();

            Profile profile = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>(config)
                .BuildServiceProvider()
                .GetService<IOptions<Profile>>()
                .Value;

            Console.WriteLine($"Profile.Gender:{profile.Gender}");
            Console.WriteLine($"Profile.Age:{profile.Age}");
            Console.WriteLine($"Profile.ContactInfo.EmailAddress:{profile.ContactInfo.EmailAddress}");
            Console.WriteLine($"Profile.ContactInfo.PhoneNo:{profile.ContactInfo.PhoneNo}");
        }
    }
}
