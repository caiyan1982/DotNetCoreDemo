/*********
* 
*   from:http://www.cnblogs.com/artech/p/asp-net-core-di-di.html
*   
*********/
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace DIDemo.Demo1
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IQux { }

    public class Foo : IFoo
    {
        public IBar Bar { get; private set; }

        [Injection]
        public IBaz Baz { get; set; }

        public Foo() { }

        [Injection]
        public Foo(IBar bar)
        {
            Bar = bar;
        }
    }
    public class Bar : IBar { }
    public class Baz : IBaz
    {
        public IQux Qux { get; private set; }

        [Injection]
        public void Initialize(IQux qux)
        {
            Qux = qux;
        }
    }
    public class Qux : IQux { }

    [AttributeUsage(AttributeTargets.Constructor
                    | AttributeTargets.Property
                    | AttributeTargets.Method, AllowMultiple = false)]
    public class InjectionAttribute : Attribute { }

    public class Cat
    {
        private ConcurrentDictionary<Type, Type> typeMapping = new ConcurrentDictionary<Type, Type>();

        public void Register<TFrom, TTo>()
        {
            Register(typeof(TFrom), typeof(TTo));
        }

        public void Register(Type from, Type to)
        {
            typeMapping[from] = to;
        }

        public object GetService(Type serviceType)
        {
            Type type;
            if (!typeMapping.TryGetValue(serviceType, out type))
            {
                type = serviceType;
            }
            if (type.IsInterface || type.IsAbstract)
            {
                return null;
            }

            ConstructorInfo constructor = this.GetConstructor(type);
            if (constructor == null)
            {
                return null;
            }

            object[] arguments = constructor.GetParameters().Select(p => this.GetService(p.ParameterType)).ToArray();
            object service = constructor.Invoke(arguments);
            this.InitializeInjectedProperties(service);
            this.InitializeInjectedMethods(service);
            return service;
        }

        public T GetService<T>() where T : class
        {
            return GetService(typeof(T)) as T;
        }

        protected virtual ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            return constructors.FirstOrDefault(c => c.GetCustomAttribute<InjectionAttribute>() != null)
                ?? constructors.FirstOrDefault();
        }

        protected virtual void InitializeInjectedProperties(object service)
        {
            PropertyInfo[] properties = service.GetType().GetProperties()
                .Where(p => p.CanWrite && p.GetCustomAttribute<InjectionAttribute>() != null).ToArray();
            Array.ForEach(properties, p => p.SetValue(service, this.GetService(p.PropertyType)));
        }

        protected virtual void InitializeInjectedMethods(object service)
        {
            MethodInfo[] methods = service.GetType().GetMethods()
                .Where(m => m.GetCustomAttribute<InjectionAttribute>() != null).ToArray();
            Array.ForEach(methods, m =>
            {
                object[] arguments = m.GetParameters().Select(p => this.GetService(p.ParameterType)).ToArray();
                m.Invoke(service, arguments);
            });
        }
    }

    class Demo1
    {
        public void Excute()
        {
            Cat cat = new Cat();
            cat.Register<IFoo, Foo>();
            cat.Register<IBar, Bar>();
            cat.Register<IBaz, Baz>();
            cat.Register<IQux, Qux>();

            IFoo service = cat.GetService<IFoo>();
            Foo foo = (Foo)service;
            Baz baz = (Baz)foo.Baz;

            Console.WriteLine("cat.GetService<IFoo>():{0}", service);
            Console.WriteLine("cat.GetService<IFoo>().Bar:{0}", foo.Bar);
            Console.WriteLine("cat.GetService<IFoo>().Baz:{0}", foo.Baz);
            Console.WriteLine("cat.GetService<IFoo>().Baz.Qux:{0}", baz.Qux);
        }
    }
}
