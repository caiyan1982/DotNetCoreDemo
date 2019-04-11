/*********
* 
*   from:http://www.cnblogs.com/artech/p/asp-net-core-di-register.html
*   
*********/
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DIDemo.Demo2
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IGux
    {
        IFoo Foo { get; }
        IBar Bar { get; }
        IBaz Baz { get; }
    }

    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { }
    public class Gux : IGux
    {
        public IFoo Foo { get; private set; }
        public IBar Bar { get; private set; }
        public IBaz Baz { get; private set; }

        public Gux(IFoo foo, IBar bar, IBaz baz)
        {
            this.Foo = foo;
            this.Bar = bar;
            this.Baz = baz;
        }
    }

    #region 服务实例集合
    public interface IFooBar { }

    public class FooBar1 : IFooBar { }

    public class FooBar2 : IFooBar { }
    #endregion

    #region 泛型
    public interface IFooBar<T1, T2>
    {
        T1 Foo { get; }

        T2 Bar { get; }
    }

    public class FooBar<T1, T2> : IFooBar<T1, T2>
    {
        public T1 Foo { get; private set; }

        public T2 Bar { get; private set; }

        public FooBar(T1 foo, T2 bar)
        {
            Foo = foo;
            Bar = bar;
        }
    }
    #endregion

    class Demo2
    {
        public void Excute()
        {
            IServiceCollection services = new ServiceCollection()
                .AddSingleton<IFoo, Foo>()
                .AddSingleton<IBar>(new Bar())
                .AddSingleton<IBaz>(_ => new Baz())
                .AddSingleton<IGux, Gux>()
                .AddSingleton<IFooBar, FooBar1>()
                .AddSingleton<IFooBar, FooBar2>()
                .AddTransient(typeof(IFooBar<,>), typeof(FooBar<,>));

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            Console.WriteLine("serviceProvider.GetService<IFoo>(): {0}", serviceProvider.GetService<IFoo>());
            Console.WriteLine("serviceProvider.GetService<IBar>(): {0}", serviceProvider.GetService<IBar>());
            Console.WriteLine("serviceProvider.GetService<IBaz>(): {0}", serviceProvider.GetService<IBaz>());
            Console.WriteLine("serviceProvider.GetService<IGux>(): {0}", serviceProvider.GetService<IGux>());
            Console.WriteLine("serviceProvider.GetService<IFooBar>(): {0}", serviceProvider.GetService<IFooBar>());

            IEnumerable<IFooBar> services1 = serviceProvider.GetServices<IFooBar>();
            int index = 1;
            Console.WriteLine("serviceProvider.GetService<IFooBar>(): ");

            foreach (IFooBar foobar in services1)
            {
                Console.WriteLine("{0}:{1}", index++, foobar);
            }

            Console.WriteLine("serviceProvider.GetService<IFooBar<IFoo, IBar>>().Foo: {0}",
                serviceProvider.GetService<IFooBar<IFoo, IBar>>().Foo);
            Console.WriteLine("serviceProvider.GetService<IFooBar<IFoo, IBar>>().Bar: {0}",
                serviceProvider.GetService<IFooBar<IFoo, IBar>>().Bar);
        }
    }
}
