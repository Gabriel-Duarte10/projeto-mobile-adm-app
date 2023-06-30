using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Requests;
using projeto_mobile_adm_app.Services;
using System.Text.Json;
using static projeto_mobile_adm_app.Services.ViaCEPService;

namespace projeto_mobile_adm_app.Views.App.Usina;

public partial class PopupUsinaEditCreate : Popup
{
    public UsinaDto Usina { get; set; }
    public int UsuarioId;
    public PopupUsinaEditCreate(UsinaDto usina)
	{
		InitializeComponent();
        Usina = usina;
        if (Usina != null)
        {
            LabelTitle.Text = "Editar Líquido";
            EntryNome.Text = Usina.Nome;
            EntryRua.Text = Usina.Rua;
            EntryCidade.Text = Usina.Cidade;
            EntryUF.Text = Usina.UF;
            EntryNumero.Text = Usina.Numero.ToString();
            EntryCep.Text = Usina.CEP;
        }
        else
        {
            LabelTitle.Text = "Criar Líquido";
        }
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        UsuarioId = JsonSerializer.Deserialize<AdministradorDto>(jsonString).Id;
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
    }

    private void OnCloseButtonClicked(object sender, TappedEventArgs e)
    {
        // Feche o pop-up
        this.Close();
    }

    private void OnCancelButtonClicked(object sender, EventArgs e)
    {
        // Feche o pop-up
        this.Close();
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        UsinaRequest request = new UsinaRequest
        {
            Id = Usina?.Id,
            Nome = EntryNome.Text,
            Rua = EntryRua.Text,
            Cidade = EntryCidade.Text,
            UF = EntryUF.Text,
            Numero = Int32.Parse(EntryNumero.Text), // Certifique-se de que EntryNumero.Text pode ser convertido em um int
            CEP = EntryCep.Text,
            IdAdministrador = UsuarioId           
        };
        if (Usina == null)
        {
            await CreateUsinaAsync(request);
        }
        else
        {
            await UpdateUsinaAsync(request);
        }
        MessagingCenter.Send(Application.Current, "UsinaLoadDataAsync");
        this.Close();
    }
    private async Task CreateUsinaAsync(UsinaRequest request)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.PostAsync<UsinaRequest, object>("usina", request);

            await Application.Current.MainPage.DisplayAlert("Sucesso", "Usina criada com sucesso", "Ok");
        }
        catch (Exception ex)
        {
        }
    }
    private async Task UpdateUsinaAsync(UsinaRequest request)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.PutAsync<UsinaRequest, object>("usina", request);

            await Application.Current.MainPage.DisplayAlert("Sucesso", "Usina Atualizada com sucesso", "Ok");
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
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
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
            !string.IsNullOrWhiteSpace(EntryRua.Text) &&
            !string.IsNullOrWhiteSpace(EntryCidade.Text) &&
            !string.IsNullOrWhiteSpace(EntryUF.Text) &&
            !string.IsNullOrWhiteSpace(EntryNumero.Text) &&
            !string.IsNullOrWhiteSpace(EntryCep.Text);

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