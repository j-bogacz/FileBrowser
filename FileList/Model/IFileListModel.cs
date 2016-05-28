using System;
using System.Collections.Generic;

namespace FileBrowser.FileList
{
    public interface IFileListModel
    {
        event EventHandler FileListLoaded;

        string FilePath { get; set; }

        IEnumerable<string> FileList { get; }

        void Initialize();

        void LoadFileList();
    }
}