using IMC.Auxiliares;

namespace IMC.View;

public partial class HistorialMediciones : ContentPage
{
    private readonly VMHistorialMediciones _vmHistorial;
    private readonly SesionPacienteService _sesion;

    public HistorialMediciones()
    {
        _vmHistorial = App.Current.Services.GetRequiredService<VMHistorialMediciones>();
        _sesion = App.Current.Services.GetRequiredService<SesionPacienteService>();
        BindingContext = _vmHistorial; 
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        int idPaciente = _sesion.PacienteActual?.ID ?? 0;

        if (idPaciente > 0)
            await _vmHistorial.CargarMedicionesPacienteAsync(idPaciente);
    }
}
