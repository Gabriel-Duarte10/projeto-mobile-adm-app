using projeto_mobile_adm_app.Views.Account;

namespace projeto_mobile_adm_app;

public partial class App : Application
{
	public App(LoginView loginView)
	{
		InitializeComponent();

        MainPage = new NavigationPage(loginView);
    }
}
