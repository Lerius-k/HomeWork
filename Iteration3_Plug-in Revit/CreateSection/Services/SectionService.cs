using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CreateSection.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreateSection.Services
{
    public class SectionService : ISectionService
    {
        private readonly ExternalCommandData _commandData;
        const int sectionCount = 3;

        public SectionService(
            ExternalCommandData commandData)
        {
            _commandData = commandData;
        }

        public bool CreateSection(
            FamilyInstance instance,
            double widthOffsetMm,
            double depthOffsetMm,
            double heightOffsetMm,
            string sectionName)
        {
            var doc = _commandData.Application.ActiveUIDocument.Document;

            var bbox = instance.get_BoundingBox(null); //взять bb объекта

            if (bbox == null)
            {
                return false;
            }

            var center = (bbox.Min + bbox.Max) / 2; // нашли у бб центр
            var size = bbox.Max - bbox.Min; // нашли размер бб

            //переводим отступы во внутренние единицы измередния
            var widthOffset = UnitUtils.ConvertToInternalUnits(widthOffsetMm, DisplayUnitType.DUT_MILLIMETERS);
            var depthOffset = UnitUtils.ConvertToInternalUnits(depthOffsetMm, DisplayUnitType.DUT_MILLIMETERS);
            var heightOffset = UnitUtils.ConvertToInternalUnits(heightOffsetMm, DisplayUnitType.DUT_MILLIMETERS);

            Transform transform = Transform.CreateTranslation(XYZ.Zero); //создаем локальную систему координат

            transform.Origin = center; // задаем ноль локальной системе коорднинат в центре объекта

            //создаем новй бб 
            var sectionBox = new BoundingBoxXYZ();

            //лист координатных точек. в цикле перествлем их последовательно в листе. и поновым координатам создаем разрез 
            List<XYZ> points = new List<XYZ>()
            {
                (XYZ.BasisZ.CrossProduct(XYZ.BasisY)).Normalize(),
                XYZ.BasisZ,
                XYZ.BasisY
            };

            //в цикле создаем разрезы по уникальному положению локальной системы коордниант
            for (int i = 0; i < sectionCount; i++)
            {
                int sectionNumber = i + 1;
                //задаем новое направление всям локальной системы координат !влияет на направление построения разреза
                transform.BasisX = points[0];
                transform.BasisY = points[1];
                transform.BasisZ = points[2];

                sectionBox.Transform = transform; //! перемещаем бб в новую систему координат                

                //задаем размеры созданному бб с учетом отступов
                sectionBox.Min = new XYZ(-size.X / 2 - widthOffset, -size.Z / 2 - depthOffset, -size.Y / 2 - heightOffset);
                sectionBox.Max = new XYZ(size.X / 2 + widthOffset, size.Z / 2 + depthOffset, size.Y / 2 + heightOffset);

                //находим в документе тип рзареза
                var viewType = new FilteredElementCollector(doc)
                    .OfClass(typeof(ViewFamilyType))
                    .OfType<ViewFamilyType>()
                    .FirstOrDefault(x => x.ViewFamily == ViewFamily.Section);

                if (viewType == null)
                {
                    return false;
                }

                try
                {
                    using (var transaction = new Transaction(doc, "VisualizeTransform"))
                    {
                        transaction.Start();
                        var viewSection = ViewSection.CreateSection(doc, viewType.Id, sectionBox); //создаем в докумденте разрез заданного типа по бб с отступами
                        viewSection.Name = $"{sectionName} {sectionNumber}";

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

                //меняем положение точек в листе
                XYZ temp = points[0];
                points.RemoveAt(0);
                points.Add(temp);
            }
            return true;
        }
    }
}
