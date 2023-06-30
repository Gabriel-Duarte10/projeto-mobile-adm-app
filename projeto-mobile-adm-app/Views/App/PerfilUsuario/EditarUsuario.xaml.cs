using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Requests;
using projeto_mobile_adm_app.Services;
using System.Text.Json;
using static projeto_mobile_adm_app.Services.ViaCEPService;

namespace projeto_mobile_adm_app.Views.App;

public partial class EditarUsuario : ContentPage
{
    private readonly AdministradorDto _adm;
    public EditarUsuario()
	{
		InitializeComponent();
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        _adm = JsonSerializer.Deserialize<AdministradorDto>(jsonString);

        EntryCep.TextChanged += (sender, e) =>
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
        EntryNome.Text = _adm.Usuario.Nome;
        EntryCep.Text = _adm.Usuario.CEP;
        EntryCidade.Text = _adm.Usuario.Cidade;
        EntryEmail.Text = _adm.Usuario.Email;
        EntryRua.Text = _adm.Usuario.Rua;
        EntryNumero.Text = _adm.Usuario.Numero.ToString();
        EntryUF.Text = _adm.Usuario.UF;
    }

    private void ReturnPerfil(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void SalvarUser(object sender, EventArgs e)
    {
        var Dados = new UsuarioDadosRequest
        {
            Id = _adm.Usuario.Id,
            Nome = EntryNome.Text,
            Email = EntryEmail.Text,
            CPFouCNPJ = _adm.Usuario.CPFouCNPJ,
            PerfilEnum = PerfilEnum.Administrador,
            CEP = EntryCep.Text.Replace(".", "").Replace("-", ""),
            Rua = EntryRua.Text,
            Numero = int.TryParse(EntryNumero.Text, out var numero) ? numero : 0,
            UF = EntryUF.Text,
            Cidade = EntryCidade.Text,
            StatusEnum = StatusEnum.Aprovado,
            PostoId = null
        };
        await UpdateUserAsync(Dados);
        _adm.Usuario.Nome = EntryNome.Text;
        _adm.Usuario.Email = EntryEmail.Text;
        _adm.Usuario.CEP = EntryCep.Text;
        _adm.Usuario.Cidade = EntryCidade.Text;
        _adm.Usuario.Rua = EntryRua.Text;
        _adm.Usuario.Numero = int.TryParse(EntryNumero.Text, out var numero1) ? numero : 0;
        _adm.Usuario.UF = EntryUF.Text;
        string jsonString = JsonSerializer.Serialize(_adm);
        Preferences.Set("usuarioLogado", jsonString);
        await DisplayAlert("Sucesso", "Usuário atualizado com sucesso", "Ok");
        await Navigation.PopModalAsync();

    }
    private async Task UpdateUserAsync(UsuarioDadosRequest usuarioRequest)
    {
        try
        {
            var apiService = new ApiService();
            var result = await apiService.PutAsync<UsuarioDadosRequest, object>("usuario", usuarioRequest);
        }
        catch (Exception ex)
        {
        }
    }
    private async void OnEntryCompletedCep(object sender, EventArgs e)
    {
        if (EntryCep.Text.Count() == 10)
        {
            try
            {
                var cep = EntryCep.Text.Replace(".", "").Replace("-", "");
                ViaCEPService service = new ViaCEPService();
                Address address = await service.GetAddressByCEPAsync(cep);
                EntryRua.Text = address.Street;
                EntryCidade.Text = address.City;
                EntryUF.Text = address.UF;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
                EntryRua.Text = "";
                EntryCidade.Text = "";
                EntryUF.Text = "";
            }
        }
    }
    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        createUser.IsEnabled =
            !string.IsNullOrWhiteSpace(EntryNome.Text) &&
            !string.IsNullOrWhiteSpace(EntryEmail.Text) &&
            EntryEmail.Text.Contains("@") &&
            EntryEmail.Text.Contains(".com") &&
            !string.IsNullOrWhiteSpace(EntryCep.Text) &&
            !string.IsNullOrWhiteSpace(EntryRua.Text) &&
            !string.IsNullOrWhiteSpace(EntryCidade.Text) &&
            !string.IsNullOrWhiteSpace(EntryUF.Text) &&
            !string.IsNullOrWhiteSpace(EntryNumero.Text);

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