using CommunityToolkit.Mvvm.Input;
using IMC.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMC.ViewModel
{
    public partial class VMCalculadora : ObservableValidator, IQueryAttributable
    {
        private readonly SesionPacienteService _sesion;
        private readonly IMedicion _medicionService;

        [ObservableProperty]
        private string nombreCompleto;
        public VMCalculadora()
        {
            _sesion = App.Current.Services.GetRequiredService<SesionPacienteService>();
            _medicionService = App.Current.Services.GetRequiredService<IMedicion>();
            Paciente paciente = _sesion.PacienteActual;

        }

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


        [ObservableProperty]
        private double peso; // en kilogramos

        [ObservableProperty]
        private double altura; // en metros

        [ObservableProperty]
        private double indiceMasaCorporal; // imc

        [ObservableProperty]
        private double grasaCorporal; // porcentaje de grasa corporal

        [ObservableProperty]
        private double pesoIdeal; // peso ideal calculado

        [ObservableProperty]
        private string seleccion; // para el nivel de actividad

        [ObservableProperty]
        private double tdee; // resultado del cálculo

        [ObservableProperty]
        private string estatusIMC; // para mostrar el estatus del IMC
        public void CalcularIMC()
        {
            if (Altura > 0)
            {
                IndiceMasaCorporal = Math.Round(Peso / Math.Pow(Altura / 100.0, 2), 2);
            }
            else
            {
                IndiceMasaCorporal = 0;
            }
        }

        public void CalcularGC()
        {
            var paciente = _sesion.PacienteActual;

            if (paciente == null)
            {
                GrasaCorporal = 0;
                return;
            }

            // Asegúrate de que el IMC ya está calculado
            CalcularIMC();

            int edad = CalcularEdad(paciente.FechaNacimiento);
            int sexo = paciente.Sexo; // 1 = hombre, 0 = mujer

            GrasaCorporal = Math.Round((1.2 * IndiceMasaCorporal) + (0.23 * edad) - (10.8 * sexo) - 5.4, 2);
        }

        private int CalcularEdad(DateTime fechaNacimiento) //saber que edad tiene el paciente
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }

        public void CalcularPesoIdeal()
        {
            var paciente = _sesion.PacienteActual;

            if (paciente == null || Altura <= 0)
            {
                PesoIdeal = 0;
                return;
            }

            double estaturaCm = Altura * 100; // Convertir de metros a centímetros
            int sexo = paciente.Sexo; // 1 = hombre, 0 = mujer

            if (sexo == 1)
            {
                // Masculino
                PesoIdeal = Math.Round(estaturaCm - 100 - ((estaturaCm - 150) / 4), 2);
            }
            else
            {
                // Femenino
                PesoIdeal = Math.Round(estaturaCm - 100 - ((estaturaCm - 150) / 2.5), 2);
            }
        }

        public void CalcularTDEE()
        {
            var paciente = _sesion.PacienteActual;
            if (paciente == null) return;

            int edad = DateTime.Now.Year - paciente.FechaNacimiento.Year;
            if (DateTime.Now.DayOfYear < paciente.FechaNacimiento.DayOfYear)
                edad--;

            double alturaCm = Altura; // ya viene en cm desde la vista
            double pesoKg = Peso;
            int sexo = paciente.Sexo;

            // BMR según sexo
            double bmr = 0;
            if (sexo == 1) // Hombre
                bmr = (alturaCm * 6.25) + (pesoKg * 9.99) - (edad * 4.92) + 5;
            else // Mujer
                bmr = (alturaCm * 6.25) + (pesoKg * 9.99) - (edad * 4.92) - 161;

            double factorActividad = Seleccion switch
            {
                "Rara vez" => 1.2,
                "1 a 3 dias" => 1.375,
                "3 a 5 dias" => 1.55,
                "6 a 7 dias" => 1.725,
                "Todos los dias" => 1.9,
                _ => 1.2
            };

            Tdee = Math.Round(bmr * factorActividad, 2);
        }

        public void validarIMC()
        {
            if (IndiceMasaCorporal < 18.5)
            {
                EstatusIMC = "Bajo peso";
            }
            else if (IndiceMasaCorporal >= 18.5 && IndiceMasaCorporal < 24.9)
            {
                EstatusIMC = "Peso normal";
            }
            else if (IndiceMasaCorporal >= 25 && IndiceMasaCorporal < 29.9)
            {
                EstatusIMC = "Sobrepeso";
            }
            else
            {
                EstatusIMC = "Obesidad";

            }
        }





        [RelayCommand]
        public void Calcular()
        {
            CalcularIMC();
            CalcularGC();
            CalcularPesoIdeal();
            CalcularTDEE();
            validarIMC();

            var paciente = _sesion.PacienteActual;

            if (paciente == null)
                return; // Seguridad ante posibles errores

            Medicion medicion = new Medicion
            {
                PacienteId = paciente.ID,
                fecha = DateTime.Now,
                peso = Peso,
                altura = Altura,
                indiceMasaCorporal = IndiceMasaCorporal,
                grasaCorporal = GrasaCorporal,
                pesoIdeal = PesoIdeal,
                tdee = Tdee,
                estatusIMC = EstatusIMC,
            };

            // Guardar en base de datos
            _medicionService.Insert(medicion);

        }
    }
}


