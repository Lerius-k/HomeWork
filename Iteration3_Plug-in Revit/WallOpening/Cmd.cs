using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Extensions.DependencyInjection;
using WallOpening.Abstractions;
using WallOpening.Servises;
using WallOpening.ViewModels;
using WallOpening.Views;

namespace WallOpening
{
    [Transaction(TransactionMode.Manual)]
    internal class Cmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // организуем контейнер компановки
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<ExternalCommandData>(commandData);
            services.AddSingleton<ISelectionService, SelectionService>();
            services.AddSingleton<IGeomertyService, GeomertyService>();
            services.AddSingleton<MainWindowViewModel, MainWindowViewModel>();
            services.AddSingleton<MainWindow, MainWindow>();
            var provider = services.BuildServiceProvider();

            var mainWindow = provider.GetRequiredService<MainWindow>(); //дай экземпляр окна, у тебя был
            
            ////"комнота знакомства"
            //SelectionService selectionService = new SelectionService(commandData);  //создать сервис для выбора элемента
            //GeomertyService geomertyService = new GeomertyService(); //создать сервис обработки геометрии

            //MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(selectionService, geomertyService); //создается экземпляр MainWindowViewModel и его свойства.
            //                                                                                     //в конструкторе MainWindowViewModel находится алгоритм команды
            //                                                                                     //и через этот же конструктор внедряются зависимости от сервисов

            //MainWindow mainWindow = new MainWindow(mainWindowViewModel); //создаем экземпляр окна и внедряем зависимостью экземпляр MainWindowViewModel и кладем в свойсво окна MainWindow, "DataContext"


            mainWindow.Show(); //показываем окно

            return Result.Succeeded;
        }
    }
}
