/*********
* 
*   from:http://www.cnblogs.com/artech/p/asp-net-core-di-life-time.html
*   
*********/
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIDemo2.Demo4
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }

    public class Disposable : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("{0}.Dispose()", this.GetType());
        }
    }

    public class Foo : Disposable, IFoo { }
    public class Bar : Disposable, IBar { }
    public class Baz : Disposable, IBaz { }

    class Demo4
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

            child1.GetService<IFoo>();
            child1.GetService<IFoo>();
            child2.GetService<IBar>();
            child2.GetService<IBaz>();

            Console.WriteLine("child1.Dispose()");
            ((IDisposable)child1).Dispose();

            Console.WriteLine("child2.Dispose()");
            ((IDisposable)child2).Dispose();

            Console.WriteLine("root.Dispose()");
            ((IDisposable)root).Dispose();
        }
    }
}
