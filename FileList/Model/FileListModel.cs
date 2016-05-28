using Prism.Mef.Modularity;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileBrowser.FileList
{
    [Export(typeof(IFileListModel))]
    public class FileListModel : IFileListModel
    {
        #region Private members

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
            var commandLineArgs = Environment.GetCommandLineArgs();
            FilePath = (commandLineArgs.Length == 2) ? commandLineArgs[1] : String.Empty;

            this.fileList = new List<FileInfo>();
        }

        public void LoadFileList()
        {
            // ToDo: Get file list from server using WCF using FilePath
            // Until not implemented, use random data
            var randomFileList = new[]
            {
                new FileInfo { Name = "...", IsParent  = true },
                new FileInfo { Name = RandomString(15), Type = "Folder", Icon = RandomIcon(), Created = DateTime.Now, Modified = DateTime.Now.AddHours(2), IsFolder = true },
                new FileInfo { Name = RandomString(15), Type = RandomString(10), Icon = RandomIcon(), Created = DateTime.Now, Modified = DateTime.Now.AddHours(2) },
                new FileInfo { Name = RandomString(15), Type = RandomString(10), Icon = RandomIcon(), Created = DateTime.Now, Modified = DateTime.Now.AddHours(2) },
            };

            this.fileList.Clear();
            this.fileList.AddRange(randomFileList);

            OnFileListLoaded();
        }

        private void OnFileListLoaded()
        {
            FileListLoaded?.Invoke(this, new EventArgs());
        }

        #endregion Public members

        #region Private members

        private readonly Random randomGenerator = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = chars[this.randomGenerator.Next(chars.Length)];
            }
            return new string(buffer);
        }

        private ImageSource RandomIcon()
        {
            // Convert random image to byte[] (to simulate WCF input)
            var randomIcon = new BitmapImage();
            randomIcon.BeginInit();
            randomIcon.UriSource = new Uri("pack://application:,,,/FileBrowser.FileList;component/Model/TestImages/Icon" + this.randomGenerator.Next(1, 3) + ".bmp");
            randomIcon.EndInit();

            byte[] randomData;
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(randomIcon));
            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                randomData = stream.ToArray();
            }

            // Convert simulated WCF byte[] to ImageSource
            byte[] imageData = randomData.ToArray();

            using (var stream = new MemoryStream(imageData, 0, imageData.Length, false, true))
            {
                var icon = new BitmapImage();
                icon.BeginInit();
                icon.StreamSource = stream;
                icon.EndInit();
                icon.Freeze();

                return icon;
            }
        }

        #endregion Private members
    }
}
