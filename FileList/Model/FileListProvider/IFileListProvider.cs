using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.FileList
{
    public interface IFileListProvider
    {
        event EventHandler<IEnumerable<FileInfo>> FileListChangedEvent;

        void RequestFileList(string path);
    }
}
