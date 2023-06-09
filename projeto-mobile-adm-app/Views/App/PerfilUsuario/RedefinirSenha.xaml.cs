using projeto_mobile_adm_app.Dtos;

namespace projeto_mobile_adm_app.Views.App.PerfilUsuario;

public partial class RedefinirSenha : ContentPage
{
	public RedefinirSenha(AdministradorDto admin)
	{
		InitializeComponent();
        var formattedString = new FormattedString();
        var span1 = new Span { Text = "Uma mensagem foi enviada para o email: \n", TextColor = Color.FromHex("000000") };
        var span2 = new Span { Text = admin.Usuario.Email, TextColor = Color.FromHex("0B6EFE") };

        formattedString.Spans.Add(span1);
        formattedString.Spans.Add(span2);
        LabelSuccess.FormattedText = formattedString;

    }

    private void ReturnPerfil(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void SendEmail(object sender, TappedEventArgs e)
    {

    }
}