using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileBrowser.FileList
{
    public class FileInfo
    {
        public ImageSource Icon { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public bool IsFolder { get; set; }

        public bool IsParent { get; set; }
    }
}
