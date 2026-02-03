using Autodesk.Revit.DB;
using WallGeometryAnalysis.Models;

namespace WallGeometryAnalysis.Abstractions
{
    public interface IGeomertyService
    {
        WallInfo GetWallInfo(Wall wall, double thicknessLimit);
    }
}
