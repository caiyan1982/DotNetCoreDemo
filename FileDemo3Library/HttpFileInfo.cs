using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Net.Http;

namespace FileDemo3
{
    public class HttpFileInfo : IFileInfo
    {
        private HttpClient _httpClient;

        public bool Exists { get; private set; }

        public long Length { get; private set; }

        public string PhysicalPath { get; private set; }

        public string Name { get; private set; }

        public DateTimeOffset LastModified { get; private set; }

        public bool IsDirectory { get; private set; }

        public HttpFileInfo(HttpFileDescriptor descriptor, HttpClient httpClient)
        {
            Exists = descriptor.Exists;
            IsDirectory = descriptor.IsDirectory;
            LastModified = descriptor.LastModified;
            Length = descriptor.Length;
            Name = descriptor.Name;
            PhysicalPath = descriptor.PhysicalPath;
            _httpClient = httpClient;
        }

        public Stream CreateReadStream()
        {
            HttpResponseMessage message = _httpClient.GetAsync(PhysicalPath).Result;
            return message.Content.ReadAsStreamAsync().Result;
        }
    }
}
