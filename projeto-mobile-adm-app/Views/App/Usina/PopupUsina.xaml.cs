using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Dtos;

namespace projeto_mobile_adm_app.Views.App.Usina;

public partial class PopupUsina : Popup
{
    public UsinaDto Usina { get; set; }
    public PopupUsina(UsinaDto usina)
	{
		InitializeComponent();
        Usina = usina;
        labelNome.Text = Usina.Nome;
        labelEndereco.Text = Usina.Rua + ", " + Usina.Numero.ToString() + ", " + Usina.Cidade;
    }

    private void OnCloseButtonClicked(object sender, TappedEventArgs e)
    {
        // Feche o pop-up
        this.Close();
    }

    private void EditarUsina(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new PopupUsinaEditCreate(Usina);
            popupPage.Size = new Size(350, 650);
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
    }
}