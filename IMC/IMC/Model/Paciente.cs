using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IMC.Model
{
    [SQLite.Table("Paciente")]
    public class Paciente: BaseModel
    {
        // Properties for Paciente
        [SQLite.MaxLength(80)]
        public string Nombres { get; set; } = string.Empty; // Initialize to avoid null  
        [SQLite.MaxLength(80)]
        public string Apellidos { get; set; } = string.Empty; // Initialize to avoid null  
        [SQLite.MaxLength(1)]
        public int Sexo { get; set; } // '0' o '1'  

        public DateTime FechaNacimiento { get; set; }


        public override string ToString()
        {
            return $"{Nombres} {Apellidos}";
        }
    }
}
