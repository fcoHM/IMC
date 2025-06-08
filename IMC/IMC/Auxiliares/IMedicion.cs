using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMC.Model;

namespace IMC.Auxiliares
{
    public interface IMedicion
    {
        public Task<List<Medicion>> GetAll();
        public Task<Medicion?> GetById(int id);
        public Task<int> Insert(Medicion medicion);
        public Task<int> Update(Medicion medicion);
        public Task<int> Delete(Medicion medicion);
        public Task<List<Medicion>> GetByPacienteId(int pacienteId); // Método para obtener mediciones por ID de paciente

    }
}
