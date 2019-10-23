using System;

namespace ConfigurationDemo2
{


    class Program
    {
        static void Main(string[] args)
        {
            //DbConfigurationDemo dbConfigurationDemo = new DbConfigurationDemo();
            //dbConfigurationDemo.Execute();

            //FileConfigurationReloadDemo reloadDemo = new FileConfigurationReloadDemo();
            //reloadDemo.Execute();

            ExtendXmlConfigurationDemo xmlConfigurationDemo = new ExtendXmlConfigurationDemo();
            xmlConfigurationDemo.Execute();

            Console.Read();
        }
    }
}
