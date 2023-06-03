using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Maui.Views;
using projeto_mobile_adm_app.Views.App.Liquido;

namespace projeto_mobile_adm_app.Views.App;

public partial class LiquidosView : ContentPage, INotifyPropertyChanged
{
    private ObservableCollection<string> _list;
    public ObservableCollection<string> List
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
    public LiquidosView()
	{
        List = new ObservableCollection<string>
        {
            "Liquido 1",
            "Liquido 2",
            "Liquido 3",
            "Liquido 4",
            "Liquido 5",
            "Liquido 6"
        };

        InitializeComponent();

        // Defina o contexto de vinculação para esta página como ela mesma, para que possamos vincular à propriedade List.
        BindingContext = this;
    }

    private void CriarLiquido(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Application.Current.MainPage.DisplayAlert("Título", "RemoverLiquido", "OK");
        });
    }
    private void RemoverLiquido(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Application.Current.MainPage.DisplayAlert("Título", "RemoverLiquido", "OK");
        });
    }
    private void EditarLiquido(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new PopupLiquidoPage();
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