using FamalyPlacment.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FamalyPlacment.ViewModels
{
    public class PlantTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PlantType type)
            {
                switch (type)
                {
                    case PlantType.Tree:
                        return "Tree";
                    case PlantType.Bush:
                        return "Bush";
                    case PlantType.Flower:
                        return "Flower";
                    default:
                        return value != null ? value.ToString() : string.Empty;
                }
            }

            return value != null ? value.ToString() : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                switch (str)
                {
                    case "Tree":
                        return PlantType.Tree;
                    case "Bush":
                        return PlantType.Bush;
                    case "Flower":
                        return PlantType.Flower;
                    default:
                        throw new ArgumentException(string.Format("Неизвестный тип мебели: {0}", str));
                }
            }

            throw new ArgumentException("Некорректное значение для конвертации");
        }
    }
}
