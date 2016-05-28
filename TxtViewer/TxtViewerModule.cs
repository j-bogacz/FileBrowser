using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;

namespace FileBrowser.TxtViewer
{
    [ModuleExport(typeof(TxtViewerModule))]
    public class TxtViewerModule : IModule
    {
        [Import]
        IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            var viewModel = new TxtViewerViewModel();
            viewModel.Initialize();
            var view = new TxtViewerView();
            view.DataContext = viewModel;

            RegionManager.AddToRegion("TxtViewerRegion", view);
        }
    }
}