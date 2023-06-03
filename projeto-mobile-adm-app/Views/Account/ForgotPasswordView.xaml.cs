using projeto_mobile_adm_app.Libraries;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace projeto_mobile_adm_app.Views.Account;

public partial class ForgotPasswordView : ContentPage, INotifyPropertyChanged
{

    private int _active = 0;
    public int Active
    {
        get => _active;
        set
        {
            if (_active != value)
            {
                _active = value;
                OnPropertyChanged(nameof(Active));
            }
        }
    }
    public ForgotPasswordView()
	{
		InitializeComponent();
        BindingContext = this;
    }

    private void ReturnLogin(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void SendEmail(object sender, EventArgs e)
    {
        if(Active == 1)
        {
            Active = 0;
        }
        else
        {
            Active = 1;
            var formattedString = new FormattedString();
            var span1 = new Span { Text = "Uma mensagem foi enviada para o email: \n", TextColor = Color.FromHex("000000") };
            var span2 = new Span { Text = EntryEmail.Text, TextColor = Color.FromHex("0B6EFE") };

            formattedString.Spans.Add(span1);
            formattedString.Spans.Add(span2);
            LabelSuccess.FormattedText = formattedString;
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}