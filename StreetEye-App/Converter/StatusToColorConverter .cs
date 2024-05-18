using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetEye_App.Converter
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                if (status == "Online")
                    return Color.FromArgb("#096101");
                else if (status == "Offline")
                    return Color.FromArgb("#c20202");
            }
            return Color.FromArgb("#000000"); // cor padrão, se o valor não corresponder a nenhum dos casos acima
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
