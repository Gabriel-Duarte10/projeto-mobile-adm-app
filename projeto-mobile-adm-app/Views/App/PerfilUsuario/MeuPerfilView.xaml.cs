using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Views.Account;
using projeto_mobile_adm_app.Views.App.PerfilUsuario;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace projeto_mobile_adm_app.Views.App;

public partial class MeuPerfilView : ContentPage
{
	public AdministradorDto _adm;
	public MeuPerfilView()
	{
		InitializeComponent();
        LoadDataAsync();

    }
    private async Task LoadDataAsync()
    {
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        _adm = JsonSerializer.Deserialize<AdministradorDto>(jsonString);
        lblNome.Text = _adm.Usuario.Nome;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Chama a função para carregar os dados
        LoadDataAsync();
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
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        Preferences.Remove("usuarioLogado");
        Application.Current.MainPage = new NavigationPage(new LoginView());

        popup.Close();
    }
}