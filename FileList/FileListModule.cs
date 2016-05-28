using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System;
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
            var viewModel = new FileListViewModel(new FileListModel());
            viewModel.Initialize();
            var view = new FileListView();
            view.DataContext = viewModel;

            RegionManager.AddToRegion("FileListRegion", view);
        }
    }
}