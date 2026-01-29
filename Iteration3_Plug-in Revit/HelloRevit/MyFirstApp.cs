
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;

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
    public class StatisticOfWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //добираемся до документа 
            UIApplication uiApp = commandData.Application;
            Application app = uiApp.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            try
            {
                //отбираем все стены
                var walls = new FilteredElementCollector(doc)
                .OfClass(typeof(Wall))
                .OfType<Wall>()
                .ToList();

                // Проверяем, есть ли стены
                if (walls.Count == 0)
                {
                    TaskDialog.Show("Ошибка", "В проекте нет стен.");
                    return Result.Failed;
                }

                //сортируем лист по возрастанию
                var sortedByLength = walls
                    .OrderBy(l => l.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble())
                    .ToList();
                //рассчитываем среднее значение длины 
                double totalLenght = 0;
                foreach (var wall in sortedByLength)
                {
                    totalLenght += UnitUtils.ConvertFromInternalUnits(wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), DisplayUnitType.DUT_MILLIMETERS);
                }
                double averageLength = totalLenght / walls.Count;

                // находим самую длинную стену и значение длины
                Wall theLongestWall = sortedByLength.LastOrDefault();
                Parameter longestWallParameter = theLongestWall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                //double valueLongestWallParameter = longestWallParameter.AsDouble(); //длина самой длинной (без конвертации)
                string valueLongestWallParameterString = longestWallParameter.AsValueString(); //длина строкой

                // находим самую короткую стену и значение длины
                Wall theShortestWall = sortedByLength.FirstOrDefault();
                Parameter shortestWallParameter = theShortestWall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                //double valueShortestWallParameter = shortestWallParameter.AsDouble(); //длина самой короткой  (без конвертации)
                string valueShortestWallParameterString = shortestWallParameter.AsValueString(); //длина строкой

                //изменяем параметр "Комментарии"
                using (Transaction t = new Transaction(doc, "Пометить короткую и длинную стену в \"Комментарии\""))
                {
                    t.Start();
                    Parameter commentParamTheLongestWall = theLongestWall.LookupParameter("Комментарии");
                    Parameter commentParamTheShortestWall = theShortestWall.LookupParameter("Комментарии");
                    commentParamTheLongestWall.Set("Самая длинная стена");
                    commentParamTheShortestWall.Set("Самая короткая стена");
                    t.Commit();
                }

                // Выыод статистики
                TaskDialog.Show("Статистика по стенам.", $"Общее количество стен: {walls.Count} шт;" +
                    $"\nСамая короткая стена: {valueShortestWallParameterString} мм;" +
                    $"\nСамая длинная стена: {valueLongestWallParameterString} мм;" +
                    $"\nСреднее значение длины: {averageLength:F2} мм;" +
                    $"\n\nСамая длинная и короткая стены помечены комментарием.");
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                TaskDialog.Show("Ошибка", $"Произошла ошибка: {ex.Message}");
                return Result.Failed;
            }
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class MyFirstApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            //создание вкладки
            application.CreateRibbonTab("Lerius-K");

            //создание панели с кнопкой
            var panel = application.CreateRibbonPanel("Lerius-K", "Старт");
            var button = new PushButtonData(
                "FirstApp",
                "Приветствие",
                "C:\\Users\\koskovvo\\AppData\\Roaming\\Autodesk\\Revit\\Addins\\2019\\C#course\\HelloRevit.dll",
                "HelloRevit.MyCommand"
                );

            panel.AddItem(button);
            //создание второй панели
            var panel2 = application.CreateRibbonPanel("Lerius-K", "SDK инструменты");
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


            //создание третьей панели (Команды из заданий)
            var panel3 = application.CreateRibbonPanel("Lerius-K", "Здания курса C#");
            //Команда StatisticOfWall  cобирает статистику по всем стенам в проекте
            var button5 = new PushButtonData(
                "StatisticOfWall",
                "Cтатистика по стенам",
                "C:\\Users\\koskovvo\\AppData\\Roaming\\Autodesk\\Revit\\Addins\\2019\\C#course\\HelloRevit.dll",
                "HelloRevit.StatisticOfWall"
                );
            
            panel3.AddItem(button5);

            return Result.Cancelled;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Cancelled;
        }
    }
}
