using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Views.App.Liquido;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using projeto_mobile_adm_app.Dtos;

namespace projeto_mobile_adm_app.Views.App;

public partial class LiquidosView : ContentPage, INotifyPropertyChanged
{
    private ObservableCollection<LiquidoDto> _list;
    private ObservableCollection<LiquidoDto> _originalList;
    public ObservableCollection<LiquidoDto> List
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
    public LiquidosView()
    {
        _originalList = new ObservableCollection<LiquidoDto>
        {
            new LiquidoDto { Id = 1, Nome = "Liquido 1", ValorUnitario = 16.99 },
            new LiquidoDto { Id = 2, Nome = "Liquido 2", ValorUnitario = 17.99 },
            new LiquidoDto { Id = 3, Nome = "Liquido 3", ValorUnitario = 18.99 },
            new LiquidoDto { Id = 4, Nome = "Liquido 4", ValorUnitario = 18.99 },
            new LiquidoDto { Id = 5, Nome = "Liquido 5", ValorUnitario = 18.99 },
            new LiquidoDto { Id = 6, Nome = "Liquido 6", ValorUnitario = 18.99 }
        };

        List = new ObservableCollection<LiquidoDto>(_originalList);

        InitializeComponent();

        FilterText = string.Empty;

        BindingContext = this;
    }
    private void FilterList()
    {
        if (string.IsNullOrWhiteSpace(FilterText))
        {
            // Se o texto do filtro estiver vazio, apenas copie a lista original.
            List = new ObservableCollection<LiquidoDto>(_originalList);
        }
        else
        {
            // Se o texto do filtro não estiver vazio, aplique o filtro e crie uma nova lista.
            List = new ObservableCollection<LiquidoDto>(_originalList.Where(l => l.Nome.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList());
        }
    }


    private void CriarLiquido(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new PopupLiquidoPage(null);
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
    }
    private async void RemoverLiquido(object sender, EventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        var item = (LiquidoDto)((TappedEventArgs)e).Parameter;

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
            CharacterSpacing = 0.3,
            Font = Microsoft.Maui.Font.SystemFontOfSize(14),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
        };


        string actionButtonText = "Desfazer";

        // A ação de desfazer agora adiciona o item de volta à sua posição original nas duas listas
        Action action = () =>
        {
            List.Insert(originalIndex, item);
            _originalList.Insert(originalIndex, item); // Adiciona de volta na lista original
        };

        TimeSpan duration = TimeSpan.FromSeconds(5);

        var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

        await snackbar.Show(cancellationTokenSource.Token);
    }
    private void EditarLiquido(object sender, TappedEventArgs e)
    {
        var item = (LiquidoDto)((TappedEventArgs)e).Parameter;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new PopupLiquidoPage(item);
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
    }

    // Implementação de INotifyPropertyChanged.
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}