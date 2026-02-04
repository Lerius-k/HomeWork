using Autodesk.Revit.DB;
using CSharpFunctionalExtensions;
using FamalyPlacment.Models;

namespace FamalyPlacment.Abstractions
{
    public interface IPlacementService
    {
        Result Place(PlantType selectedPlantType, int count);
    }
}
