﻿/*********
* 
*   from https://www.cnblogs.com/artech/p/net-core-file-provider-05.html
*   
*********/
using Microsoft.Extensions.FileProviders;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace FileDemo3
{
    public class HttpDirectoryContents : IDirectoryContents
    {
        private IEnumerable<IFileInfo> _fileInfos;

        public bool Exists { get; private set; }

        public HttpDirectoryContents(HttpDirectoryContentsDescriptor descriptor, HttpClient httpClient)
        {
            Exists = descriptor.Exists;
            _fileInfos = descriptor.FileDescriptors.Select(file => file.ToFileInfo(httpClient));
        }

        public IEnumerator<IFileInfo> GetEnumerator() => _fileInfos.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _fileInfos.GetEnumerator();
    }
}
