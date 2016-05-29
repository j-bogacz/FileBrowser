using FileBrowser.FileList;
using FileBrowser.TxtViewer;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
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
            Application.Current.MainWindow.Closing += MainWindowClosing;
            Application.Current.MainWindow.Show();
        }

        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<PubSubEvent<bool>>().Publish(true);
        }
    }
}
