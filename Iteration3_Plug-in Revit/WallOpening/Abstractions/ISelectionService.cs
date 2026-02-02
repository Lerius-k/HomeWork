using Autodesk.Revit.DB;

namespace WallOpening.Abstractions
{
    public interface ISelectionService
    {
        FamilyInstance PickOpening();
    }
}
