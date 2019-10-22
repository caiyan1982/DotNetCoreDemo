/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-08.html
*   
*********/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationDemo2
{
    public class ThreadPoolOptions
    {
        public int MinThreads { get; set; }
        public int MaxThreads { get; set; }

        public override string ToString()
        {
            return $"Thread pool size:[{MinThreads}, {MaxThreads}]";
        }
    }
    class FileConfigurationReloadDemo
    {
        public void Execute()
        {
            IConfiguration config = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "threadPool.json", ReloadOnChange = true })
                .Build();

            Action changeCallback = () =>
            {
                ThreadPoolOptions options = new ServiceCollection()
                    .AddOptions()
                    .Configure<ThreadPoolOptions>(config)
                    .BuildServiceProvider()
                    .GetService<IOptions<ThreadPoolOptions>>()
                    .Value;

                Console.WriteLine(options);
            };

            ChangeToken.OnChange(() => config.GetReloadToken(), changeCallback);

            Random random = new Random();
            while (true)
            {
                ThreadPoolOptions options = new ThreadPoolOptions
                {
                    MinThreads = random.Next(10, 20),
                    MaxThreads = random.Next(40, 50)
                };

                File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "threadPool.json"),
                    JsonSerializer.Serialize(options));
                Task.Delay(5000).Wait();
            }
        }
    }
}
