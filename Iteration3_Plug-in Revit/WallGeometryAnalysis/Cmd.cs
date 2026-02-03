using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Extensions.DependencyInjection;
using WallGeometryAnalysis.Abstractions;
using WallGeometryAnalysis.Servises;
using WallGeometryAnalysis.ViewModels;
using WallGeometryAnalysis.Views;

namespace WallGeometryAnalysis
{
    [Transaction(TransactionMode.Manual)]
    internal class Cmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<ExternalCommandData>(commandData);
            services.AddSingleton<ISelectionService, SelectionService>();
            services.AddSingleton<IGeomertyService, GeomertyService>();
            services.AddSingleton<MainWindowViewModel, MainWindowViewModel>();
            services.AddSingleton<MainWindow, MainWindow>();
            var provider = services.BuildServiceProvider();

            var mainWindow = provider.GetRequiredService<MainWindow>();

            mainWindow.Show(); 

            return Result.Succeeded;
        }
    }
}
