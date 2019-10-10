/*********
* 
*   from https://www.cnblogs.com/artech/p/net-core-file-provider-03.html
*   
*********/
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileDemo1.Demo5
{
    class Demo5
    {
        public void Execute()
        {
            IFileProvider fileProvider = new PhysicalFileProvider(@"F:\Test");

            Action<object> callback = null;
            IDisposable register = null;

            callback = _ =>
            {
                register.Dispose();
                LoadFileAsync(fileProvider);
                register = fileProvider.Watch("data.txt").RegisterChangeCallback(callback, null);
            };

            register = fileProvider.Watch("data.txt").RegisterChangeCallback(callback, null);

            while (true)
            {
                File.WriteAllText(@"F:\Test\data.txt", DateTime.Now.ToString());
                Task.Delay(5000).Wait();
            }
        }

        public static async void LoadFileAsync(IFileProvider fileProvider)
        {
            using (Stream readStream = fileProvider.GetFileInfo("data.txt").CreateReadStream())
            {
                byte[] buffer = new byte[readStream.Length];
                await readStream.ReadAsync(buffer, 0, buffer.Length);
                Console.WriteLine(Encoding.ASCII.GetString(buffer));
            }
        }
    }
}
