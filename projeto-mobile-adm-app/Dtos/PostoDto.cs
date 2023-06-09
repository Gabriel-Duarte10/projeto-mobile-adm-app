using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Dtos
{
    public class PostoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public List<PostoAceitaLiquidoDto> LiquidosAceitos { get; set; }
        public DonoPostoDto DonoPosto { get; set; }
    }
    public class DonoPostoDto
    {
        public int Id { get; set; }
        public UsuarioDto Usuario { get; set; }

    }
    public class PostoAceitaLiquidoDto
    {
        public LiquidoDto Liquido { get; set; }
        public int CapacidadeTotal { get; set; }
        public int CapacidadeOcupada { get; set; }
    }
}
