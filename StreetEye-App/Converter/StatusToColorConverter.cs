using System.Globalization;

namespace StreetEye_App.Converter
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                if (status == "Online" || status == "Aberto")
                    return Color.FromArgb("#39EF61");
                else if (status == "Offline" || status == "Fechado")
                    return Color.FromArgb("#EF3939");
            }
            return Color.FromArgb("#FFFFFF"); // cor padrão, se o valor não corresponder a nenhum dos casos acima
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
