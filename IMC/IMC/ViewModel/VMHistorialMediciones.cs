using CommunityToolkit.Mvvm.Input;
using IMC.Auxiliares;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMC.ViewModel
{   
    
    public partial class VMHistorialMediciones:ObservableObject
    {
        private readonly IMedicion _medicionService;
        public ObservableCollection<Medicion> Mediciones { get; set; } = new();
        public VMHistorialMediciones()
        {
            _medicionService = App.Current.Services.GetRequiredService<IMedicion>();
            
        }

        [RelayCommand]
        public async Task CargarMedicionesPacienteAsync(int IdPaciente)
        {
            try
            {
                Mediciones.Clear();
                var lista = await _medicionService.GetByPacienteId(IdPaciente);

                foreach (var paciente in lista)
                    Mediciones.Add(paciente);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar las Mediciones: {ex.Message}");
            }
        }
    }
}
