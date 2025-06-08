namespace IMC.View;

public partial class Home : ContentPage
{
	private readonly VMHome _vmHome;
    public Home()
	{
		_vmHome = App.Current.Services.GetRequiredService<VMHome>();
		BindingContext = _vmHome; // Asignar el ViewModel a la vista
        InitializeComponent();
	}
}