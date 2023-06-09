using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPFouCNPJ { get; set; }
        public string Email { get; set; }
        public PerfilEnum PerfilEnum { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public StatusEnum StatusEnum { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
    }
    public class UsuarioPerfil
    {
        public int UsuarioId { get; set; }
        public int PerfilUsuarioId { get; set; }
        public PerfilEnum PerfilEnum { get; set; }
    }
    public enum PerfilEnum
    {
        // Adicione os valores do enum PerfilEnum aqui
        Administrador = 1,
        Cliente = 2,
        DonoPosto = 3,
        FuncionarioPosto = 4
    }
    public enum StatusEnum
    {
        // Adicione os valores do enum StatusEnum aqui
        Pendente = 1,
        Aprovado = 2,
        Reprovado = 3,
        Bloqueado = 4,
        Cancelado = 5
    }
}
