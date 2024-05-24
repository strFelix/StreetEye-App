using System.Globalization;

namespace StreetEye_App.Converter
{
    public class StatusTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                if (status == "Aberto")
                    return Color.FromArgb("#000000");
                else if (status == "Fechado")
                    return Color.FromArgb("#FFFFFF");
            }
            return Color.FromArgb("#00000"); // cor padrão, se o valor não corresponder a nenhum dos casos acima
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
