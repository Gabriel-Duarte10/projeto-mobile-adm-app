using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Views.App.GerenciaPessoas;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace projeto_mobile_adm_app.Views.App;

public partial class GerenciaPessoasPostoViews : ContentPage, INotifyPropertyChanged
{
    private ObservableCollection<PostoDto> _listPosto;
    private ObservableCollection<PostoDto> _originalListPosto;

    public ObservableCollection<PostoDto> ListPosto
    {
        get { return _listPosto; }
        set
        {
            if (_listPosto != value)
            {
                _listPosto = value;
                OnPropertyChanged();
            }
        }
    }
    private ObservableCollection<ClienteDto> _listCliente;
    private ObservableCollection<ClienteDto> _originalListCliente;

    public ObservableCollection<ClienteDto> ListCliente
    {
        get { return _listCliente; }
        set
        {
            if (_listCliente != value)
            {
                _listCliente = value;
                OnPropertyChanged();
            }
        }
    }
    private string _filterText;
    private CancellationTokenSource _cts = new CancellationTokenSource();
    public string FilterText
    {
        get { return _filterText; }
        set
        {
            if (_filterText != value)
            {
                _filterText = value;
                OnPropertyChanged();

                _cts.Cancel(); // cancel any previous search
                _cts = new CancellationTokenSource();

                Task.Delay(500, _cts.Token) // delay for half a second
                    .ContinueWith(
                        t => PesquisaFiltroList(),
                        CancellationToken.None,
                        TaskContinuationOptions.OnlyOnRanToCompletion,
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
    public GerenciaPessoasPostoViews()
	{
		InitializeComponent();
        _originalListPosto = new ObservableCollection<PostoDto>
        {
            // Pendente
            new PostoDto
            {
                Id = 1,
                Nome = "Posto 1",
                CEP = "12345-678",
                Rua = "Avenida 232323",
                Numero = 2323,
                UF = "SP",
                Cidade = "São Paulo",
                DonoPosto = new DonoPostoDto
                {
                    Id = 1,
                    Usuario = new UsuarioDto
                    {
                        Id = 1,
                        Nome = "Dono 1",
                        CPFouCNPJ = "123.456.789-00",
                        Email = "dono1@email.com",
                        PerfilEnum = PerfilEnum.DonoPosto,
                        CEP = "12345-678",
                        Rua = "Avenida Paulista",
                        Numero = 100,
                        UF = "SP",
                        Cidade = "São Paulo",
                        StatusEnum = StatusEnum.Pendente,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                },
                LiquidosAceitos = new List<PostoAceitaLiquidoDto>
                {
                    new PostoAceitaLiquidoDto
                    {
                        Liquido = new LiquidoDto
                        {
                            Id = 1,
                            Nome = "Líquido 1",
                            ValorUnitario = 10.0
                        },
                        CapacidadeTotal = 100,
                        CapacidadeOcupada = 50
                    }
                }
            },
            // Aprovado
            new PostoDto
            {
                Id = 2,
                Nome = "Posto 2",
                CEP = "23456-789",
                Rua = "Rua Santa Clara",
                Numero = 200,
                UF = "RJ",
                Cidade = "Rio de Janeiro",
                DonoPosto = new DonoPostoDto
                {
                    Id = 2,
                    Usuario = new UsuarioDto
                    {
                        Id = 2,
                        Nome = "Dono 2",
                        CPFouCNPJ = "234.567.890-11",
                        Email = "dono2@email.com",
                        PerfilEnum = PerfilEnum.DonoPosto,
                        CEP = "23456-789",
                        Rua = "Rua Santa Clara",
                        Numero = 200,
                        UF = "RJ",
                        Cidade = "Rio de Janeiro",
                        StatusEnum = StatusEnum.Aprovado,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                },
                LiquidosAceitos = new List<PostoAceitaLiquidoDto>
                {
                    new PostoAceitaLiquidoDto
                    {
                        Liquido = new LiquidoDto
                        {
                            Id = 2,
                            Nome = "Líquido 2",
                            ValorUnitario = 20.0
                        },
                        CapacidadeTotal = 200,
                        CapacidadeOcupada = 100
                    }
                }
            },
            // Reprovado
            new PostoDto
            {
                Id = 3,
                Nome = "Posto 3",
                CEP = "34567-890",
                Rua = "Avenida Altamiro Avelino Soares",
                Numero = 300,
                UF = "MG",
                Cidade = "Belo Horizonte",
                DonoPosto = new DonoPostoDto
                {
                    Id = 3,
                    Usuario = new UsuarioDto
                    {
                        Id = 3,
                        Nome = "Dono 3",
                        CPFouCNPJ = "345.678.901-22",
                        Email = "dono3@email.com",
                        PerfilEnum = PerfilEnum.DonoPosto,
                        CEP = "34567-890",
                        Rua = "Avenida Altamiro Avelino Soares",
                        Numero = 300,
                        UF = "MG",
                        Cidade = "Belo Horizonte",
                        StatusEnum = StatusEnum.Reprovado,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                },
                LiquidosAceitos = new List<PostoAceitaLiquidoDto>
                {
                    new PostoAceitaLiquidoDto
                    {
                        Liquido = new LiquidoDto
                        {
                            Id = 3,
                            Nome = "Líquido 3",
                            ValorUnitario = 30.0
                        },
                        CapacidadeTotal = 300,
                        CapacidadeOcupada = 150
                    }
                }
            },
            // Bloqueado
            new PostoDto
            {
                Id = 4,
                Nome = "Posto 4",
                CEP = "45678-901",
                Rua = "Avenida Bento Gonçalves",
                Numero = 400,
                UF = "RS",
                Cidade = "Porto Alegre",
                DonoPosto = new DonoPostoDto
                {
                    Id = 4,
                    Usuario = new UsuarioDto
                    {
                        Id = 4,
                        Nome = "Dono 4",
                        CPFouCNPJ = "456.789.012-33",
                        Email = "dono4@email.com",
                        PerfilEnum = PerfilEnum.DonoPosto,
                        CEP = "45678-901",
                        Rua = "Avenida Bento Gonçalves",
                        Numero = 400,
                        UF = "RS",
                        Cidade = "Porto Alegre",
                        StatusEnum = StatusEnum.Bloqueado,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
        },
                LiquidosAceitos = new List<PostoAceitaLiquidoDto>
                {
                    new PostoAceitaLiquidoDto
                    {
                        Liquido = new LiquidoDto
                        {
                            Id = 4,
                            Nome = "Líquido 4",
                            ValorUnitario = 40.0
                        },
                        CapacidadeTotal = 400,
                        CapacidadeOcupada = 200
                    }
                }
            }
        };
        _originalListCliente = new ObservableCollection<ClienteDto>
        {
            // Pendente
            new ClienteDto
            {
                Id = 1,
                Saldo = 1000.00,
                Usuario = new UsuarioDto
                {
                    Id = 1,
                    Nome = "Usuario1",
                    CPFouCNPJ = "12345678910",
                    Email = "usuario1@email.com",
                    PerfilEnum = PerfilEnum.Cliente,
                    CEP = "12345678",
                    Rua = "Rua A",
                    Numero = 1,
                    UF = "UF",
                    Cidade = "Cidade",
                    StatusEnum = StatusEnum.Bloqueado,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            },
            // Aprovado
            new ClienteDto
            {
                Id = 2,
                Saldo = 2000.00,
                Usuario = new UsuarioDto
                {
                    Id = 2,
                    Nome = "Usuario2",
                    CPFouCNPJ = "23456789101",
                    Email = "usuario2@email.com",
                    PerfilEnum = PerfilEnum.Cliente,
                    CEP = "23456789",
                    Rua = "Rua B",
                    Numero = 2,
                    UF = "UF",
                    Cidade = "Cidade",
                    StatusEnum = StatusEnum.Aprovado,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            },
            // Reprovado
            new ClienteDto
            {
                Id = 3,
                Saldo = 3000.00,
                Usuario = new UsuarioDto
                {
                    Id = 3,
                    Nome = "Usuario3",
                    CPFouCNPJ = "34567891012",
                    Email = "usuario3@email.com",
                    PerfilEnum = PerfilEnum.Cliente,
                    CEP = "34567890",
                    Rua = "Rua C",
                    Numero = 3,
                    UF = "UF",
                    Cidade = "Cidade",
                    StatusEnum = StatusEnum.Aprovado,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            },
            // Bloqueado
            new ClienteDto
            {
                Id = 4,
                Saldo = 4000.00,
                Usuario = new UsuarioDto
                {
                    Id = 4,
                    Nome = "Usuario4",
                    CPFouCNPJ = "45678910123",
                    Email = "usuario4@email.com",
                    PerfilEnum = PerfilEnum.Cliente,
                    CEP = "45678901",
                    Rua = "Rua D",
                    Numero = 4,
                    UF = "UF",
                    Cidade = "Cidade",
                    StatusEnum = StatusEnum.Bloqueado,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            }
        };

        ListPosto = new ObservableCollection<PostoDto>(_originalListPosto);
        ListCliente = new ObservableCollection<ClienteDto>(_originalListCliente);
        selectStatus.SelectedIndex = 0;
        ClienteCollectionView.IsVisible = false;
        BindingContext = this;
    }

    private void PostoTab(object sender, EventArgs e)
    {
        PostoTabButton.BackgroundColor = Color.FromArgb("#0B6EFE");
        PostoTabButton.TextColor = Color.FromArgb("#FFFFFF");
        ClienteTabButton.BackgroundColor = Color.FromArgb("#FFFFFF");
        ClienteTabButton.TextColor = Color.FromArgb("#686868");
        selectStatus.SelectedIndex = 0;
        FilterEntry.Text = "";
        ClienteCollectionView.IsVisible = false;
        PostoCollectionView.IsVisible = true;
    }

    private void ClienteTab(object sender, EventArgs e)
    {
        ClienteTabButton.BackgroundColor = Color.FromArgb("#0B6EFE");
        ClienteTabButton.TextColor = Color.FromArgb("#FFFFFF");
        PostoTabButton.BackgroundColor = Color.FromArgb("#FFFFFF");
        PostoTabButton.TextColor = Color.FromArgb("#686868");
        selectStatus.SelectedIndex = 0;
        FilterEntry.Text = "";
        ClienteCollectionView.IsVisible = true;
        PostoCollectionView.IsVisible = false;
    }
    void FiltroStatus(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if(PostoCollectionView.IsVisible == true)
        {
            if (selectedIndex == 0)
            {
                ListPosto = new ObservableCollection<PostoDto>(_originalListPosto);
            }
            if (selectedIndex == 1)
            {
                ListPosto = new ObservableCollection<PostoDto>(_originalListPosto
                        .Where(x => x.DonoPosto.Usuario.StatusEnum == StatusEnum.Pendente).ToList());
            }
            if (selectedIndex == 2)
            {
                ListPosto = new ObservableCollection<PostoDto>(_originalListPosto
                        .Where(x => x.DonoPosto.Usuario.StatusEnum == StatusEnum.Aprovado).ToList());
            }
            if (selectedIndex == 3)
            {
                ListPosto = new ObservableCollection<PostoDto>(_originalListPosto
                        .Where(x => x.DonoPosto.Usuario.StatusEnum == StatusEnum.Reprovado).ToList());
            }
            if (selectedIndex == 4)
            {
                ListPosto = new ObservableCollection<PostoDto>(_originalListPosto
                        .Where(x => x.DonoPosto.Usuario.StatusEnum == StatusEnum.Bloqueado).ToList());
            }
            OnPropertyChanged(nameof(ListPosto));
        }
        else
        {
            if (selectedIndex == 0)
            {
                ListCliente = new ObservableCollection<ClienteDto>(_originalListCliente);
            }
            if (selectedIndex == 1)
            {
                ListCliente = new ObservableCollection<ClienteDto>(_originalListCliente
                        .Where(x => x.Usuario.StatusEnum == StatusEnum.Pendente).ToList());
            }
            if (selectedIndex == 2)
            {
                ListCliente = new ObservableCollection<ClienteDto>(_originalListCliente
                        .Where(x => x.Usuario.StatusEnum == StatusEnum.Aprovado).ToList());
            }
            if (selectedIndex == 3)
            {
                ListCliente = new ObservableCollection<ClienteDto>(_originalListCliente
                        .Where(x => x.Usuario.StatusEnum == StatusEnum.Reprovado).ToList());
            }
            if (selectedIndex == 4)
            {
                ListCliente = new ObservableCollection<ClienteDto>(_originalListCliente
                        .Where(x => x.Usuario.StatusEnum == StatusEnum.Bloqueado).ToList());
            }
            OnPropertyChanged(nameof(ListCliente));
        }

        
        
    }
    private void PesquisaFiltroList()
    {
        if (string.IsNullOrWhiteSpace(FilterText))
        {
            // Se o texto do filtro estiver vazio, apenas copie a lista original.
            if (PostoCollectionView.IsVisible == true)
            {
                ListPosto = new ObservableCollection<PostoDto>(_originalListPosto);
            }
            else
            {
                ListCliente = new ObservableCollection<ClienteDto>(_originalListCliente);
            }
        }
        else
        {
            if (PostoCollectionView.IsVisible == true)
            {
                // Se o texto do filtro não estiver vazio, aplique o filtro e crie uma nova lista.
                ListPosto = new ObservableCollection<PostoDto>(_originalListPosto
                    .Where(l => l.Nome.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList());
            }else
            {
                ListCliente = new ObservableCollection<ClienteDto>(_originalListCliente
                    .Where(l => l.Usuario.Nome.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList());
            }
        }
        OnPropertyChanged(nameof(ListPosto));
        OnPropertyChanged(nameof(ListCliente));
    }
    // Implementação de INotifyPropertyChanged.
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void AprovarPosto(object sender, EventArgs e)
    {

    }
    private void NegarPosto(object sender, EventArgs e)
    {

    }
    private void BloquearPosto(object sender, EventArgs e)
    {

    }
    private void DesbloquearPosto(object sender, EventArgs e)
    {

    }
    private void AprovarCliente(object sender, EventArgs e)
    {

    }
    private void NegarCliente(object sender, EventArgs e)
    {

    }
    private void BloquearCliente(object sender, EventArgs e)
    {

    }
    private void DesbloquearCliente(object sender, EventArgs e)
    {

    }

    private void AbrirPosto(object sender, TappedEventArgs e)
    {
        var item = (PostoDto)((TappedEventArgs)e).Parameter;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new PopupDadosPosto(item);
            popupPage.Size = new Size(350, 300);
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
    }

    private void AbrirCliente(object sender, TappedEventArgs e)
    {
        var item = (ClienteDto)((TappedEventArgs)e).Parameter;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new PopupDadosCliente(item);
            popupPage.Size = new Size(350, 270);
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
    }
}