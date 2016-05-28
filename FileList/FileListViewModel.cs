using Microsoft.Practices.ServiceLocation;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.FileList
{
    public class FileListViewModel : BindableBase
    {
        #region Private members

        IFileListModel model;

        #endregion Private members

        #region Constructor

        public FileListViewModel(IFileListModel model)
        {
            this.model = model;
        }

        #endregion Constructor

        #region Public members

        private string filePath;
        public string FilePath
        {
            get { return this.filePath; }
            set { SetProperty(ref this.filePath, value); }
        }

        public ObservableCollection<FileInfo> FileList { get; set; }

        public void Initialize()
        {
            FileList = new ObservableCollection<FileInfo>();

            this.model.Initialize();
            this.model.FileListLoaded += OnFileListLoaded;
            this.model.LoadFileList();
        }

        #endregion Public members

        #region Private members

        private void OnFileListLoaded(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            FilePath = this.model.FilePath;

            FileList.Clear();
            FileList.AddRange(this.model.FileList);
        }

        #endregion Private members
    }
}
