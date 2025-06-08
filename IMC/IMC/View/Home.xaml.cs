namespace IMC.View;

public partial class Home : ContentPage
{
    private readonly VMHome _vmHome;

    public Home()
    {
        InitializeComponent(); // Siempre primero
        _vmHome = App.Current.Services.GetRequiredService<VMHome>();
        BindingContext = _vmHome;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vmHome.InicializarAsync();
    }
}
