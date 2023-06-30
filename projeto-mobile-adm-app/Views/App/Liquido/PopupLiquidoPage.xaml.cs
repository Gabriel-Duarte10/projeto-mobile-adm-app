using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Requests;
using projeto_mobile_adm_app.Services;
using System.Text.Json;

namespace projeto_mobile_adm_app.Views.App.Liquido;

public partial class PopupLiquidoPage : Popup
{
    public LiquidoDto Liquido { get; set; }
    public int UsuarioId;

    public PopupLiquidoPage(LiquidoDto liquido)
    {
        InitializeComponent();
        Liquido = liquido;
        if (Liquido != null)
        {
            EntryNome.Text = Liquido.Nome;
            EntryValor.Text = Liquido.ValorUnitario.ToString();
            LabelTitle.Text = "Editar L�quido";
        }
        else
        {
            LabelTitle.Text = "Criar L�quido";
        }
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        UsuarioId = JsonSerializer.Deserialize<AdministradorDto>(jsonString).Id;
    }
    private void OnCloseButtonClicked(object sender, EventArgs e)
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
        if(EntryNome.Text == "")
        {
            await Application.Current.MainPage.DisplayAlert("Erro", "Nome n�o pode ser vazio", "Ok");
            return;
        }
        if (EntryValor.Text == "")
        {
            await Application.Current.MainPage.DisplayAlert("Erro", "Valor n�o pode ser vazio", "Ok");
            return;
        }
        var liquidoRequest = new LiquidoRequest
        {
            Id = Liquido?.Id,
            Nome = EntryNome.Text,
            ValorUnitario = double.Parse(EntryValor.Text),
            IdAdministrador = UsuarioId
        };

        if (Liquido == null)
        {
            await CreateLiquidoAsync(liquidoRequest);
        }
        else
        {
            await UpdateLiquidoAsync(liquidoRequest);
        }

        MessagingCenter.Send(Application.Current, "ChamarLoadDataAsync");



        this.Close();
    }
    private async Task CreateLiquidoAsync(LiquidoRequest liquidoRequest)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.PostAsync<LiquidoRequest, object>("liquido", liquidoRequest);

            await Application.Current.MainPage.DisplayAlert("Sucesso", "Liquido criado com sucesso", "Ok");
        }
        catch (Exception ex)
        {
        }
    }
    private async Task UpdateLiquidoAsync(LiquidoRequest liquidoRequest)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.PutAsync<LiquidoRequest, object>("liquido", liquidoRequest);

            await Application.Current.MainPage.DisplayAlert("Sucesso", "Liquido Atualizado com sucesso", "Ok");
        }
        catch (Exception ex)
        {
        }
    }

}