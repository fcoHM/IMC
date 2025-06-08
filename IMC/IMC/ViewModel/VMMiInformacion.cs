using CommunityToolkit.Mvvm.Input;
using IMC.Auxiliares;
using IMC.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMC.ViewModel
{
    public partial class VMMiInformacion : ObservableValidator
    {
        private Paciente _paciente;
        private readonly SesionPacienteService _sesion;
        private readonly IPaciente _pacienteRepository;

        public ObservableCollection<string> Errores { get; set; } = new();

        // Campos de la vista
        private string nombres = string.Empty;
        [Required(ErrorMessage = "El campo Nombres es obligatorio.")]
        [System.ComponentModel.DataAnnotations.MaxLength(80, ErrorMessage = "El campo Nombres no puede exceder los 80 caracteres.")]
        public string Nombres
        {
            get => nombres;
            set => SetProperty(ref nombres, value, true);
        }

        private string apellidos = string.Empty;
        [Required(ErrorMessage = "El campo Apellidos es obligatorio.")]
        [System.ComponentModel.DataAnnotations.MaxLength(80, ErrorMessage = "El campo Apellidos no puede exceder los 80 caracteres.")]
        public string Apellidos
        {
            get => apellidos;
            set => SetProperty(ref apellidos, value, true);
        }

        private string sexoSeleccionado = string.Empty;
        [Required(ErrorMessage = "El campo Sexo es obligatorio.")]
        public string SexoSeleccionado
        {
            get => sexoSeleccionado;
            set => SetProperty(ref sexoSeleccionado, value, true);
        }

        private DateTime fechaNacimiento = DateTime.Today.AddYears(-20);
        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio.")]
        public DateTime FechaNacimiento
        {
            get => fechaNacimiento;
            set => SetProperty(ref fechaNacimiento, value, true);
        }

        public VMMiInformacion()
        {
            _sesion = App.Current.Services.GetRequiredService<SesionPacienteService>();
            _pacienteRepository = App.Current.Services.GetRequiredService<IPaciente>();
            _paciente = _sesion.PacienteActual;

            if (_paciente != null)
            {
                Nombres = _paciente.Nombres;
                Apellidos = _paciente.Apellidos;
                SexoSeleccionado = _paciente.Sexo == 0 ? "Masculino" : "Femenino";
                FechaNacimiento = _paciente.FechaNacimiento;
            }
        }

        [RelayCommand]
        public async Task ActualizarInformacionAsync()
        {
            Errores.Clear();
            ValidateAllProperties();

            if (HasErrors)
            {
                foreach (var prop in new[] { nameof(Nombres), nameof(Apellidos), nameof(SexoSeleccionado), nameof(FechaNacimiento) })
                {
                    var erroresPropiedad = GetErrors(prop).ToList();
                    foreach (var error in erroresPropiedad)
                        Errores.Add($"{prop}: {error.ErrorMessage}");
                }

                await Application.Current.MainPage.DisplayAlert("Errores de Validación", string.Join("\n", Errores), "OK");
                return;
            }

            if (_paciente == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No hay paciente activo en sesión.", "OK");
                return;
            }

            // Actualizar los datos del paciente
            _paciente.Nombres = Nombres;
            _paciente.Apellidos = Apellidos;
            _paciente.FechaNacimiento = FechaNacimiento;
            _paciente.Sexo = SexoSeleccionado == "Masculino" ? 0 : 1;

            try
            {
                int resultado = await _pacienteRepository.Update(_paciente);
                if (resultado > 0)
                {
                    _sesion.PacienteActual = _paciente;
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Información actualizada correctamente", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar la información", "OK");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al actualizar: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error inesperado", "OK");
            }
        }


        [RelayCommand]
        public async Task EliminarCuentaAsync()
        {
            if (_paciente == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No hay un paciente en sesión.", "OK");
                return;
            }

            bool confirmar = await Application.Current.MainPage.DisplayAlert(
                "Confirmar eliminación",
                "¿Estás seguro de que deseas eliminar tu cuenta? Esta acción no se puede deshacer.",
                "Sí", "No");

            if (!confirmar)
                return;

            try
            {
                int resultado = await _pacienteRepository.Delete(_paciente);

                if (resultado > 0)
                {
                    _sesion.Limpiar();
                    _paciente = null; // <---- Aquí actualizamos la referencia local también

                    Application.Current.MainPage = new NavigationPage(new MainPage());

                    await Application.Current.MainPage.DisplayAlert("Cuenta eliminada", "Tu cuenta ha sido eliminada correctamente.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar la cuenta.", "OK");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al eliminar cuenta: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un error al eliminar tu cuenta.", "OK");
            }
        }



    }
}
