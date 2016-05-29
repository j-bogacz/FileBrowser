using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace FileBrowser.TxtViewer
{
    public class DropTxtFileBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(TextBox.DragOverEvent, new DragEventHandler(DragOver), true);
            AssociatedObject.AddHandler(TextBox.DropEvent, new DragEventHandler(Drop), true);
        }

        private void DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = false;
        }

        private void Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var metadata = (string[])e.Data.GetData(DataFormats.FileDrop);

                var filePath = metadata.FirstOrDefault();
                               
                if (!String.IsNullOrWhiteSpace(filePath) && File.Exists(filePath) && Path.GetExtension(filePath) == ".txt")
                {
                    try
                    {
                        AssociatedObject.Text = File.ReadAllText(filePath, Encoding.Default);
                    }
                    catch (Exception)
                    {
                        AssociatedObject.Text = "File could not be opened. Make sure the file is a text file.";
                    }
                }
                else
                {
                    AssociatedObject.Text = "File could not be opened. Make sure the file is a text file.";
                }
            }
        }
    }
}
