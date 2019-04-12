/*********
* 
*   from:http://www.cnblogs.com/artech/p/net-core-file-provider-01.html
*   
*********/
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileDemo1.Demo3
{
    public interface IFileManager
    {
        Task<string> ReadAllTextAsync(string path);
    }

    public class FileManager : IFileManager
    {
        public IFileProvider FileProvider { get; private set; }

        public FileManager(IFileProvider fileProvider)
        {
            FileProvider = fileProvider;
        }

        public async Task<string> ReadAllTextAsync(string path)
        {
            byte[] buffer;

            using (Stream readStream = FileProvider.GetFileInfo(path).CreateReadStream())
            {
                buffer = new byte[readStream.Length];
                await readStream.ReadAsync(buffer, 0, buffer.Length);
            }

            return Encoding.ASCII.GetString(buffer);
        }
    }

    class Demo3
    {
        public void Execute()
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            string content1 = new ServiceCollection()
            .AddSingleton<IFileProvider>(new EmbeddedFileProvider(assembly))
            .AddSingleton<IFileManager, FileManager>()
            .BuildServiceProvider()
            .GetService<IFileManager>()
            .ReadAllTextAsync("data.txt").Result;

            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.data.txt");
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            string content2 = Encoding.ASCII.GetString(buffer);

            Console.WriteLine($"content1 is {content1}");
            Console.WriteLine($"content2 is {content2}");

            Debug.Assert(content1 == content2);
        }
    }
}
