using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Services;
using projeto_mobile_adm_app.Views.Account;
using projeto_mobile_adm_app.Views.App;

namespace projeto_mobile_adm_app;

public partial class App : Application
{
    public static event Action<string> PasswordResetLinkReceived;

    public App(LoginView loginView)
    {
        InitializeComponent();
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        if(string.IsNullOrEmpty(jsonString))
        {
            MainPage = new NavigationPage(loginView);
        }
        else
        {
            MainPage = new NavigationPage(new AppView());
        }
        NavigationService.Instance.Initialize(MainPage.Navigation);
    }

    public static void OnPasswordResetLinkReceived(string token)
    {
        PasswordResetLinkReceived?.Invoke(token);
        NavigationService.Instance.NavigateToPasswordResetPage(token);
    }
}


