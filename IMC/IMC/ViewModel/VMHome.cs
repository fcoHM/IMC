using CommunityToolkit.Mvvm.ComponentModel;
using IMC.Auxiliares;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace IMC.ViewModel
{
    public partial class VMHome : ObservableObject, IQueryAttributable
    {
        private readonly SesionPacienteService _sesion;

        [ObservableProperty]
        private string nombreCompleto;

        public VMHome()
        {
            _sesion = App.Current.Services.GetRequiredService<SesionPacienteService>();
        }

        // Este método se llama automáticamente cada vez que navegas con parámetros
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (_sesion.PacienteActual != null)
            {
                NombreCompleto = $"{_sesion.PacienteActual.Nombres} {_sesion.PacienteActual.Apellidos}";
            }
            else
            {
                NombreCompleto = "Paciente no identificado";
            }
        }
    }
}
