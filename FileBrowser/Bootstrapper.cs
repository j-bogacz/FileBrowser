using FileBrowser.FileList;
using FileBrowser.TxtViewer;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;
using UserInterface;

namespace FileBrowser
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(FileListModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(TxtViewerModule).Assembly));
        }

        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
