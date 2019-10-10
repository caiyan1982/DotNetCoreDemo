/*********
* 
*   from https://www.cnblogs.com/artech/p/net-core-file-provider-03.html
*   
*********/
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileDemo1.Demo6
{
    #region Use a custom ChangeToken Class.
    static class ChangeToken
    {
        public static IDisposable OnChange(Func<IChangeToken> changeTokenProducer, Action changeTokenConsumer)
        {
            Action<object> callback = null;
            callback = delegate (object s)
            {
                changeTokenConsumer();
                changeTokenProducer().RegisterChangeCallback(callback, null);
            };

            return changeTokenProducer().RegisterChangeCallback(callback, null);
        }

        public static IDisposable OnChange<TState>(Func<IChangeToken> changeTokenProducer, Action<TState> changeTokenConsumer, TState state)
        {
            Action<object> callback = null;
            callback = delegate (object s)
            {
                changeTokenConsumer((TState)s);
                changeTokenProducer().RegisterChangeCallback(callback, s);
            };

            return changeTokenProducer().RegisterChangeCallback(callback, state);
        }
    }
    #endregion

    class Demo6
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
