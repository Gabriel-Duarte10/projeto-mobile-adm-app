using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Dtos
{
    public class DashboardDto
    {

    }
    public class DashboardClienteDto
    {
        public int QtdTransacoes { get; set; }
        public int? QtdDoada { get; set; }
        public ClienteDto Cliente { get; set; }
    }
    public class DashboardLiquidoDto
    {
        public int QtdTransacoes { get; set; }
        public int? QtdDoada { get; set; }
        public LiquidoDto Liquido { get; set; }
    }
    public class DashboardFuncionarioPostoDto
    {
        public int QtdTransacoes { get; set; }
        public int? QtdDoada { get; set; }
        public FuncionarioPostoDto FuncionarioPosto { get; set; }
    }
    public class DashboardUsinaDto
    {
        public int QtdTransacoes { get; set; }
        public int? QtdDoada { get; set; }
        public UsinaDto Usina { get; set; }
    }
    public class DashboardPostoDto
    {
        public int QtdTransacoes { get; set; }
        public int? QtdDoada { get; set; }
        public PostoDto Posto { get; set; }
    }
}
