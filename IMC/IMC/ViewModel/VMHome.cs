using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IMC.Auxiliares;
using IMC.View;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace IMC.ViewModel
{
    public partial class VMHome : ObservableObject
    {
        private readonly SesionPacienteService _sesion;

        [ObservableProperty]
        private string nombreCompleto;

        public VMHome()
        {
            _sesion = App.Current.Services.GetRequiredService<SesionPacienteService>();
        }

        // Este método se llama automáticamente cuando se navega con parámetros
       
        public async Task InicializarAsync()
        {
            if (_sesion.PacienteActual != null)
            {
                NombreCompleto = $"{_sesion.PacienteActual.Nombres} {_sesion.PacienteActual.Apellidos}";
            }
            else
            {
                await Shell.Current.GoToAsync("///MainPage");
                await Application.Current.MainPage.DisplayAlert("Sesión inválida", "Por favor selecciona un paciente.", "OK");
            }
        }
        [RelayCommand]
        public async Task Calcular()
        {
            await Shell.Current.GoToAsync($"{nameof(Calculadora)}?refresh=true");
        }

        [RelayCommand]
        public async Task HistorialMedi()
        {
            await Shell.Current.GoToAsync($"{nameof(HistorialMediciones)}?refresh=true");
        }

        [RelayCommand]
        public async Task MiInformacion()
        {
            await Shell.Current.GoToAsync($"{nameof(MiInformacion)}?refresh=true");
        }
    }
}
