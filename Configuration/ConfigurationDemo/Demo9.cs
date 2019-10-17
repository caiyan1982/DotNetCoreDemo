/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-05.html
*   
*********/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationDemo
{
    class Demo9
    {
        public void Execute()
        {
            Environment.SetEnvironmentVariable("TEST_gender", "Male");
            Environment.SetEnvironmentVariable("TEST_age", "18");
            Environment.SetEnvironmentVariable("TEST_contactInfo:emailAddress", "foobar@outlook.com");
            Environment.SetEnvironmentVariable("TEST_contactInfo.phoneNo", "123456789");

            IConfiguration config = new ConfigurationBuilder()
                .AddEnvironmentVariables("TEST_")
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
