using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace FirstDevTools.Servises
{
    public class FloorFinishSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            // Проверяем категорию: перекрытия
            if (elem.Category.Id.IntegerValue != (int)BuiltInCategory.OST_Floors)
                return false;

            // Проверяем параметр BDS_Class на значение "Отделка полов"
            Parameter classParam = elem.LookupParameter("BDS_Class");
            if (classParam != null && classParam.StorageType == StorageType.String)
            {
                return classParam.AsString() == "Отделка полов";
            }

            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            // Запрещаем выбор по ссылкам (например, по граням)
            return false;
        }
    }
}
