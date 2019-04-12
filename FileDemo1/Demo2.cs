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
using System.Text;
using System.Threading.Tasks;

namespace FileDemo1.Demo2
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

    class Demo2
    {
        public void Execute()
        {
            string content = new ServiceCollection()
                .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"F:\Test"))
                .AddSingleton<IFileManager, FileManager>()
                .BuildServiceProvider()
                .GetService<IFileManager>()
                .ReadAllTextAsync("data.txt").Result;

            Console.WriteLine(content);
            Debug.Assert(content == File.ReadAllText(@"F:\Test\data.txt"));
        }
    }
}
