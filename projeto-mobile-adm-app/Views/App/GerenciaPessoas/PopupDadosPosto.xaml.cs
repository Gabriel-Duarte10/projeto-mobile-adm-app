using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Dtos;

namespace projeto_mobile_adm_app.Views.App.GerenciaPessoas;

public partial class PopupDadosPosto : Popup
{
	public PostoDto Posto { get; set; }
	public PopupDadosPosto(PostoDto posto)
	{
		InitializeComponent();
		Posto = posto;
		lblNomePosto.Text = posto.Nome;
		lblNomeDono.Text = posto.DonoPosto.Usuario.Nome;
		lblCnpj.Text =  "CNPJ: " + posto.DonoPosto.Usuario.CPFouCNPJ;
		lblEnderecoDono.Text = posto.DonoPosto.Usuario.Rua + ", " +
 				posto.DonoPosto.Usuario.Cidade + ", " +
				posto.DonoPosto.Usuario.UF;
		lblEnderecoPosto.Text = posto.Rua + ", " +
 				posto.Cidade + ", " + 
				posto.UF;
		lblStatus.Text = posto.DonoPosto.Usuario.StatusEnum.ToString();

    }
}