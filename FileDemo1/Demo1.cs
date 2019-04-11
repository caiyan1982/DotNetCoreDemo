/*********
* 
*   from:http://www.cnblogs.com/artech/p/net-core-file-provider-01.html
*   
*********/
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace FileDemo1.Demo1
{
    public interface IFileManager
    {
        void ShowStructure(Action<int, string> render);
    }

    public class FileManager : IFileManager
    {
        public IFileProvider FileProvider { get; private set; }

        public FileManager(IFileProvider fileProvider)
        {
            this.FileProvider = fileProvider;
        }

        public void ShowStructure(Action<int, string> render)
        {
            int layer = -1;

            Render("", ref layer, render);
        }

        private void Render(string subPath, ref int layer, Action<int, string> render)
        {
            layer++;

            foreach (var fileInfo in FileProvider.GetDirectoryContents(subPath))
            {
                render(layer, fileInfo.Name);
                if (fileInfo.IsDirectory)
                {
                    Render($@"{subPath}\{fileInfo.Name}".TrimStart('\\'), ref layer, render);
                }
            }

            layer--;
        }
    }
    class Demo1
    {
        public void Execute()
        {
            new ServiceCollection()
                .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"F:\Test"))
                .AddSingleton<IFileManager, FileManager>()
                .BuildServiceProvider()
                .GetService<IFileManager>()
                .ShowStructure((layer, name) => Console.WriteLine("{0}{1}", new string('\t', layer), name));
        }
    }
}
