
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace HelloRevit
{
    [Transaction(TransactionMode.Manual)]
    public class MyCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Логика команды
            TaskDialog.Show("Информация", "Привет, Revit!");
            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class MyFirstApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            //создание вкладки
            application.CreateRibbonTab("ПИК-Привет");

            //создание панели с кнопкой
            var panel = application.CreateRibbonPanel("ПИК-Привет", "Общее");
            var button = new PushButtonData(
                "FirstApp",
                "Приветствие",
                "C:\\Users\\koskovvo\\AppData\\Roaming\\Autodesk\\Revit\\Addins\\2019\\C#course\\HelloRevit.dll",
                "HelloRevit.MyCommand"
                );

            panel.AddItem(button);
            return Result.Cancelled;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Cancelled;
        }
    }
}
