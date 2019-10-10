/*********
* 
*   from https://www.cnblogs.com/artech/p/net-core-file-provider-01.html
*   
*********/
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileDemo3Client
{
    class FileManager : IFileManager
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
}
