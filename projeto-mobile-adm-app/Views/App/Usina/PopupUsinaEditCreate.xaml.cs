using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Dtos;

namespace projeto_mobile_adm_app.Views.App.Usina;

public partial class PopupUsinaEditCreate : Popup
{
    public UsinaDto Usina { get; set; }
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

    private void OnSaveButtonClicked(object sender, EventArgs e)
    {

    }
}