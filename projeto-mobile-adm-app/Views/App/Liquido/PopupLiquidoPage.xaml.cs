using CommunityToolkit.Maui.Views;

namespace projeto_mobile_adm_app.Views.App.Liquido;

public partial class PopupLiquidoPage : Popup
{
	public PopupLiquidoPage()
	{
		InitializeComponent();
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
        // Salve os dados e feche o pop-up
        this.Close();
    }
}