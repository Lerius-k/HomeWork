using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using WallGeometryAnalysis.Abstractions;

namespace WallGeometryAnalysis.Servises
{
    public class SelectionService : ISelectionService
    {
        private readonly ExternalCommandData _commandData;

        public SelectionService(ExternalCommandData commandData)
        {
            _commandData = commandData;
        }
        public Wall PickWall()
        {
            try
            {
                Reference reference = _commandData.Application.ActiveUIDocument.Selection.PickObject(ObjectType.Element, new WallSelectionFilter());
                Wall wall = _commandData.Application.ActiveUIDocument.Document.GetElement(reference) as Wall;
                return wall;
            }
            catch
            {
                return null;
            }
        }
    }
}
