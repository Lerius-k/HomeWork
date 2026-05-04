using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using FirstDevTools.Servises;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace FirstDevTools
{

    public class SimpleTools : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            //создание вкладки
            application.CreateRibbonTab("PikDev");

            //создание панели с кнопкой
            var panel = application.CreateRibbonPanel("PikDev", "About");
            var buttonInfo = new PushButtonData(
                "Info",
                "О\nпанели",
                "C:\\ProgramData\\Autodesk\\Revit\\Addins\\2019\\PikDev\\FirstDevTools.dll",
                "FirstDevTools.Info"
                );

            panel.AddItem(buttonInfo);

            //создание второй панели
            var panel2 = application.CreateRibbonPanel("PikDev", "Отделка");
            //Команда PickforDeletion позволяет выбрать некоторые элементы, а затем удалить их из документа.
            var buttonTransitPararametersInFloorFromRoom = new PushButtonData(
                "FloorsTransitPararameters",
                "Перенос\nпараметров\nиз помещений\nв полы",
                "C:\\ProgramData\\Autodesk\\Revit\\Addins\\2019\\PikDev\\FirstDevTools.dll",
                "FirstDevTools.FloorsTransitPararameters"
                );

            panel2.AddItem(buttonTransitPararametersInFloorFromRoom);

            return Result.Cancelled;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Cancelled;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class Info : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Логика команды
            TaskDialog.Show("Информация", "Направление цифровой трансформации" +
                "\n\nИнструменты для ускорения работы BIM-координаторов\nв девелопменте" +
                "\n\nhttps://home.pik.ru/employees/units/32217");
            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class FloorsTransitPararameters : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            // 1. Выбор элементов пользователем с нашим фильтром
            IList<Reference> pickedRefs = null;
            try
            {
                pickedRefs = uiDoc.Selection.PickObjects(
                    ObjectType.Element,
                    new FloorFinishSelectionFilter(),
                    "Выберите перекрытия с типом \"Отделка полов\"");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                TaskDialog.Show("Инфо", "Выбор элементов отменен.");
                return Result.Cancelled;
            }

            if (pickedRefs == null || pickedRefs.Count == 0)
            {
                TaskDialog.Show("Ошибка", "Не выбрано ни одного элемента.");
                return Result.Failed;
            }

            // Получаем сами элементы из ссылок
            List<Element> selectedFloors = pickedRefs
                .Select(r => doc.GetElement(r.ElementId))
                .ToList();

            // 2. Проверка наличия трёх целевых параметров у всех выбранных перекрытий
            List<ElementId> floorsWithoutParams = new List<ElementId>();
            foreach (Element floor in selectedFloors)
            {
                if (floor.LookupParameter("СК_Отделка_Помещение_Зона") == null ||
                    floor.LookupParameter("СК_Отделка_Помещение_Подзона") == null ||
                    floor.LookupParameter("СК_Отделка_Помещение_Имя") == null)
                {
                    floorsWithoutParams.Add(floor.Id);
                }
            }

            if (floorsWithoutParams.Any())
            {
                string ids = string.Join(", ", floorsWithoutParams.Select(id => id.IntegerValue.ToString()));
                TaskDialog.Show("Целевые параметры для записи не обнаружены у следующих экземпляров:", ids);
                return Result.Failed;
            }

            // 3. Сбор всех подходящих помещений в проекте (Один раз, для эффективности)
            List<Element> allValidRooms = GetAllValidRooms(doc);

            if (allValidRooms.Count == 0)
            {
                TaskDialog.Show("Ошибка", "В проекте не найдено помещений с типом \"Помещения\" и необходимыми параметрами.");
                return Result.Failed;
            }

            // Список для помещений, пересекающих текущее перекрытие. Будет использоваться повторно.
            List<Element> intersectedRooms = new List<Element>();

            // Словарь для хранения отчёта: ключ - ID перекрытия, значение - список ID помещений
            Dictionary<ElementId, List<ElementId>> reportData =
                new Dictionary<ElementId, List<ElementId>>();

            // Счётчики для сводного отчёта
            int processedFloorsCount = 0;
            int totalRoomsCount = 0;

            // 4. Обработка каждого выбранного перекрытия
            using (Transaction transaction = new Transaction(doc, "Перенос параметров из помещений в отделку полов"))
            {
                transaction.Start();

                foreach (Element floor in selectedFloors)
                {
                    // Очищаем список перед поиском пересечений для нового перекрытия
                    intersectedRooms.Clear();

                    // 4.1. Получение геометрии перекрытия и создание солида над верхней гранью
                    Solid floorTopSolid = CreateSolidFromFloorTopFace(floor, 0.984252); // 300 мм в футах

                    if (floorTopSolid == null || floorTopSolid.Volume <= 0)
                    {
                        // Если не удалось создать солид, пропускаем это перекрытие
                        continue;
                    }

                    // 4.2. Поиск помещений, геометрия которых пересекается с солидом перекрытия
                    foreach (Element room in allValidRooms)
                    {
                        if (DoesRoomIntersectSolid(room, floorTopSolid))
                        {
                            intersectedRooms.Add(room);
                        }
                    }

                    // Сохраняем ID помещений для отчёта
                    reportData[floor.Id] = intersectedRooms.Select(r => r.Id).ToList();

                    // Если нет пересекающихся помещений, переходим к следующему перекрытию
                    if (intersectedRooms.Count == 0)
                    {
                        continue;
                    }

                    // 4.3. Проверка параметров у найденных пересекающихся помещений
                    List<ElementId> invalidRooms = new List<ElementId>();
                    foreach (Element room in intersectedRooms)
                    {
                        Parameter zoneParam = room.LookupParameter("ROM_Зона");
                        Parameter subzoneParam = room.LookupParameter("ROM_Подзона");
                        Parameter nameParam = room.LookupParameter("Имя");

                        if (zoneParam == null || subzoneParam == null || nameParam == null ||
                            string.IsNullOrEmpty(zoneParam.AsString()) ||
                            string.IsNullOrEmpty(subzoneParam.AsString()) ||
                            string.IsNullOrEmpty(nameParam.AsString()))
                        {
                            invalidRooms.Add(room.Id);
                        }
                    }

                    if (invalidRooms.Any())
                    {
                        string ids = string.Join(", ", invalidRooms.Select(id => id.IntegerValue.ToString()));
                        TaskDialog.Show("Ошибка",
                            $"У следующих помещений не хватает или не заполнен параметр для считывания (ROM_Зона, ROM_Подзона, Имя): {ids}");
                        transaction.RollBack();
                        return Result.Failed;
                    }

                    // 4.4. Извлечение и обработка значений параметров из помещений
                    string zoneValue = GetCombinedParameterValue(intersectedRooms, "ROM_Зона");
                    string subzoneValue = GetCombinedParameterValue(intersectedRooms, "ROM_Подзона");
                    string nameValue = GetCombinedParameterValue(intersectedRooms, "Имя");

                    // 4.5. Запись значений в перекрытие согласно маппингу
                    floor.LookupParameter("СК_Отделка_Помещение_Зона").Set(zoneValue);
                    floor.LookupParameter("СК_Отделка_Помещение_Подзона").Set(subzoneValue);
                    floor.LookupParameter("СК_Отделка_Помещение_Имя").Set(nameValue);

                    // Увеличиваем счётчики
                    processedFloorsCount++;
                    totalRoomsCount += intersectedRooms.Count;
                }

                transaction.Commit();
            }

            // 5. Формирование сводного отчёта
            StringBuilder summaryReport = new StringBuilder();
            summaryReport.AppendLine("СВОДНЫЙ ОТЧЁТ");
            summaryReport.AppendLine("=============");
            summaryReport.AppendLine($"Всего выбрано перекрытий: {selectedFloors.Count}");
            summaryReport.AppendLine($"Обработано перекрытий: {processedFloorsCount}");
            summaryReport.AppendLine($"Всего помещений, передавших параметры: {totalRoomsCount}");

            // 6. Формирование подробного отчёта
            StringBuilder detailedReport = new StringBuilder();
            detailedReport.AppendLine("ПОДРОБНЫЙ ОТЧЁТ");
            detailedReport.AppendLine("===============");
            detailedReport.AppendLine();

            foreach (ElementId floorId in reportData.Keys)
            {
                List<ElementId> roomIds = reportData[floorId];
                Element floor = doc.GetElement(floorId);

                detailedReport.AppendLine($"Перекрытие: ID {floorId.IntegerValue}");
                detailedReport.AppendLine($"Имя: {floor.Name}");

                if (roomIds.Count == 0)
                {
                    detailedReport.AppendLine("Помещения: не найдены");
                }
                else
                {
                    detailedReport.AppendLine($"Помещения ({roomIds.Count} шт.):");
                    foreach (ElementId roomId in roomIds)
                    {
                        Element room = doc.GetElement(roomId);
                        string roomName = room.LookupParameter("Имя")?.AsString() ?? "без имени";
                        detailedReport.AppendLine($"  - ID {roomId.IntegerValue} (Имя: {roomName})");
                    }
                }
                detailedReport.AppendLine();
            }

            // 7. Вывод сводного отчёта
            TaskDialog summaryDialog = new TaskDialog("Отчёт о переносе параметров");
            summaryDialog.MainInstruction = "Перенос параметров завершён";
            summaryDialog.MainContent = summaryReport.ToString();
            summaryDialog.CommonButtons = TaskDialogCommonButtons.Ok;
            summaryDialog.Show();

            // 8. Вопрос пользователю о подробном отчёте
            TaskDialog questionDialog = new TaskDialog("Подробный отчёт");
            questionDialog.MainInstruction = "Открыть подробный отчёт?";
            questionDialog.MainContent = "Нужно ли открыть подробный отчёт в блокноте?";
            questionDialog.CommonButtons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No;
            questionDialog.DefaultButton = TaskDialogResult.No;

            TaskDialogResult result = questionDialog.Show();

            if (result == TaskDialogResult.Yes)
            {
                // Сохраняем отчёт во временный файл и открываем в блокноте
                try
                {
                    string tempFilePath = Path.Combine(Path.GetTempPath(), $"FloorParametersReport_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                    File.WriteAllText(tempFilePath, detailedReport.ToString());
                    Process.Start("notepad.exe", tempFilePath);
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Ошибка", $"Не удалось открыть отчёт в блокноте: {ex.Message}");
                }
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// Создаёт Solid из верхней грани перекрытия, выдавленный вверх на заданную высоту
        /// </summary>
        private Solid CreateSolidFromFloorTopFace(Element floor, double height)
        {
            Options geomOptions = new Options();
            geomOptions.ComputeReferences = true;
            geomOptions.IncludeNonVisibleObjects = false;

            GeometryElement geomElem = floor.get_Geometry(geomOptions);
            if (geomElem == null)
                return null;

            // Список для хранения верхних граней
            List<Face> topFaces = new List<Face>();

            // Находим верхнюю грань перекрытия (с максимальной Z-координатой)
            foreach (GeometryObject geomObj in geomElem)
            {
                if (geomObj is Solid solid && solid.Volume > 0)
                {
                    double maxZ = double.MinValue;
                    Face topFace = null;

                    foreach (Face face in solid.Faces)
                    {
                        // Проверяем, что грань смотрит вверх (приблизительно)
                        if (face.ComputeNormal(new UV(0.5, 0.5)).Z > 0.5)
                        {
                            BoundingBoxUV uvBB = face.GetBoundingBox();
                            UV midPoint = new UV(
                                (uvBB.Min.U + uvBB.Max.U) / 2,
                                (uvBB.Min.V + uvBB.Max.V) / 2);
                            XYZ facePoint = face.Evaluate(midPoint);

                            if (facePoint.Z > maxZ)
                            {
                                maxZ = facePoint.Z;
                                topFace = face;
                            }
                        }
                    }

                    if (topFace != null)
                    {
                        topFaces.Add(topFace);
                    }
                }
                else if (geomObj is GeometryInstance geomInstance)
                {
                    // Обрабатываем вложенную геометрию
                    GeometryElement instanceGeom = geomInstance.GetInstanceGeometry();
                    foreach (GeometryObject instanceObj in instanceGeom)
                    {
                        if (instanceObj is Solid instanceSolid && instanceSolid.Volume > 0)
                        {
                            double maxZ = double.MinValue;
                            Face topFace = null;

                            foreach (Face face in instanceSolid.Faces)
                            {
                                if (face.ComputeNormal(new UV(0.5, 0.5)).Z > 0.5)
                                {
                                    BoundingBoxUV uvBB = face.GetBoundingBox();
                                    UV midPoint = new UV(
                                        (uvBB.Min.U + uvBB.Max.U) / 2,
                                        (uvBB.Min.V + uvBB.Max.V) / 2);
                                    XYZ facePoint = face.Evaluate(midPoint);

                                    if (facePoint.Z > maxZ)
                                    {
                                        maxZ = facePoint.Z;
                                        topFace = face;
                                    }
                                }
                            }

                            if (topFace != null)
                            {
                                topFaces.Add(topFace);
                            }
                        }
                    }
                }
            }

            if (topFaces.Count == 0)
                return null;

            // Используем первую найденную верхнюю грань
            Face mainTopFace = topFaces[0];

            // Получаем контур грани
            EdgeArrayArray loops = mainTopFace.EdgeLoops;
            if (loops == null || loops.Size == 0)
                return null;

            // Берем первый (внешний) контур
            EdgeArray outerLoop = loops.get_Item(0);

            // Создаем кривые контура
            List<Curve> curves = new List<Curve>();
            foreach (Edge edge in outerLoop)
            {
                curves.Add(edge.AsCurve());
            }

            // Создаем CurveLoop из контура
            CurveLoop curveLoop = CurveLoop.Create(curves);

            // Создаем экструзию вверх
            try
            {
                Solid extrudedSolid = GeometryCreationUtilities.CreateExtrusionGeometry(
                    new List<CurveLoop> { curveLoop },
                    XYZ.BasisZ,
                    height);

                return extrudedSolid;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Проверяет, пересекается ли геометрия помещения с заданным Solid
        /// </summary>
        private bool DoesRoomIntersectSolid(Element room, Solid solid)
        {
            Options geomOptions = new Options();
            geomOptions.ComputeReferences = false;
            geomOptions.IncludeNonVisibleObjects = false;

            GeometryElement geomElem = room.get_Geometry(geomOptions);
            if (geomElem == null)
                return false;

            foreach (GeometryObject geomObj in geomElem)
            {
                if (geomObj is Solid roomSolid && roomSolid.Volume > 0)
                {
                    try
                    {
                        // Проверяем пересечение солидов
                        Solid intersection = BooleanOperationsUtils.ExecuteBooleanOperation(
                            roomSolid,
                            solid,
                            BooleanOperationsType.Intersect);

                        if (intersection != null && intersection.Volume > 0)
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        // Если операция не удалась, проверяем через ограничивающие параллелепипеды
                        BoundingBoxXYZ roomBB = roomSolid.GetBoundingBox();
                        BoundingBoxXYZ solidBB = solid.GetBoundingBox();

                        Outline roomOutline = new Outline(roomBB.Min, roomBB.Max);
                        Outline solidOutline = new Outline(solidBB.Min, solidBB.Max);

                        if (roomOutline.Intersects(solidOutline, 0.001))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Возвращает все помещения с типом "Помещения", у которых есть нужные параметры и они заполнены
        /// </summary>
        private List<Element> GetAllValidRooms(Document doc)
        {
            var rooms = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Rooms)
                .WhereElementIsNotElementType()
                .Cast<Element>();

            List<Element> validRooms = new List<Element>();
            foreach (Element room in rooms)
            {
                Parameter classParam = room.LookupParameter("BDS_Class");
                if (classParam != null && classParam.AsString() == "Помещения")
                {
                    Parameter zoneParam = room.LookupParameter("ROM_Зона");
                    Parameter subzoneParam = room.LookupParameter("ROM_Подзона");
                    Parameter nameParam = room.LookupParameter("Имя");

                    if (zoneParam != null && !string.IsNullOrEmpty(zoneParam.AsString()) &&
                        subzoneParam != null && !string.IsNullOrEmpty(subzoneParam.AsString()) &&
                        nameParam != null && !string.IsNullOrEmpty(nameParam.AsString()))
                    {
                        validRooms.Add(room);
                    }
                }
            }
            return validRooms;
        }

        /// <summary>
        /// Получает уникальные значения параметра из списка элементов.
        /// Если значения разные, объединяет их через "|", иначе возвращает одно значение.
        /// </summary>
        private string GetCombinedParameterValue(List<Element> elements, string paramName)
        {
            HashSet<string> uniqueValues = new HashSet<string>();
            foreach (Element elem in elements)
            {
                Parameter param = elem.LookupParameter(paramName);
                if (param != null && !string.IsNullOrEmpty(param.AsString()))
                {
                    uniqueValues.Add(param.AsString());
                }
            }

            if (uniqueValues.Count == 0)
                return string.Empty;

            var sortedValues = uniqueValues.OrderBy(v => v);
            return string.Join("|", sortedValues);
        }
    }

}
