using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.PlatformConfiguration;
using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Requests;
using projeto_mobile_adm_app.Services;
using System.Text.Json;
using static projeto_mobile_adm_app.Services.ViaCEPService;

namespace projeto_mobile_adm_app.Views.Account;

public partial class CreateUserView : ContentPage
{
    
    public CreateUserView()
	{
		InitializeComponent();
        entryCep.TextChanged += (sender, e) =>
        {
            var entry = sender as Entry;
            int oldLength = e.OldTextValue?.Length ?? 0;
            int newLength = e.NewTextValue?.Length ?? 0;

            // Only proceed if the user is adding characters, not deleting them.
            if (newLength > oldLength)
            {
                string text = new string(entry.Text.Where(ch => Char.IsDigit(ch)).ToArray());

                if (text.Length >= 2)
                    text = text.Insert(2, ".");
                if (text.Length >= 6)
                    text = text.Insert(6, "-");
                if (text.Length > 10) // Limit the text length to valid CEP length
                {
                    text = text.Remove(10);
                }

                entry.Text = text;
                entry.CursorPosition = entry.Text.Length;
            }
        };

        entryCPFCNPJ.TextChanged += (sender, e) =>
        {
            var entry = sender as Entry;
            int oldLength = e.OldTextValue?.Length ?? 0;
            int newLength = e.NewTextValue?.Length ?? 0;

            // Only proceed if the user is adding characters, not deleting them.
            if (newLength > oldLength)
            {
                string text = new string(entry.Text.Where(ch => Char.IsDigit(ch)).ToArray());

                if (text.Length > 11)
                {
                    if (text.Length >= 2)
                        text = text.Insert(2, ".");
                    if (text.Length >= 6)
                        text = text.Insert(6, ".");
                    if (text.Length >= 10)
                        text = text.Insert(10, "/");
                    if (text.Length >= 15)
                        text = text.Insert(15, "-");
                    if (text.Length > 18) // Limit the text length to valid CNPJ length
                    {
                        text = text.Remove(18);
                    }
                }
                else
                {
                    if (text.Length >= 3)
                        text = text.Insert(3, ".");
                    if (text.Length >= 7)
                        text = text.Insert(7, ".");
                    if (text.Length >= 11)
                        text = text.Insert(11, "-");
                    if (text.Length > 14) // Limit the text length to valid CPF length
                    {
                        text = text.Remove(14);
                    }
                }

                entry.Text = text;
                entry.CursorPosition = entry.Text.Length;
            }
        };

        createUser.Opacity = 0.2;
    }

    private async void CreateUser(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            if(entrySenha != entryConfirmaSenha)
            {
                await DisplayAlert("Erro", "As senhas não conferem", "Ok");
                popup.Close();
                return;
            }
            if(entrySenha.Text.Count() < 6)
            {
                await DisplayAlert("Erro", "A senha deve conter no mínimo 6 caracteres", "Ok");
                popup.Close();
                return;
            }
            var usuarioRequest = new UsuarioRequest
            {
                Dados = new UsuarioDadosRequest
                {
                    Nome = entryNome.Text,
                    Email = entryEmail.Text,
                    CPFouCNPJ = entryCPFCNPJ.Text,
                    PerfilEnum = PerfilEnum.Administrador,
                    CEP = entryCep.Text.Replace(".", "").Replace("-", ""),
                    Rua = entryRua.Text,
                    Numero = int.TryParse(entryNumero.Text, out var numero) ? numero : 0,
                    UF = entryUF.Text,
                    Cidade = entryCidade.Text,
                    StatusEnum = StatusEnum.Aprovado,
                    PostoId = null
                },
                Conta = new UsuarioContaRequest
                {
                    Login = RadioEmail.IsChecked ? entryEmail.Text : entryCPFCNPJ.Text.Replace(".", "").Replace("-", "").Replace("/", ""),
                    Senha = entrySenha.Text
                }
            };
                await CreateUserAsync(usuarioRequest);
            popup.Close();
        }
        catch(Exception ex)
        {
            popup.Close();
        }
    }
    private async Task CreateUserAsync(UsuarioRequest usuarioRequest)
    {
        try
        {
            var apiService = new ApiService();
            var result = await apiService.PostAsync<UsuarioRequest, object>("usuario", usuarioRequest);

            await DisplayAlert("Sucesso", "Usuário criado com sucesso", "Ok");
            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
        }
    }

    private void ReturnLogin(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void OnEntryCompletedCep(object sender, EventArgs e)
    {
        if(entryCep.Text.Count() == 10)
        {
            try
            {
                var cep = entryCep.Text.Replace(".", "").Replace("-", "");
                ViaCEPService service = new ViaCEPService();
                Address address = await service.GetAddressByCEPAsync(cep);
                entryRua.Text = address.Street;
                entryCidade.Text = address.City;
                entryUF.Text = address.UF;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
                entryRua.Text = "";
                entryCidade.Text = "";
                entryUF.Text = "";
            }
        }
    }
    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        createUser.IsEnabled =
            !string.IsNullOrWhiteSpace(entryNome.Text) &&
            !string.IsNullOrWhiteSpace(entryEmail.Text) &&
            entryEmail.Text.Contains("@") &&
            entryEmail.Text.Contains(".com") &&
            !string.IsNullOrWhiteSpace(entryCPFCNPJ.Text) &&
            (entryCPFCNPJ.Text.Count() == 14 || entryCPFCNPJ.Text.Count() == 18) &&
            !string.IsNullOrWhiteSpace(entrySenha.Text) &&
            !string.IsNullOrWhiteSpace(entryConfirmaSenha.Text) &&
            !string.IsNullOrWhiteSpace(entryCep.Text) &&
            !string.IsNullOrWhiteSpace(entryRua.Text) &&
            !string.IsNullOrWhiteSpace(entryCidade.Text) &&
            !string.IsNullOrWhiteSpace(entryUF.Text) &&
            !string.IsNullOrWhiteSpace(entryNumero.Text);

        if (createUser.IsEnabled)
        {
            createUser.Opacity = 1;
        }
        else
        {
            createUser.Opacity = 0.2;
        }
    }
}