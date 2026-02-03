using Autodesk.Revit.DB;

namespace WallGeometryAnalysis.Abstractions
{
    public interface ISelectionService
    {
        Wall PickWall();
    }
}
