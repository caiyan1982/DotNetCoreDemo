﻿/*********
* 
*   from https://www.cnblogs.com/artech/p/net-core-file-provider-05.html
*   
*********/
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net.Http;

namespace FileDemo3
{
    public class HttpFileProvider : IFileProvider
    {
        private readonly string _baseAddress;
        private HttpClient _httpClient;

        public HttpFileProvider(string baseAddress)
        {
            _baseAddress = baseAddress.TrimEnd('/');
            _httpClient = new HttpClient();
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            string url = $"{_baseAddress}/{subpath.TrimStart('/')}?dir-meta";
            string content = _httpClient.GetStringAsync(url).Result;
            HttpDirectoryContentsDescriptor descriptor = 
                JsonConvert.DeserializeObject<HttpDirectoryContentsDescriptor>(content);
            return new HttpDirectoryContents(descriptor, _httpClient);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            string url = $"{_baseAddress}/{subpath.TrimStart('/')}?file-meta";
            string content = _httpClient.GetStringAsync(url).Result;
            HttpFileDescriptor descriptor = JsonConvert.DeserializeObject<HttpFileDescriptor>(content);

            return descriptor.ToFileInfo(_httpClient);
        }

        public IChangeToken Watch(string filter)
        {
            return NullChangeToken.Singleton;
        }
    }
}
