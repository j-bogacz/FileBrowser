using FileBrowser.FileList.FileBrowserServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileBrowser.FileList
{
    public class FileListProvider : IFileListProvider, IFileBrowserServiceCallback
    {
        #region Private members

        private FileBrowserServiceClient client;

        #endregion Private members

        #region Constructor

        public FileListProvider()
        {
            var instanceContext = new InstanceContext(this);
            this.client = new FileBrowserServiceClient(instanceContext);
        }

        #endregion Constructor

        #region Public members

        #region IFileListProvider implementation

        public event EventHandler<IEnumerable<FileInfo>> FileListChangedEvent;

        public void RequestFileList(string path)
        {
            this.client.RequestFileList(path);
        }

        #endregion IFileListProvider implementation

        #region IFileBrowserServiceCallback implementation

        public void FileListChanged(FileBrowserServiceReference.FileInfo[] fileList)
        {
            var newFileList = fileList.Select(info => GetFileInfo(info)).ToArray();

            FileListChangedEvent?.Invoke(this, newFileList);
        }

        #endregion IFileBrowserServiceCallback implementation

        #endregion Public members

        #region Private methods

        private FileInfo GetFileInfo(FileBrowserServiceReference.FileInfo info)
        {
            return new FileInfo
            {
                Icon = GetIcon(info.Icon),
                Name = info.Name,
                Type = info.Type,
                Created = info.Created,
                Modified = info.Modified,
                IsFolder = info.IsFolder,
                IsParent = info.IsParent
            };
        }

        private ImageSource GetIcon(byte[] icon)
        {
            if (icon == null)
            {
                return null;
            }

            using (var stream = new MemoryStream(icon, 0, icon.Length, false, true))
            {
                var imageSource = new BitmapImage();
                imageSource.BeginInit();
                imageSource.StreamSource = stream;
                imageSource.EndInit();
                imageSource.Freeze();

                return imageSource;
            }
        }

        #endregion Private methods
    }
}
