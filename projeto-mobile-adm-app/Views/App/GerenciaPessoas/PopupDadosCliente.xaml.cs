using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Dtos;

namespace projeto_mobile_adm_app.Views.App.GerenciaPessoas;

public partial class PopupDadosCliente : Popup
{
	public ClienteDto Cliente { get; set; }
	public PopupDadosCliente(ClienteDto cliente)
	{
		InitializeComponent();
		Cliente = cliente;
		lblCpf.Text = "CPF: " + cliente.Usuario.CPFouCNPJ;
		lblDataCadastro.Text = "Cliente cadastrado dia: " + cliente.Usuario.CreatedAt?.ToString("dd/MM/yyyy");
		lblEndereco.Text = "Endereço: " + cliente.Usuario.Rua + ", " +
                cliente.Usuario.Cidade + ", " +
                cliente.Usuario.UF;
		lblEmail.Text = "Email: " + cliente.Usuario.Email;
		lblStatus.Text = cliente.Usuario.StatusEnum.ToString();
		lblNomeCliente.Text = cliente.Usuario.Nome;
	}
}