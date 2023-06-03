using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Requests;

namespace projeto_mobile_adm_app.Views.App.Liquido;

public partial class PopupLiquidoPage : Popup
{
    public LiquidoDto Liquido { get; set; }

    public PopupLiquidoPage(LiquidoDto liquido)
    {
        InitializeComponent();
        Liquido = liquido;
        if (Liquido != null)
        {
            EntryNome.Text = Liquido.Nome;
            EntryValor.Text = Liquido.ValorUnitario.ToString();
            LabelTitle.Text = "Editar Líquido";
        }
        else
        {
            LabelTitle.Text = "Criar Líquido";
        }
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

    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var liquidoRequest = new LiquidoRequest
        {
            Id = Liquido?.Id,
            Nome = EntryNome.Text,
            ValorUnitario = double.Parse(EntryValor.Text),
            IdAdministrador = 1
        };

        if (Liquido == null)
        {
            // Crie um novo item e adicione-o à lista
        }
        else
        {
            // Atualize o item existente
        }

        this.Close();
    }

}