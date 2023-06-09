using projeto_mobile_adm_app.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Libraries.Converters
{
    public class BotaoStatusPosto : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PostoDto posto = (PostoDto)value;
            if (posto == null)
            {
                return false;
            }
            // converter parameter para int e colocar em type
            int type = int.Parse(parameter.ToString());
            StatusEnum status = posto.DonoPosto.Usuario.StatusEnum;
            // para exibir o botao de aprovar e negar
            if(type == 1)
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
            // para definir o texto de status
            if (type == 4)
            {
                if (status == StatusEnum.Aprovado)
                {
                    return "Aprovado";
                }
                if(status == StatusEnum.Bloqueado)
                {
                    return "Bloqueado";
                }
                if (status == StatusEnum.Reprovado)
                {
                    return "Reprovado";
                }
            }
            // para exibir o botao de desbloquear
            if (type == 5)
            {
                if (status == StatusEnum.Aprovado)
                {
                    return true;
                }
                if (status == StatusEnum.Bloqueado)
                {
                    return true;
                }
                if (status == StatusEnum.Reprovado)
                {
                    return true;
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
