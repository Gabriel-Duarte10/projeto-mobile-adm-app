using projeto_mobile_adm_app.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Requests
{
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public PerfilEnum PerfilEnum { get; set; }
    }
    public class UsuarioRequest
    {
        public UsuarioDadosRequest Dados { get; set; }
        public UsuarioContaRequest Conta { get; set; }
    }
    public class UsuarioDadosRequest
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPFouCNPJ { get; set; }
        public PerfilEnum PerfilEnum { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public StatusEnum StatusEnum { get; set; }
        public int? PostoId { get; set; }
    }
    public class UsuarioContaRequest
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
    public class UsuarioRedefinirSenhaRequest
    {
        public int? UsuarioId { get; set; }
        public string Senha { get; set; }
    }
}
