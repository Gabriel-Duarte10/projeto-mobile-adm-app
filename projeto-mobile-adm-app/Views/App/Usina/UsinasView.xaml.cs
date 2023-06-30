using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using projeto_mobile_adm_app.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using projeto_mobile_adm_app.Views.App.Usina;
using projeto_mobile_adm_app.Services;

namespace projeto_mobile_adm_app.Views.App;

public partial class UsinasView : ContentPage, INotifyPropertyChanged
{
    private ObservableCollection<UsinaDto> _list;
    private ObservableCollection<UsinaDto> _originalList;
    public ObservableCollection<UsinaDto> List
    {
        get { return _list; }
        set
        {
            if (_list != value)
            {
                _list = value;
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
                        t => FilterList(),
                        CancellationToken.None,
                        TaskContinuationOptions.OnlyOnRanToCompletion,
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
    public UsinasView()
    {

        InitializeComponent();

        LoadDataAsync();

        MessagingCenter.Subscribe<Application>(Application.Current, "UsinaLoadDataAsync", (sender) =>
        {
            LoadDataAsync();
        });

        BindingContext = this;
    }
    private async Task LoadDataAsync()
    {
        var apiService = new ApiService();
        // Exemplo de GET
        var usinaReq = await apiService.GetAsync<List<UsinaDto>>("usina");

        _originalList = new ObservableCollection<UsinaDto>(usinaReq);

        List = new ObservableCollection<UsinaDto>(_originalList);

        FilterText = string.Empty;
    }
    private void FilterList()
    {
        if (string.IsNullOrWhiteSpace(FilterText))
        {
            // Se o texto do filtro estiver vazio, apenas copie a lista original.
            List = new ObservableCollection<UsinaDto>(_originalList);
        }
        else
        {
            // Se o texto do filtro não estiver vazio, aplique o filtro e crie uma nova lista.
            List = new ObservableCollection<UsinaDto>(_originalList.Where(l => l.Nome.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList());
        }
    }
    private void CriarUsina(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new PopupUsinaEditCreate(null);
            popupPage.Size = new Size(350, 650);
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
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
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        MessagingCenter.Unsubscribe<Application>(Application.Current, "UsinaLoadDataAsync");
    }
    private async void RemoverUsina(object sender, EventArgs e)
    {
        var item = (UsinaDto)((TappedEventArgs)e).Parameter;

        // Guarda a posição original do item antes de removê-lo
        int originalIndex = List.IndexOf(item);

        string text = item.Nome + " excluido";
        List.Remove(item);
        _originalList.Remove(item); // Remove item da lista original

        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            ActionButtonTextColor = Color.FromRgba("#0B6EFE"),
            CornerRadius = new CornerRadius(5),
            CharacterSpacing = 0,
            Font = Microsoft.Maui.Font.SystemFontOfSize(18),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
        };

        string actionButtonText = "Desfazer";

        var tcs = new TaskCompletionSource<bool>();

        // A ação de desfazer agora adiciona o item de volta à sua posição original nas duas listas
        Action action = () =>
        {
            tcs.SetResult(true);
            List.Insert(originalIndex, item);
            _originalList.Insert(originalIndex, item); // Adiciona de volta na lista original
        };

        TimeSpan duration = TimeSpan.FromSeconds(5);

        var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

        snackbar.Show();

        await Task.WhenAny(tcs.Task, Task.Delay(duration));

        if (!tcs.Task.IsCompleted)
        {
            await DeleteUsinaAsync(item);
            await DisplayAlert("Sucesso", "Usina removida com sucesso", "Ok");
        }
    }
    private async Task DeleteUsinaAsync(UsinaDto item)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.DeleteAsync("usina/" + item.Id.ToString());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void AbrirUsina(object sender, TappedEventArgs e)
    {
        var item = (UsinaDto)((TappedEventArgs)e).Parameter;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new PopupUsina(item);
            popupPage.Size = new Size(350, 250);
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
    }
}
