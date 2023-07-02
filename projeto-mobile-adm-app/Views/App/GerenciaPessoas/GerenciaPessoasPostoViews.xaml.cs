using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Requests;
using projeto_mobile_adm_app.Services;
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
        _originalListPosto = new ObservableCollection<PostoDto>();
        
        //ListPosto = new ObservableCollection<PostoDto>(_originalListPosto);
        selectStatus.SelectedIndex = 0;
        ClienteCollectionView.IsVisible = false;
        LoadDataAsync();
        BindingContext = this;
    }
    private async Task LoadDataAsync()
    {
        String collection = null; 
        try
        {
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;
            if(ListPosto != null)
            {
                if(PostoCollectionView.IsVisible == true)
                {
                    PostoCollectionView.IsVisible = false;
                    collection = "posto";
                }
                if (ClienteCollectionView.IsVisible == true)
                {
                    collection = "cliente";
                    ClienteCollectionView.IsVisible = false;
                };
            }

            var apiService = new ApiService();
            // Exemplo de GET
            var postoReq = await apiService.GetAsync<List<PostoDto>>("posto");

            _originalListPosto = new ObservableCollection<PostoDto>(postoReq);

            ListPosto = new ObservableCollection<PostoDto>(_originalListPosto);

            if(ListPosto.Count == 0)
            {
                LabelNaoExistePosto.IsVisible = true;
            }

            var clienteReq = await apiService.GetAsync<List<ClienteDto>>("cliente");

            _originalListCliente = new ObservableCollection<ClienteDto>(clienteReq);

            ListCliente = new ObservableCollection<ClienteDto>(_originalListCliente);

        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
          
            if(collection != null)
            {
                if(collection == "posto")
                {
                    PostoCollectionView.IsVisible = true;
                }
                if (collection == "cliente")
                {
                    ClienteCollectionView.IsVisible = true;
                }
                FiltroStatus(selectStatus, EventArgs.Empty);
            }
        }
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
        if (ListPosto.Count == 0)
        {
            LabelNaoExistePosto.IsVisible = true;
            LabelNaoExisteCliente.IsVisible = false;
        }
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
        if(ListCliente.Count == 0)
        {
            LabelNaoExisteCliente.IsVisible = true;
            LabelNaoExistePosto.IsVisible = false;
        }
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
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Chama a função para carregar os dados
        LoadDataAsync();
    }
  
    private void AprovarPosto(object sender, EventArgs e)
    {
        var item = (PostoDto)((TappedEventArgs)e).Parameter;
        var postoRequest = new PostoStatusRequest
        {
            PostoId = item.Id,
            Status = StatusEnum.Aprovado
        };
        UpdatePostoAsync(postoRequest);
    }
    private void NegarPosto(object sender, EventArgs e)
    {
        var item = (PostoDto)((TappedEventArgs)e).Parameter;
        var postoRequest = new PostoStatusRequest
        {
            PostoId = item.Id,
            Status = StatusEnum.Reprovado
        };

        UpdatePostoAsync(postoRequest);
    }
    private void BloquearPosto(object sender, EventArgs e)
    {
        var item = (PostoDto)((TappedEventArgs)e).Parameter;
        var postoRequest = new PostoStatusRequest
        {
            PostoId = item.Id,
            Status = StatusEnum.Bloqueado
        };
        UpdatePostoAsync(postoRequest);
    }
    private void DesbloquearPosto(object sender, EventArgs e)
    {
        var item = (PostoDto)((TappedEventArgs)e).Parameter;
        var postoRequest = new PostoStatusRequest
        {
            PostoId = item.Id,
            Status = StatusEnum.Aprovado
        };
        UpdatePostoAsync(postoRequest);
    }
    private async Task UpdatePostoAsync(PostoStatusRequest PostoRequest)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.PutAsync<PostoStatusRequest, object>("posto/posto-status", PostoRequest);

            LoadDataAsync();
            Application.Current.MainPage.DisplayAlert("Sucesso", "Posto Atualizado com sucesso", "Ok");
        }
        catch (Exception ex)
        {
        }
    }

    private void BloquearCliente(object sender, EventArgs e)
    {
        var item = (ClienteDto)((TappedEventArgs)e).Parameter;
        var clienteRequest = new ClienteStatusRequest
        {
            ClienteId = item.Id,
            Status = StatusEnum.Bloqueado
        };
        UpdateClienteAsync(clienteRequest);
    }

    private void DesbloquearCliente(object sender, EventArgs e)
    {
        var item = (ClienteDto)((TappedEventArgs)e).Parameter;
        var clienteRequest = new ClienteStatusRequest
        {
            ClienteId = item.Id,
            Status = StatusEnum.Aprovado
        };
        UpdateClienteAsync(clienteRequest);
    }
    private async Task UpdateClienteAsync(ClienteStatusRequest ClienteRequest)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.PutAsync<ClienteStatusRequest, object>("cliente/alterar-status", ClienteRequest);

            Application.Current.MainPage.DisplayAlert("Sucesso", "Cliente Atualizado com sucesso", "Ok");
            LoadDataAsync();
        }
        catch (Exception ex)
        {
        }
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