/*********
* 
*   from https://www.cnblogs.com/artech/p/net-core-file-provider-05.html
*   
*********/
using FileDemo3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace FileDemo3Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileManager fileManager = new ServiceCollection()
                .AddSingleton<IFileProvider>(new HttpFileProvider("http://localhost:5000/dir1"))
                .AddSingleton<IFileManager, FileManager>()
                .BuildServiceProvider()
                .GetService<IFileManager>();

            fileManager.ShowStructure((layer, name) => Console.WriteLine($"{new string('\t', layer)}{name}"));
            string content1 = fileManager.ReadAllTextAsync("foobar/foo.txt").Result;
            Console.WriteLine("foo.txt content is {0}", content1);
            Console.Read();
        }
    }
}
