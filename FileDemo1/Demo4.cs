﻿/*********
* 
*   from:http://www.cnblogs.com/artech/p/net-core-file-provider-01.html
*   
*********/
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileDemo1.Demo4
{
    class Demo4
    {
        public void Execute()
        {
            IFileProvider fileProvider = new PhysicalFileProvider(@"F:\Test");

            ChangeToken.OnChange(() => fileProvider.Watch("data.txt"), () => LoadFileAsync(fileProvider));

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
