using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Services;
using projeto_mobile_adm_app.Views.Account;

namespace projeto_mobile_adm_app;

public partial class App : Application
{
    public static event Action<string> PasswordResetLinkReceived;

    public App(LoginView loginView)
    {
        InitializeComponent();

        MainPage = new NavigationPage(loginView);
        NavigationService.Instance.Initialize(MainPage.Navigation);
    }

    public static void OnPasswordResetLinkReceived(string token)
    {
        PasswordResetLinkReceived?.Invoke(token);
        NavigationService.Instance.NavigateToPasswordResetPage(token);
    }
}


