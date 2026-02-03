using Autodesk.Revit.DB;
using WallOpening.Abstractions;
using WallOpening.Models;

namespace WallOpening.Servises
{
    public class GeomertyService : IGeomertyService
    {
        /// <summary>
        /// Получает инфу о проеме
        /// </summary>
        /// <param name="opening">проем</param>
        /// <param name="limit">допустимый лимит высоты стены над проемом</param>
        /// <returns></returns>
        public OpeningInfo GetOpeningInfo(FamilyInstance opening, double limit)
        {
            double currentDistance = GetCurrentDistance(opening);

            return new OpeningInfo()
            {
                Name = opening.Name,
                Distance = currentDistance,
                IsCorrect = currentDistance < limit
            };
        }

        private double GetCurrentDistance(FamilyInstance opening)
        {
            double maxZOpening = GetMaxZ(opening);
            double maxZHost = GetMaxZ(opening.Host);
            
            return UnitUtils.ConvertFromInternalUnits( maxZHost - maxZOpening, DisplayUnitType.DUT_MILLIMETERS);
        }

        private double GetMaxZ(Element elem)
        {
            var box = elem.get_BoundingBox(null);
            return box.Max.Z;
        }
    }
}
