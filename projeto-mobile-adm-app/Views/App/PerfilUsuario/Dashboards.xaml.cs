using projeto_mobile_adm_app.Dtos;
using projeto_mobile_adm_app.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace projeto_mobile_adm_app.Views.App.PerfilUsuario;

public partial class Dashboards : ContentPage, INotifyPropertyChanged
{
    private ObservableCollection<DashboardLiquidoDto> _listDashLiquido;
    public ObservableCollection<DashboardLiquidoDto> ListDashLiquido
    {
        get { return _listDashLiquido; }
        set
        {
            if (_listDashLiquido != value)
            {
                _listDashLiquido = value;
                OnPropertyChanged();
            }
        }
    }
    private ObservableCollection<DashboardClienteDto> _listDashCliente;
    public ObservableCollection<DashboardClienteDto> ListDashCliente
    {
        get { return _listDashCliente; }
        set
        {
            if (_listDashCliente != value)
            {
                _listDashCliente = value;
                OnPropertyChanged();
            }
        }
    }
    private ObservableCollection<DashboardPostoDto> _listDashPosto;
    public ObservableCollection<DashboardPostoDto> ListDashPosto
    {
        get { return _listDashPosto; }
        set
        {
            if (_listDashPosto != value)
            {
                _listDashPosto = value;
                OnPropertyChanged();
            }
        }
    }
    private ObservableCollection<DashboardUsinaDto> _listDashUsina;
    public ObservableCollection<DashboardUsinaDto> ListDashUsina
    {
        get { return _listDashUsina; }
        set
        {
            if (_listDashUsina != value)
            {
                _listDashUsina = value;
                OnPropertyChanged();
            }
        }
    }
    public int Active = 0;
    public Dashboards()
    {
        InitializeComponent();

        selectStatus.SelectedIndex = 0;
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
            LiquidoCollectionView.IsVisible = false;
            ClienteCollectionView.IsVisible = false;
            PostoCollectionView.IsVisible = false;
            UsinaCollectionView.IsVisible = false;
            var apiService = new ApiService();
            
            switch (Active)
            {
                case 0:
                    var liquidoReq = await apiService.GetAsync<List<DashboardLiquidoDto>>("dashboard/liquido-adm");
                    ListDashLiquido = new ObservableCollection<DashboardLiquidoDto>(liquidoReq);
                    collection = "liquido";
                    break;
                case 1:
                    var clienteReq = await apiService.GetAsync<List<DashboardClienteDto>>("dashboard/cliente-adm");
                    ListDashCliente = new ObservableCollection<DashboardClienteDto>(clienteReq);
                    collection = "cliente";
                    break;
                case 2:
                    var postoReq = await apiService.GetAsync<List<DashboardPostoDto>>("dashboard/posto-adm");
                    ListDashPosto = new ObservableCollection<DashboardPostoDto>(postoReq);
                    collection = "posto";
                    break;
                case 3:
                    var usinaReq = await apiService.GetAsync<List<DashboardUsinaDto>>("dashboard/usina-adm");
                    ListDashUsina = new ObservableCollection<DashboardUsinaDto>(usinaReq);
                    collection = "usina";
                    break;
            }

            if (ListDashLiquido.Count == 0)
            {
                LabelNaoExisteTransacoes.IsVisible = true;
            }
            if(ListDashUsina.Count == 0)
            {
                LabelNaoExisteTransacoesUsinas.IsVisible = true;
            }

        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;

            if (collection != null)
            {
                if (collection == "posto")
                {
                    PostoCollectionView.IsVisible = true;
                }
                if (collection == "cliente")
                {
                    ClienteCollectionView.IsVisible = true;
                }
                if (collection == "usina")
                {
                    UsinaCollectionView.IsVisible = true;
                }
                if (collection == "liquido")
                {
                    LiquidoCollectionView.IsVisible = true;
                }
            }
        }
    }

    void FiltroStatus(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        Active = selectedIndex;

        LoadDataAsync();

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
}