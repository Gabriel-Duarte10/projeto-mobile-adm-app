using CommunityToolkit.Maui.Core.Platform;
using Microsoft.Maui.Controls;
using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Requests;
using projeto_mobile_adm_app.Services;
using projeto_mobile_adm_app.Views.App;
using System.Text.Json;

namespace projeto_mobile_adm_app.Views.Account;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
        InitializeComponent();

    }
    private async void LoginPost(object sender, EventArgs e)
    {
        var apiService = new ApiService();
        LoginRequest loginRequest = new LoginRequest
        {
            Login = EntryLogin.Text,
            Senha = EntrySenha.Text,
            PerfilEnum = PerfilEnum.Administrador
        };
        try
        {
            LoginDto loginResponse = await apiService.PostAsync<LoginRequest, LoginDto>("manter-conta/login", loginRequest);
            if (loginResponse != null)
            {
                string jsonString = JsonSerializer.Serialize(loginResponse);
                Preferences.Default.Set("usuarioLogado", jsonString);
                Application.Current.MainPage = new NavigationPage(new AppView());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }

    }

    private void NavigationForgotPassword(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new ForgotPasswordView());
    }

    private void NavigationCreateUser(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new CreateUserView());
    }
}