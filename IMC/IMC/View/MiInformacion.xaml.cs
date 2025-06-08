namespace IMC.View;

public partial class MiInformacion : ContentPage
{
	 private readonly VMMiInformacion _viewModel;
    public MiInformacion()
	{
		_viewModel = App.Current.Services.GetRequiredService<VMMiInformacion>();
		BindingContext = _viewModel; // Set the BindingContext to the ViewModel
        InitializeComponent();
	}
}