using projeto_mobile_adm_app.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Libraries.Converters
{
    public class ColorStatusBorderPosto : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PostoDto posto = (PostoDto)value;
            if (posto == null)
            {
                return Colors.Black;
            }
            StatusEnum status = posto.DonoPosto.Usuario.StatusEnum;
            if (status == StatusEnum.Pendente)
            {
                return Color.FromArgb("#414955");
            }
            if (status == StatusEnum.Aprovado)
            {
                return Color.FromArgb("#3F8D32");
            }
            if (status == StatusEnum.Reprovado)
            {
                return Color.FromArgb("#DA7676");
            }
            if (status == StatusEnum.Bloqueado)
            {
                return Color.FromArgb("#89AE83");
            }
            return Color.FromArgb("#414955");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
