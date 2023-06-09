using CommunityToolkit.Maui.Views;

namespace projeto_mobile_adm_app.Views.App;

public partial class EditarUsuario : ContentPage
{
	public EditarUsuario()
	{
		InitializeComponent();
	}

    private void ReturnPerfil(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void SalvarUser(object sender, EventArgs e)
    {

    }
}