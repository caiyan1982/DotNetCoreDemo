using System;
using System.Threading.Tasks;

namespace FileDemo3Client
{
    public interface IFileManager
    {
        void ShowStructure(Action<int, string> render);

        Task<string> ReadAllTextAsync(string path);
    }
}
