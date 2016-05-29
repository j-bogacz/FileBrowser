using System;
using System.Collections.Generic;

namespace FileBrowser.FileList
{
    public class FileListModel : IFileListModel
    {
        #region Private members

        private List<FileInfo> fileList;

        private IFileListProvider fileListProvider;

        #endregion Private members

        #region Constructor

        public FileListModel(IFileListProvider fileListProvider)
        {
            this.fileList = new List<FileInfo>();

            this.fileListProvider = fileListProvider;
        }

        #endregion Constructor

        #region Public members

        #region IFileListModel implementation

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
            this.fileListProvider.FileListChangedEvent += OnFileListChanged; ;
        }
        
        public void Uninitialize()
        {
            this.fileListProvider.FileListChangedEvent -= OnFileListChanged;
            this.fileListProvider = null;
        }

        public void LoadFileList()
        {
            if (!String.IsNullOrWhiteSpace(FilePath))
            {
                this.fileListProvider.RequestFileList(FilePath);
            }
        }

        #endregion IFileListModel implementation

        #endregion Public members

        #region Private methods

        private void OnFileListChanged(object sender, IEnumerable<FileInfo> newFileList)
        {
            this.fileList.Clear();
            if (newFileList != null)
            {
                this.fileList.AddRange(newFileList);
            }

            OnFileListLoaded();
        }

        private void OnFileListLoaded()
        {
            FileListLoaded?.Invoke(this, new EventArgs());
        }

        #endregion Private methods
    }
}
