using Autodesk.Revit.DB;

namespace CreateSection.Abstractions
{
    public interface ISectionService
    {
        bool CreateSection(FamilyInstance familyInstance, double widthOffsetMm, double depthOffsetMm, double heightOffsetMm, string sectionName);
    }
}
