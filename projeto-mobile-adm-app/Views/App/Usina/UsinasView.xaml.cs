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
        _originalList = new ObservableCollection<UsinaDto>
        {
            new UsinaDto { Id = 1, Nome = "Usina 1", CEP = "01310-000", Rua = "Avenida Paulista", Numero = 610, UF = "SP", Cidade = "São Paulo" },
            new UsinaDto { Id = 2, Nome = "Usina 2", CEP = "22041-011", Rua = "Rua Santa Clara", Numero = 1, UF = "RJ", Cidade = "Rio de Janeiro" },
            new UsinaDto { Id = 3, Nome = "Usina 3", CEP = "31330-000", Rua = "Avenida Altamiro Avelino Soares", Numero = 1, UF = "MG", Cidade = "Belo Horizonte" },
            new UsinaDto { Id = 4, Nome = "Usina 4", CEP = "91501-970", Rua = "Avenida Bento Gonçalves", Numero = 9500, UF = "RS", Cidade = "Porto Alegre" },
            new UsinaDto { Id = 5, Nome = "Usina 5", CEP = "41820-020", Rua = "Avenida Tancredo Neves", Numero = 1, UF = "BA", Cidade = "Salvador" },
            new UsinaDto { Id = 6, Nome = "Usina 6", CEP = "60312-060", Rua = "Avenida Presidente Castelo Branco", Numero = 2001, UF = "CE", Cidade = "Fortaleza" }

        };
        List = new ObservableCollection<UsinaDto>(_originalList);

        InitializeComponent();

        FilterText = string.Empty;

        BindingContext = this;
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

    private async void RemoverUsina(object sender, EventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

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
