using Microsoft.Practices.ServiceLocation;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private FileInfo selectedFile;
        public FileInfo SelectedFile
        {
            get { return this.selectedFile; }
            set { SetProperty(ref this.selectedFile, value); }
        }

        public DelegateCommand LoadFileListCommand { get; set; }
        public DelegateCommand GoLevelUpCommand { get; set; }
        public DelegateCommand ListFolderCommand { get; set; }

        public void Initialize()
        {
            FileList = new ObservableCollection<FileInfo>();

            GoLevelUpCommand = new DelegateCommand(ExecuteGoLevelUp, (o) => SelectedFile != null && SelectedFile.IsParent);
            ListFolderCommand = new DelegateCommand(ExecuteListFolder, (o) => SelectedFile != null && !SelectedFile.IsParent && SelectedFile.IsFolder);
            LoadFileListCommand = new DelegateCommand(ExecuteLoadFileList, (o) => GoLevelUpCommand.CanExecute(null) || ListFolderCommand.CanExecute(null));


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

            SelectedFile = FileList.FirstOrDefault();
        }

        private void ExecuteLoadFileList(object obj)
        {
            //if (SelectedFile != null)
            //{
                if (SelectedFile.IsParent)
                {
                    ExecuteGoLevelUp(null);
                }
                else
                {
                    ExecuteListFolder(null);
                }
            //}
        }

        private void ExecuteGoLevelUp(object obj)
        {
            //if (SelectedFile != null && SelectedFile.IsParent)
            //{
                var path = Path.GetDirectoryName(FilePath);

                if (path != null)
                {
                    this.model.FilePath = path;
                    this.model.LoadFileList();
                }
            //}
        }

        private void ExecuteListFolder(object obj)
        {
            //if (SelectedFile != null && !SelectedFile.IsParent)
            //{
                this.model.FilePath = Path.Combine(FilePath, SelectedFile.Name);
                this.model.LoadFileList();
            //}
        }

        #endregion Private members
    }
}
