using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IMC.Model;
using IMC.Model.Repositories;
using IMC.View;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IMC.Auxiliares;

namespace IMC.ViewModel
{
    public partial class VMMainPage : ObservableObject
    {
        private readonly IPaciente _pacienteRepository;

        public ObservableCollection<Paciente> Pacientes { get; set; } = new();

        public VMMainPage()
        {
            _pacienteRepository = App.Current.Services.GetRequiredService<IPaciente>();
        }

        [RelayCommand]
        public async Task RegistrarAsync()
        {
            await Shell.Current.GoToAsync(nameof(PageRegister));
        }

        [RelayCommand]
        public async Task CargarPacientesAsync()
        {
            try
            {
                Pacientes.Clear();
                var lista = await _pacienteRepository.GetAll();

                foreach (var paciente in lista)
                    Pacientes.Add(paciente);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar pacientes: {ex.Message}");
            }
        }


        [RelayCommand]
        public async Task SeleccionarPacienteAsync(Paciente paciente)
        {
            if (paciente == null)
                return;

            var sesion = App.Current.Services.GetRequiredService<SesionPacienteService>();
            sesion.EstablecerPaciente(paciente);

            // Confirmación para depuración
            System.Diagnostics.Debug.WriteLine($"✅ Paciente seleccionado: {paciente.Nombres}");

            await Shell.Current.GoToAsync(nameof(Home));
        }

    }
}
