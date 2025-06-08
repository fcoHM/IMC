using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IMC.Model;
using IMC.Model.Repositories;
using IMC.View;
using Microsoft.Maui.Controls;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using IMC.Auxiliares;

namespace IMC.ViewModel
{
    public partial class VMPageRegister : ObservableValidator
    {
        private readonly IPaciente _pacienteRepository;
        private readonly SesionPacienteService _sesionPaciente;

        public ObservableCollection<string> Errores { get; set; } = new();
        

        private string nombres = string.Empty; // Initialize to avoid CS8618
        [Required(ErrorMessage = "El campo Nombres es obligatorio.")]
        [System.ComponentModel.DataAnnotations.MaxLength(80, ErrorMessage = "El campo Nombres no puede exceder los 80 caracteres.")]
        public string Nombres { get => nombres; set => SetProperty(ref nombres, value, true); }

        private string apellidos = string.Empty; // Initialize to avoid CS8618
        [Required(ErrorMessage = "El campo Apellidos es obligatorio.")]
        [System.ComponentModel.DataAnnotations.MaxLength(80, ErrorMessage = "El campo Apellidos no puede exceder los 80 caracteres.")]
        public string Apellidos { get => apellidos; set => SetProperty(ref apellidos, value, true); }

        private string sexoSeleccionado = string.Empty;

        [Required(ErrorMessage = "El campo Sexo es obligatorio.")]
        public string SexoSeleccionado
        {
            get => sexoSeleccionado;
            set => SetProperty(ref sexoSeleccionado, value, true);
        }


        private DateTime fechaNacimiento;
        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio.")]
        public DateTime FechaNacimiento { get => fechaNacimiento; set => SetProperty(ref fechaNacimiento, value, true); }

        public VMPageRegister()
        {
            _pacienteRepository = App.Current.Services.GetRequiredService<IPaciente>();
            _sesionPaciente = App.Current.Services.GetRequiredService<SesionPacienteService>();
            FechaNacimiento = DateTime.Now.AddYears(-10); // Valor por defecto: 10 años atrás
        }



        [RelayCommand]
        public async Task GuardarPaciente()
        {
            ValidateAllProperties();
            Errores.Clear();
            GetErrors(nameof(Nombres)).ToList().ForEach(f => Errores.Add("Nombre: " + f.ErrorMessage));
            GetErrors(nameof(Apellidos)).ToList().ForEach(f => Errores.Add("Apellidos: " + f.ErrorMessage));
            GetErrors(nameof(SexoSeleccionado)).ToList().ForEach(f => Errores.Add("Sexo: " + f.ErrorMessage));
            GetErrors(nameof(FechaNacimiento)).ToList().ForEach(f => Errores.Add("Fecha nacimiento: " + f.ErrorMessage));

            if (Errores.Count > 0)
                return;

            int sexo = SexoSeleccionado switch
            {
                "Masculino" => 1,
                "Femenino" => 0,
                _ => 0
            };

            if (sexo == 0)
            {
                Errores.Add("Sexo no válido.");
                return;
            }

            Paciente nuevoPaciente = new Paciente
            {
                Nombres = Nombres+" ",
                Apellidos = Apellidos,
                Sexo = sexo,
                FechaNacimiento = FechaNacimiento
            };

           
            int resultado = await _pacienteRepository.Insert(nuevoPaciente);
            _sesionPaciente.EstablecerPaciente(nuevoPaciente); // Establecer el paciente actual en la sesión

            await Shell.Current.GoToAsync($"{nameof(Home)}?refresh=true");



        }

    }
}
