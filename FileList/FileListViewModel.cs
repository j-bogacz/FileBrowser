using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FileBrowser.FileList
{
    public class FileListViewModel : BindableBase
    {
        #region Private members

        private string filePath;
        private FileInfo selectedFile;

        private string eventLog;

        IFileListModel model;

        #endregion Private members

        #region Constructor

        public FileListViewModel(IFileListModel model)
        {
            FileList = new ObservableCollection<FileInfo>();

            this.model = model;
        }

        #endregion Constructor

        #region Public members

        public string FilePath
        {
            get { return this.filePath; }
            set { SetProperty(ref this.filePath, value); }
        }

        public ObservableCollection<FileInfo> FileList { get; set; }

        public FileInfo SelectedFile
        {
            get { return this.selectedFile; }
            set { SetProperty(ref this.selectedFile, value); }
        }

        public DelegateCommand LoadFileListCommand { get; set; }
        public DelegateCommand GoLevelUpCommand { get; set; }
        public DelegateCommand ListFolderCommand { get; set; }

        public string EventLog
        {
            get { return this.eventLog; }
            set { SetProperty(ref this.eventLog, value); }
        }

        public void Initialize()
        {
            GoLevelUpCommand = new DelegateCommand(ExecuteGoLevelUp, () => SelectedFile != null && SelectedFile.IsParent);
            ListFolderCommand = new DelegateCommand(ExecuteListFolder, () => SelectedFile != null && !SelectedFile.IsParent && SelectedFile.IsFolder);
            LoadFileListCommand = new DelegateCommand(ExecuteLoadFileList, () => GoLevelUpCommand.CanExecute() || ListFolderCommand.CanExecute());

            var commandLineArgs = Environment.GetCommandLineArgs();

            if ((commandLineArgs.Length != 2) || !Path.IsPathRooted(commandLineArgs[1]))
            {
                LogEvent("Incorrect number of arguments or passed argument doesn't represents file path");
                return;
            }

            FilePath = Path.GetPathRoot(commandLineArgs[1]);
            if (!FilePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                FilePath += Path.DirectorySeparatorChar;
            }

            this.model.FileListLoaded += OnFileListLoaded;

            try
            {
                LogEvent("Requesting initial 'LoadFileList': " + FilePath);

                this.model.FilePath = FilePath;
                this.model.LoadFileList();

                LogEvent("Request for initial 'LoadFileList' queued: " + FilePath);
            }
            catch (Exception ex)
            {
                LogEvent("Exception requesting initial 'LoadFileList': " + ex.Message);
            }
        }

        #endregion Public members

        #region Private methods

        private void OnFileListLoaded(object sender, EventArgs e)
        {
            LogEvent("Request for new file list handled: " + this.model.FilePath);

            UpdateData();
        }

        private void UpdateData()
        {
            FilePath = this.model.FilePath;

            FileList.Clear();
            FileList.AddRange(this.model.FileList);

            SelectedFile = FileList.FirstOrDefault();
        }

        private void ExecuteLoadFileList()
        {
            if (SelectedFile.IsParent)
            {
                ExecuteGoLevelUp();
            }
            else
            {
                ExecuteListFolder();
            }
        }

        private void ExecuteGoLevelUp()
        {
            var path = Path.GetDirectoryName(FilePath);

            if (path != null)
            {
                try
                {
                    LogEvent("Requesting 'GoLevelUp': " + path);

                    this.model.FilePath = path;
                    this.model.LoadFileList();

                    LogEvent("Request for 'GoLevelUp' queued: " + path);
                }
                catch (Exception ex)
                {
                    LogEvent("Exception requesting 'GoLevelUp': " + ex.Message);
                }
            }
        }

        private void ExecuteListFolder()
        {
            var path = Path.Combine(FilePath, SelectedFile.Name);

            if (path != null)
            {
                try
                {
                    LogEvent("Requesting 'ListFolder': " + path);

                    this.model.FilePath = path;
                    this.model.LoadFileList();

                    LogEvent("Request for 'ListFolder' queued: " + path);
                }
                catch (Exception ex)
                {
                    LogEvent("Exception requesting 'ListFolder': " + ex.Message);
                }
            }
        }

        private void LogEvent(string message)
        {
            EventLog += String.Format("{0}: {1}\n", DateTime.Now.ToString("HH:mm:ss.fff"), message);
        }

        #endregion Private methods
    }
}
