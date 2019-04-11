using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FileDemo2.Demo1
{
    class Demo1
    {
        public void Execute()
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            string[] resourceNames = assembly.GetManifestResourceNames();

            foreach (var resourceName in resourceNames)
            {
                Console.WriteLine(resourceName);
            }
        }
    }
}
