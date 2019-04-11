/*********
* 
*   from:http://www.cnblogs.com/artech/p/asp-net-core-di-life-time.html
*   
*********/
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIDemo2.Demo1
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
        public Gux(IFoo foo)
        {
            Console.WriteLine("Gux(IFoo)");
        }

        public Gux(IFoo foo, IBar bar)
        {
            Console.WriteLine("Gux(IFoo, IBar)");
        }

        public Gux(IFoo foo, IBar bar, IBaz baz)
        {
            Console.WriteLine("Gux(IFoo, IBar, IBaz)");
        }
    }

    class Demo1
    {
        public void Excute()
        {
            new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient<IGux, Gux>()
                .BuildServiceProvider()
                .GetService<IGux>();
        }
    }
}
