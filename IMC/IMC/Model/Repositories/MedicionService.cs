using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMC.Auxiliares;

namespace IMC.Model.Repositories
{
    class MedicionService : IMedicion
    {
        private readonly SQLiteHelper<Medicion> db; //conexión a la base de datos
        public MedicionService() 
        {
            db = new SQLiteHelper<Medicion>();
        }



        public Task<int> Delete(Medicion medicion)
            => Task.FromResult(db.Delete(medicion));

        public Task<List<Medicion>> GetAll()
            => Task.FromResult(db.GetAllData());

        public Task<Medicion?> GetById(int id)
            => Task.FromResult(db.Get(id));

        public Task<List<Medicion>> GetByPacienteId(int pacienteId)
            => Task.FromResult(db.GetAllData().Where(m => m.PacienteId == pacienteId).ToList());
        public Task<int> Insert(Medicion medicion)
            => Task.FromResult(db.Add(medicion));

        public Task<int> Update(Medicion medicion)
            => Task.FromResult(db.Update(medicion));
    }
}
