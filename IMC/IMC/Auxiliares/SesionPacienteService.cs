using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMC.Auxiliares
{
    internal class SesionPacienteService
    {
        public Paciente? PacienteActual { get; set; }

        public void EstablecerPaciente(Paciente paciente)
        {
            PacienteActual = paciente;
        }
        
        public void Limpiar()
        {
            PacienteActual = null;
        }

    }

}
