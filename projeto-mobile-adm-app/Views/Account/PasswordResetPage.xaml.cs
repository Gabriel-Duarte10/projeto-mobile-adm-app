using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Requests;
using projeto_mobile_adm_app.Services;

namespace projeto_mobile_adm_app.Views.Account;

public partial class PasswordResetPage : ContentPage
{
    public int? UserId;
    public PasswordResetPage(string token)
	{
		InitializeComponent();
        var tokenService = new TokenService();
        UserId = tokenService.GetUserIdFromToken(token);
	}

    private async void RedefiniSenha(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            if (entrySenha.Text != entryConfirmaSenha.Text)
            {
                await DisplayAlert("Erro", "As senhas não coincidem", "Ok");
                popup.Close();
                return;
            }
            if (entrySenha.Text.Length < 6)
            {
                await DisplayAlert("Erro", "A senha deve conter no mínimo 6 caracteres", "Ok");
                popup.Close();
                return;
            }
            var usuarioRedefinirSenhaRequest = new UsuarioRedefinirSenhaRequest
            {
                UsuarioId = UserId,
                Senha = entrySenha.Text,
            };
            await RedefinirSenhaAsync(usuarioRedefinirSenhaRequest);
            popup.Close();
        }
        catch(Exception ex)
        {
            popup.Close();
        }
    }
    private async Task RedefinirSenhaAsync(UsuarioRedefinirSenhaRequest usuarioRedefinirSenhaRequest)
    {
        try
        {
            var apiService = new ApiService();
            var result = await apiService.PutAsync<UsuarioRedefinirSenhaRequest, object>("usuario/redefinir-senha", usuarioRedefinirSenhaRequest);
            await DisplayAlert("Sucesso", "Senha redefinida com sucesso", "Ok");
            await Navigation.PopModalAsync();

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}