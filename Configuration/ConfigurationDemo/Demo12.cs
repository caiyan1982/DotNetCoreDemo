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
    class Demo12
    {
        public void Execute()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("json2.json")
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
