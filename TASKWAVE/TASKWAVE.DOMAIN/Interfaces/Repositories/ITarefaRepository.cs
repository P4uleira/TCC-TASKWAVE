using TASKWAVE.DOMAIN.ENTITY;

namespace TASKWAVE.DOMAIN.Interfaces.Repositories
{
    public interface ITarefaRepository : IBaseRepository<Tarefa>{
        public Task<List<Tarefa>> GetTasksByUsuarioEquipe(int usuarioId);
    }
}
