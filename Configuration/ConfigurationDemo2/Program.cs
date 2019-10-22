using System;

namespace ConfigurationDemo2
{


    class Program
    {
        static void Main(string[] args)
        {
            DbConfigurationDemo dbConfigurationDemo = new DbConfigurationDemo();
            dbConfigurationDemo.Execute();

            Console.Read();
        }
    }
}
