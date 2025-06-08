namespace IMC.View;

public partial class Calculadora : ContentPage
{
	private readonly VMCalculadora _vmCalculadora;
    public Calculadora()
	{
		_vmCalculadora = App.Current.Services.GetRequiredService<VMCalculadora>();
		BindingContext = _vmCalculadora; // Asignar el ViewModel a la vista
        InitializeComponent();
	}
}