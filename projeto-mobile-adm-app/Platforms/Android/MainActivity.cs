using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using Android.Content;
using Android.Net; // Adicione esta linha
using projeto_mobile_adm_app.Views.Account;
using AndroidUri = Android.Net.Uri;
using projeto_mobile_adm_app.Services;
using projeto_mobile_adm_app.Dtos;

namespace projeto_mobile_adm_app.Platforms.Android;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]

// Para URLs HTTP
[IntentFilter(new[] { Intent.ActionView },
     Categories = new[]
     {
         Intent.ActionView,
         Intent.CategoryDefault,
         Intent.CategoryBrowsable
     },
     DataScheme = "http", DataHost = "projetomobile-f277d.firebaseapp.com", DataPathPrefix = "/adm/redefinir-senha"
     )
 ]

// Para URLs HTTPS
[IntentFilter(new[] { Intent.ActionView },
     Categories = new[]
     {
         Intent.ActionView,
         Intent.CategoryDefault,
         Intent.CategoryBrowsable
     },
     DataScheme = "https", DataHost = "projetomobile-f277d.firebaseapp.com", DataPathPrefix = "/adm/redefinir-senha"
     )
 ]

public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);
        Delegate.SetLocalNightMode(AppCompatDelegate.ModeNightNo);

        if (Intent.Action == Intent.ActionView)
        {
            AndroidUri uri = Intent.Data;

            if (uri != null && uri.Path.StartsWith("/adm/redefinir-senha"))
            {
                string token = uri.GetQueryParameter("token");
                if (!string.IsNullOrEmpty(token))
                {
                    App.OnPasswordResetLinkReceived(token);
                }
            }
        }
    }
}

