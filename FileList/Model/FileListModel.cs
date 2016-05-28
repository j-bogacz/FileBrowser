using Prism.Mef.Modularity;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.FileList
{
    [Export(typeof(IFileListModel))]
    public class FileListModel : IFileListModel
    {
        #region Private members

        private List<string> fileList;

        #endregion Private members

        #region Public members

        public event EventHandler FileListLoaded;

        public string FilePath { get; set; }

        public IEnumerable<string> FileList
        {
            get
            {
                return this.fileList.ToArray();
            }
        }

        public void Initialize()
        {
            var commandLineArgs = Environment.GetCommandLineArgs();
            FilePath = (commandLineArgs.Length == 2) ? commandLineArgs[1] : String.Empty;

            this.fileList = new List<string> { "File A", "File B", "File C" };
        }

        public void LoadFileList()
        {
            // Get file list from server using WCF

            OnFileListLoaded();
        }

        private void OnFileListLoaded()
        {
            FileListLoaded?.Invoke(this, new EventArgs());
        }

        #endregion Public members
    }
}
