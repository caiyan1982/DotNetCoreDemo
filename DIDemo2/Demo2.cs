/*********
* 
*   from:http://www.cnblogs.com/artech/p/asp-net-core-di-life-time.html
*   
*********/
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIDemo2.Demo2
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IGux { }

    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { }
    public class Gux : IGux
    {
        public Gux(IFoo foo, IBar bar)
        {
            Console.WriteLine("Gux(IFoo, IBar)");
        }

        public Gux(IBar bar, IBaz baz)
        {
            Console.WriteLine("Gux(IBar, IBaz)");
        }
    }

    class Demo2
    {
        public void Excute()
        {
            new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient<IBaz, Baz>()
                .AddTransient<IGux, Gux>()
                .BuildServiceProvider()
                .GetService<IGux>();
        }
    }
}
