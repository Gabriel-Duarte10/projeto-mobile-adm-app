using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Libraries.Converters
{
    public class DataStringCriacao : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime data = (DateTime)value;
            if(data == null)
            {
                return "Data não informada";
            }

            string dataString = data.ToString("dd/MM/yyyy 'as' HH:mm");

            return "Criado dia " + dataString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
