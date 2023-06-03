using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Requests
{
    public class LiquidoRequest
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public double ValorUnitario { get; set; }
        public int IdAdministrador { get; set; }
    }
}
