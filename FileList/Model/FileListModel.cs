using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace FileBrowser.FileList
{
    [Export(typeof(IFileListModel))]
    public class FileListModel : IFileListModel
    {
        #region Private members

        private IFileListProvider fileListProvider;

        private List<FileInfo> fileList;

        #endregion Private members

        #region Public members

        public event EventHandler FileListLoaded;

        public string FilePath { get; set; }

        public IEnumerable<FileInfo> FileList
        {
            get
            {
                return this.fileList.ToArray();
            }
        }

        public void Initialize()
        {
            this.fileListProvider = new FileListProvider();
            this.fileListProvider.FileListChangedEvent += OnFileListChanged; ;

            var commandLineArgs = Environment.GetCommandLineArgs();
            FilePath = (commandLineArgs.Length == 2) ? commandLineArgs[1] : String.Empty;

            this.fileList = new List<FileInfo>();
        }

        public void LoadFileList()
        {
            if (!String.IsNullOrWhiteSpace(FilePath))
            {
                this.fileListProvider.RequestFileList(FilePath);
            }
        }

        #endregion Public members

        #region Private members

        private void OnFileListChanged(object sender, IEnumerable<FileInfo> e)
        {
            this.fileList.Clear();
            this.fileList.AddRange(e);

            OnFileListLoaded();
        }

        private void OnFileListLoaded()
        {
            FileListLoaded?.Invoke(this, new EventArgs());
        }

        #endregion Private members
    }
}
