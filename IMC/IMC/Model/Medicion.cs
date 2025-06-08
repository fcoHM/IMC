using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMC.Model
{
    [SQLite.Table("Medicion")]
    public class Medicion: BaseModel
    {
        [Indexed]
        public int PacienteId { get; set; } // Clave foránea a Paciente
        public double peso { get; set; } // en kilogramos
        public double altura { get; set; } // en metros
        public double indiceMasaCorporal { get; set; } // imc
        public double actividadFisica { get; set; } // nivel de actividad (0-5)
        public double grasaCorporal { get; set; } // porcentaje de grasa corporal
        public double pesoIdeal { get; set; } // peso ideal calculado
        public double tdee { get; set; } // resultado del cálculo
        public DateTime fecha { get; set; } // fecha de la medición
        public string estatusIMC { get; set; } // para mostrar el estatus del IMC


    }
}
