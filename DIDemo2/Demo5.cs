/*********
* 
*   from:http://www.cnblogs.com/artech/p/asp-net-core-di-life-time.html
*   
*********/
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIDemo2.Demo5
{
    public interface IFoobar : IDisposable { }

    public class Foobar : IFoobar
    {
        ~Foobar()
        {
            Console.WriteLine("Foobar.Finalize()");
        }

        public void Dispose()
        {
            Console.WriteLine("Foobar.Dispose");
        }
    }

    class Demo5
    {
        public void Execute()
        {
            IServiceProvider serviceProvider = new ServiceCollection()
                .AddTransient<IFoobar, Foobar>()
                .BuildServiceProvider();

            Exec(serviceProvider);
            GC.Collect();

            Console.WriteLine("------------------");

            Exec2(serviceProvider);
            GC.Collect();
        }

        private void Exec(IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<IFoobar>().Dispose();
        }

        private void Exec2(IServiceProvider serviceProvider)
        {
            using (IServiceScope serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<IFoobar>();
            }
        }
    }
}
