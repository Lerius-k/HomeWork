
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
            //создание второй панели
            var panel2 = application.CreateRibbonPanel("ПИК-Привет", "SDK инструменты");
            //Команда PickforDeletion позволяет выбрать некоторые элементы, а затем удалить их из документа.
            var button1 = new PushButtonData(
                "PickforDeletion",
                "Выбрать и удалить",
                "C:\\Users\\koskovvo\\AppData\\Roaming\\Autodesk\\Revit\\Addins\\2019\\C#course\\Selections.dll",
                "Revit.SDK.Samples.Selections.CS.PickforDeletion"
                );
            //Команда PlaceAtPointOnWallFace позволяет выбрать точку на стене, а затем установить окно с фиксированным размером 36" x 48".
            var button2 = new PushButtonData(
                "PlaceAtPointOnWallFace",
                "Вставить окно по точке",
                "C:\\Users\\koskovvo\\AppData\\Roaming\\Autodesk\\Revit\\Addins\\2019\\C#course\\Selections.dll",
                "Revit.SDK.Samples.Selections.CS.PlaceAtPointOnWallFace"
                );
            //Команда PlaceAtPickedFaceWorkplane позволяет выбрать грань, установить на ней рабочую плоскость, а затем выбрать точку на рабочей плоскости в центр для создания круга.
            var button3 = new PushButtonData(
                "PlaceAtPickedFaceWorkplane",
                "Круг по плоскости",
                "C:\\Users\\koskovvo\\AppData\\Roaming\\Autodesk\\Revit\\Addins\\2019\\C#course\\Selections.dll",
                "Revit.SDK.Samples.Selections.CS.PlaceAtPickedFaceWorkplane"
                );
            //Команда SelectionDialog позволяет выбрать элемент и точку из диалога. После выбора точки элемент перемещается в выбранную точку.
            var button4 = new PushButtonData(
                "SelectionDialog",
                "Переместить в точку",
                "C:\\Users\\koskovvo\\AppData\\Roaming\\Autodesk\\Revit\\Addins\\2019\\C#course\\Selections.dll",
                "Revit.SDK.Samples.Selections.CS.SelectionDialog"
                );
            panel2.AddItem(button1);
            panel2.AddItem(button2);
            panel2.AddItem(button3);
            panel2.AddItem(button4);

            return Result.Cancelled;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Cancelled;
        }
    }
}
