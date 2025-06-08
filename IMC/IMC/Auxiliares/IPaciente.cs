

namespace IMC.Auxiliares
{
    public interface IPaciente
    {
        public Task<List<Paciente>> GetAll();
        public Task<Paciente?> GetById(int id);
        public Task<int> Insert(Paciente paciente);
        public Task<int> Update(Paciente paciente);
        public Task<int> Delete(Paciente paciente);
    }
}
