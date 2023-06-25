using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Dtos
{
    public class FuncionarioPostoDto
    {
        public int Id { get; set; }
        public UsuarioDto Usuario { get; set; }
        public PostoDto Posto { get; set; }
    }
}
