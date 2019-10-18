/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-06.html
*   
*********/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace ConfigurationDemo
{
    class Demo18
    {
        public void Execute()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddIniFile("init2.ini")
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
