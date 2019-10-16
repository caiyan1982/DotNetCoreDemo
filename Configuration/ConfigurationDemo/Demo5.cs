
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
    public class Profile
    {
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }

    public class ContactInfo
    {
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
    }
    class Demo5
    {
        public void Execute()
        {
            Dictionary<string, string> source = new Dictionary<string, string>
            {
                ["gender"] = "Male",
                ["age"] = "18",
                ["contactInfo:emailAddress"] = "foobar@outlook.com",
                ["contactInfo:phoneNo"] = "123456789"
            };

            IConfiguration config = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
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
