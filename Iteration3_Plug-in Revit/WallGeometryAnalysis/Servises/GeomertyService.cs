using Autodesk.Revit.DB;
using System;
using WallGeometryAnalysis.Abstractions;
using WallGeometryAnalysis.Models;

namespace WallGeometryAnalysis.Servises
{
    public class GeomertyService : IGeomertyService
    {
        public WallInfo GetWallInfo(Wall wall, double thicknessLimit)
        {
            double thickness = UnitUtils.ConvertFromInternalUnits(wall.Width, DisplayUnitType.DUT_MILLIMETERS);
            return new WallInfo()
            {
                WallName = wall.Name,
                WallType = wall.WallType.Name,
                Length = Math.Round(UnitUtils.ConvertFromInternalUnits(wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(), DisplayUnitType.DUT_MILLIMETERS), 3),
                Height = Math.Round(UnitUtils.ConvertFromInternalUnits(wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).AsDouble(), DisplayUnitType.DUT_MILLIMETERS), 3),
                Thickness = Math.Round(thickness, 3),
                Volume = Math.Round(UnitUtils.ConvertFromInternalUnits(wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble(), DisplayUnitType.DUT_CUBIC_METERS), 3),
                Area = Math.Round(UnitUtils.ConvertFromInternalUnits(wall.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED).AsDouble(), DisplayUnitType.DUT_SQUARE_METERS), 3),
                IsCorrect = thickness <= thicknessLimit
            };
        }
    }
}
