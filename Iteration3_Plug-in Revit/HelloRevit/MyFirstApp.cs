
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

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
                TaskDialog.Show("Ошибка", $"Произошла ошибка: {ex.ToString()}");
                return Result.Failed;
            }
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class FamInstancesByCategory : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //добираемся до документа 
            UIApplication uiApp = commandData.Application;
            Application app = uiApp.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            IList<Reference> pickedRefs = new List<Reference>();
            //выбираем элементы с проверкой
            try
            {
                TaskDialog.Show("Внимание", $"Внимание.\n\nДоступен выбор только загружаемых семейств.");
                pickedRefs = uiDoc.Selection.PickObjects(ObjectType.Element, new OnlyFamilyInstance(), "Выберите элементы");
            }
            catch
            {
                TaskDialog.Show("Инфо", $"Элементы не выбраны");
                return Result.Failed;
            }

            //обрабатываем полученные элементы 
            try
            {
                //создаем пустой словарь
                Dictionary<string, int> elems = new Dictionary<string, int>();
                //циклом проверяем наличие по ключу(имя категории) в словаре
                foreach (var pickedRef in pickedRefs)
                {
                    string kay = doc.GetElement(pickedRef).Category.Name; //берем имя категории
                    bool hasKey = elems.ContainsKey(kay); //проверяем содержаение этого имени в словаре
                    if (hasKey)
                        elems[kay] += 1; // если такое имя есть, прибавляем 1
                    else
                        elems.Add(kay, 1); // если такого имени нет, добавляем пару в словарь со значением 1
                }

                string showDic = ""; //создаем строковый параметр для заполения парами ключ-занчение

                //циклом заполняем парами
                foreach (KeyValuePair<string, int> pair in elems)
                {
                    showDic += $"\n{pair.Key} - {pair.Value}";
                }

                //выводим результат
                TaskDialog.Show("Результат", $"Количество элементов всего: {pickedRefs.Count}\nРаспределение по категориям:{showDic}");

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                TaskDialog.Show("Ошибка", $"Произошла ошибка: {ex.ToString()}"); // помогает найти строку, вызвавшую исключение
                return Result.Failed;
            }
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class DistanceBetweenWalls : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //добираемся до документа 
            UIApplication uiApp = commandData.Application;
            Application app = uiApp.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            IList<Reference> pickedRefs = new List<Reference>();
            //выбираем элементы с проверкой
            bool twoWallFlag = false;
            while (twoWallFlag == false)
            {

                try
                {
                    pickedRefs = uiDoc.Selection.PickObjects(ObjectType.Element, new OnlyWall(), "Выберите две стены");
                    List<ElementId> elementIds = pickedRefs
                            .Select(refObj => refObj.ElementId)
                            .ToList();
                    uiDoc.Selection.SetElementIds(elementIds); //выделяю выбранные стены, чтобы после работы скрипта они остались выделенными

                    if (pickedRefs.Count != 2)
                    {
                        TaskDialog.Show("Ошибка исходных данных", $"Выборано стен: {pickedRefs.Count} \nНужно выбрать 2 стены.\n\nСделайте повторный выбор.");
                    }
                    else
                    {
                        twoWallFlag = true;
                    }
                }
                catch
                {
                    TaskDialog.Show("Инфо", $"Элементы не выбраны");
                    return Result.Failed;
                }
            }
            try
            {
                // кладем каждую стену в отдльнй параметр            
                Wall firstWall = doc.GetElement(pickedRefs[0]) as Wall;
                Wall secondWall = doc.GetElement(pickedRefs[1]) as Wall;

                //проверка на параллельность (номали к стене должны быть паралльеьны) - векторное произведение должно дать ноль.  
                XYZ firstWallNormal = GetWallNormal(firstWall);
                XYZ secondWallNormal = GetWallNormal(secondWall);
                var result = firstWallNormal.CrossProduct(secondWallNormal);
                double tolerance = 1e-10;
                if (result.GetLength() >= tolerance)
                    TaskDialog.Show("Информация", $"Стены не являются параллельными.\nОпределить расстояние меду ними невозможно.");
                else
                {
                    // определение точек середины каждой стены            
                    XYZ point1 = GetWallMidpoint(firstWall); // точка середины первой стены
                    XYZ point2 = GetWallMidpoint(secondWall); // точка середины второй стены

                    XYZ vectorBetween = point2 - point1; // вектор между серединами стен
                                                         // вычисление расстояния между стенами
                    double distanceBetweenWalls = Math.Abs(firstWallNormal.DotProduct(vectorBetween));
                    // переводим расстояние в миллиметры
                    double distanceMm = UnitUtils.ConvertFromInternalUnits(distanceBetweenWalls, DisplayUnitType.DUT_MILLIMETERS);

                    // выводим информацию пользователю
                    TaskDialog.Show("Результат", $"Точное расстояние между двумя стенами: {distanceMm:F2} мм\n\n*по осевой линии");
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                TaskDialog.Show("Ошибка", $"Произошла ошибка: {ex.ToString()}"); // помогает найти строку, вызвавшую исключение
                return Result.Failed;
            }

            return Result.Succeeded;
        }
        public XYZ GetWallMidpoint(Wall wall)
        {
            LocationCurve location = wall.Location as LocationCurve; //берем положение стены
            Curve wallCurve = location.Curve; //берем кривую положения стены

            // Получаем начальную и конечную точки
            XYZ startPoint = wallCurve.GetEndPoint(0);
            XYZ endPoint = wallCurve.GetEndPoint(1);

            // Вычисляем середину как среднее арифметическое координат
            return (startPoint + endPoint) / 2;
        }

        public XYZ GetWallNormal(Wall wall)
        {
            LocationCurve location = wall.Location as LocationCurve;
            Curve curve = location.Curve;

            XYZ wallDirection = (curve.GetEndPoint(1) - curve.GetEndPoint(0))
              .Normalize();
            XYZ up = XYZ.BasisZ;

            // Нормаль к стене (перпендикулярно направлению и вертикали)
            return wallDirection.CrossProduct(up)
              .Normalize();
        }

        public bool AreVectorsParallel(XYZ a, XYZ b, double tolerance = 1e-10)
        {
            XYZ cross = a.CrossProduct(b);
            // Длина близка к нулю
            return cross.GetLength() < tolerance;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class ElementGeometryAnalysis : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //добираемся до документа 
            UIApplication uiApp = commandData.Application;
            Application app = uiApp.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Reference reference = null;
            try
            {
                reference = uiDoc.Selection.PickObject(ObjectType.Element, new OnlySystemInstance(), "Выберите экземпляр системного семейства");
            }
            catch
            {
                TaskDialog.Show("Инфо", $"Элемент не выбран");
                return Result.Failed;
            }

            try
            {
                var el = doc.GetElement(reference);

                Options options = new Options();
                var solids = el.get_Geometry(options)
                    .Where(g => g is Solid)
                    .OfType<Solid>()
                    .Where(g => g.Volume > 0.0001)
                    .ToList();

                int solidsCount = 0; //количество солидов
                double solidsVolume = 0; //объем солидов
                double solidsArea = 0; //площадь солидов

                int facesCount = 0; //количество граней
                double facesArea = 0; //площадь граней

                int edgesCount = 0; //количество ребер
                double edgesLength = 0; //длина ребер

                foreach (Solid solid in solids)
                {
                    solidsCount++; //количество солидов
                    solidsVolume += solid.Volume; //объем солидов
                    solidsArea += solid.SurfaceArea; //площадь солидов

                    facesCount += solid.Faces.Size; //количество граней
                    foreach (Face face in solid.Faces)
                    {
                        facesArea += face.Area; //площадь граней
                    }
                    edgesCount += solid.Edges.Size; //количество ребер
                    foreach (Edge edge in solid.Edges)
                    {
                        Curve curve = edge.AsCurve();
                        edgesLength += curve.Length; //длина ребер
                    }
                }

                TaskDialog.Show("Результат анализа", $"Количество solid: {solidsCount};" +
                    $"\nСуммарный объем solid: {UnitUtils.ConvertFromInternalUnits(solidsVolume, DisplayUnitType.DUT_CUBIC_METERS):F3} м3;" +
                    $"\nСуммарная площадь solid: {UnitUtils.ConvertFromInternalUnits(solidsArea, DisplayUnitType.DUT_SQUARE_METERS):F2} м2;" +
                    $"\nКоличество face: {facesCount};" +
                    $"\nСуммарная площадь face: {UnitUtils.ConvertFromInternalUnits(facesArea, DisplayUnitType.DUT_SQUARE_METERS):F2} м2;" +
                    $"\nКоличество edge: {edgesCount};" +
                    $"\nСуммарная длина edge: {UnitUtils.ConvertFromInternalUnits(edgesLength, DisplayUnitType.DUT_METERS):F3} м.");
            }
            catch (Exception ex)
            {
                message = ex.Message;
                TaskDialog.Show("Ошибка", $"Произошла ошибка: {ex.ToString()}"); // помогает найти строку, вызвавшую исключение
                return Result.Failed;
            }

            return Result.Succeeded;
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

            //Команда StatisticOfWall  cобирает статистику по всем стенам в проекте
            var button6 = new PushButtonData(
                "FamInstancesByCategory",
                "Cтатистика по\nкатегориям\nэкземпляров\nв выборке",
                "C:\\Users\\koskovvo\\AppData\\Roaming\\Autodesk\\Revit\\Addins\\2019\\C#course\\HelloRevit.dll",
                "HelloRevit.FamInstancesByCategory"
                );

            // Команда DistanceBetweenWalls определяет рассточние между двумя стенами
            var button7 = new PushButtonData(
                "DistanceBetweenWalls",
                "Расчет расстояния\nмежду стенами",
                "C:\\Users\\koskovvo\\AppData\\Roaming\\Autodesk\\Revit\\Addins\\2019\\C#course\\HelloRevit.dll",
                "HelloRevit.DistanceBetweenWalls"
                );

            //Команда ElementGeometryAnalysis выводит отчет о составе геометрии экземпляра системного семейства
            var button8 = new PushButtonData(
                "ElementGeometryAnalysis",
                "Анализ геометрии\nэкземпляра системного\nсемейства",
                "C:\\Users\\koskovvo\\AppData\\Roaming\\Autodesk\\Revit\\Addins\\2019\\C#course\\HelloRevit.dll",
                "HelloRevit.ElementGeometryAnalysis"
                );

            panel3.AddItem(button5);
            panel3.AddItem(button6);
            panel3.AddItem(button7);
            panel3.AddItem(button8);

            return Result.Cancelled;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Cancelled;
        }
    }
}
