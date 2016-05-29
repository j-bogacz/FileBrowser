using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;

namespace FileBrowser.FileList
{
    [ModuleExport(typeof(FileListModule))]
    public class FileListModule : IModule
    {
        [Import]
        IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            try
            {
                var fileListProvider = new FileListProvider();
                fileListProvider.Initialize();

                var model = new FileListModel(fileListProvider);
                model.Initialize();

                var viewModel = new FileListViewModel(model);
                viewModel.Initialize();

                var view = new FileListView();
                view.DataContext = viewModel;

                RegionManager.AddToRegion("FileListRegion", view);
                
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<PubSubEvent<bool>>().Subscribe((args) =>
                {
                    model.Uninitialize();
                    fileListProvider.Uninitialize();
                });
            }
            catch
            {
                RegionManager.AddToRegion("FileListRegion", "Unable to connect to file server...");
            }
        }
    }
}