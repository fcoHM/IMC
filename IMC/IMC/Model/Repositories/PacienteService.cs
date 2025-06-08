using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMC.Auxiliares;
namespace IMC.Model.Repositories
{
    class PacienteService : IPaciente
    {
        private readonly SQLiteHelper<Paciente> db; //conexión a la base de datos
        public PacienteService()
        {
            db = new SQLiteHelper<Paciente>(); // <-- aquí se inicializa
        }
        public Task<int> Delete(Paciente paciente)
            => Task.FromResult(db.Delete(paciente));

        public Task<List<Paciente>> GetAll()
            => Task.FromResult(db.GetAllData());

        public Task<Paciente?> GetById(int id)
            => Task.FromResult(db.Get(id));

        public Task<int> Insert(Paciente paciente)
            => Task.FromResult(db.Add(paciente));

        public Task<int> Update(Paciente paciente)
            => Task.FromResult(db.Update(paciente));
    }
}
