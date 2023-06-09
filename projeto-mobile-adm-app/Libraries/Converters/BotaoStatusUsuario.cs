using projeto_mobile_adm_app.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Libraries.Converters
{
    public class BotaoStatusUsuario : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ClienteDto cliente = (ClienteDto)value;
            if (cliente == null)
            {
                return false;
            }
            int type = int.Parse(parameter.ToString());
            StatusEnum status = cliente.Usuario.StatusEnum;
            // para exibir o botao de aprovar e negar
            if (type == 1)
            {
                if (status == StatusEnum.Pendente)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // para exibir o botao de bloquear
            if (type == 2)
            {

                if (status == StatusEnum.Aprovado)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // para exibir o botao de desbloquear
            if (type == 3)
            {
                if (status == StatusEnum.Bloqueado)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
