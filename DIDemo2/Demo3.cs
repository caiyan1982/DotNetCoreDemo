/*********
* 
*   from:http://www.cnblogs.com/artech/p/asp-net-core-di-life-time.html
*   
*********/
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIDemo2.Demo3
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }

    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { }

    class Demo3
    {
        public void Excute()
        {
            IServiceProvider root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar, Bar>()
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider();
            IServiceProvider child1 = root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            IServiceProvider child2 = root.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;

            Console.WriteLine("ReferenceEquals(root.GetService<IFoo>(), root.GetService<IFoo>() = {0}",
                ReferenceEquals(root.GetService<IFoo>(), root.GetService<IFoo>()));
            Console.WriteLine("ReferenceEquals(child1.GetService<IBar>(), child1.GetService<IBar>() = {0}",
                ReferenceEquals(child1.GetService<IBar>(), child1.GetService<IBar>()));
            Console.WriteLine("ReferenceEquals(child1.GetService<IBar>(), child2.GetService<IBar>() = {0}",
                ReferenceEquals(child1.GetService<IBar>(), child2.GetService<IBar>()));
            Console.WriteLine("ReferenceEquals(child1.GetService<IBaz>(), child2.GetService<IBaz>() = {0}",
                ReferenceEquals(child1.GetService<IBaz>(), child2.GetService<IBaz>()));
        }
    }
}
