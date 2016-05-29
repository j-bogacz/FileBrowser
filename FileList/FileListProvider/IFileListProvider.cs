using System;
using System.Collections.Generic;

namespace FileBrowser.FileList
{
    public interface IFileListProvider
    {
        event EventHandler<IEnumerable<FileInfo>> FileListChangedEvent;

        void RequestFileList(string path);
    }
}
