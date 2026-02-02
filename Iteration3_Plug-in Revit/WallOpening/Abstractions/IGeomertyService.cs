using Autodesk.Revit.DB;
using WallOpening.Models;

namespace WallOpening.Abstractions
{
    public interface IGeomertyService
    {
        OpeningInfo GetOpeningInfo(FamilyInstance opening, double limit);
    }
}
