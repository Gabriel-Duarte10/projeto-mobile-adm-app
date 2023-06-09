using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Views.App.PerfilUsuario;
using System.Collections.ObjectModel;

namespace projeto_mobile_adm_app.Views.App;

public partial class MeuPerfilView : ContentPage
{
	private readonly AdministradorDto _adm;
	public MeuPerfilView()
	{
		InitializeComponent();
        _adm = new AdministradorDto()
        {
            Id = 1,
            Usuario = new UsuarioDto
            {
                Id = 1,
                Nome = "Usuario Adm",
                CPFouCNPJ = "12345678910",
                Email = "usuario1@email.com",
                PerfilEnum = PerfilEnum.Cliente,
                CEP = "12345678",
                Rua = "Rua A",
                Numero = 1,
                UF = "UF",
                Cidade = "Cidade",
                StatusEnum = StatusEnum.Bloqueado,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        };
        lblNome.Text = _adm.Usuario.Nome;
	}

    private void EditarPerfil(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new EditarUsuario());
    }
    private async void RedefinirSenha(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmação", "Tem certeza que desja redefinir sua senha?", "Confirmar", "Cancelar");
        if (answer)
        {
            Navigation.PushModalAsync(new RedefinirSenha(_adm));
        }
    }
    private void SairConta(object sender, EventArgs e)
    {

    }
}