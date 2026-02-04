using Autodesk.Revit.DB;
using CSharpFunctionalExtensions;
using FamalyPlacment.Abstractions;
using FamalyPlacment.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamalyPlacment.Services
{
    public class PlacementService : IPlacementService
    {
        private readonly Document _document;

        public PlacementService(Document document)
        {
            _document = document;
        }

        public Result Place(PlantType plantType, int count)
        {
            return Validate(count)
                .Bind(() => FindFamily(plantType))
                .Bind(s => PlaceInstances(s, count));
        }

        private Result Validate(int count)
        {
            if (count <= 0)
                return Result.Failure("Количество должно быть больше 0");
            return Result.Success();
        }

        private Result<FamilySymbol> FindFamily(PlantType plantType)
        {
            string plantName = string.Empty;
            switch (plantType)
            {
                case PlantType.Tree:
                    plantName = "Tree";
                    break;
                case PlantType.Bush:
                    plantName = "Bush";
                    break;
                case PlantType.Flower:
                    plantName = "Flower";
                    break;
            }

            FamilySymbol familySymbol = new FilteredElementCollector(_document)
                .OfCategory(BuiltInCategory.OST_Planting)
                .OfClass(typeof(FamilySymbol))
                .OfType<FamilySymbol>()
                .Where(x => x.FamilyName.Contains(plantName))
                .FirstOrDefault();

            if (familySymbol == null)
                return Result.Failure<FamilySymbol>("Не найден типоразмер для размещения");

            return familySymbol;
        }

        private Result PlaceInstances(FamilySymbol familySymbol, int count)
        {
            try
            {
                // Определяем размеры матрицы
                int rows = (int)Math.Ceiling(Math.Sqrt(count));
                int cols = (int)Math.Ceiling((double)count / rows);

                double step = UnitUtils.ConvertToInternalUnits(5, DisplayUnitType.DUT_METERS);
                var points = new List<XYZ>();

                // Заполняем матрицу построчно
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        // Останавливаемся, когда разместили все элементы
                        if (points.Count >= count) break;

                        points.Add(new XYZ(i * step, j * step, 0));
                    }
                    if (points.Count >= count) break;
                }


                var level = new FilteredElementCollector(_document)
                    .OfClass(typeof(Level))
                    .OfType<Level>()
                    .OrderBy(l => l.Elevation)
                    .FirstOrDefault();

                if (level == null)
                    return Result.Failure("Не удалось определить уровень для размещения");

                using (Transaction transaction = new Transaction(_document, "Размещение растений"))
                {
                    transaction.Start();

                    if (!familySymbol.IsActive)
                    {
                        familySymbol.Activate();
                    }

                    foreach (var point in points)
                    {
                        _document.Create.NewFamilyInstance(
                            point,
                            familySymbol,
                            level,
                            Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    }
                    transaction.Commit();
                }
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
