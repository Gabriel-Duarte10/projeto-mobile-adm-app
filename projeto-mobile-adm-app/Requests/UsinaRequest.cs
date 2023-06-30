using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Requests
{
    public class UsinaRequest
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public int IdAdministrador { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
    }
}
