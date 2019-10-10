/*********
* 
*   from https://www.cnblogs.com/artech/p/net-core-file-provider-05.html
*   
*********/
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileDemo3
{
    public class HttpDirectoryContentsDescriptor
    {
        public bool Exists { get; set; }

        public IEnumerable<HttpFileDescriptor> FileDescriptors { get; set; }

        public HttpDirectoryContentsDescriptor()
        {
            FileDescriptors = new HttpFileDescriptor[0];
        }

        public HttpDirectoryContentsDescriptor(IDirectoryContents directoryContents, Func<string, string> physicalPathResolver)
        {
            Exists = directoryContents.Exists;
            FileDescriptors = directoryContents.Select(_ => new HttpFileDescriptor(_, physicalPathResolver));
        }
    }
}
