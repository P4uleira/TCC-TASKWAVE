using TASKWAVE.DOMAIN.ENTITY;

namespace TASKWAVE.DOMAIN.Interfaces.Services
{
    public interface ITarefaService
    {
        Task CreateTask(Tarefa task);
        Task UpdateTask(Tarefa task);
        Task DeleteTask(int idTask);
        Task<IEnumerable<Tarefa>> GetAllTasks();
        Task<Tarefa> GetTaskById(int idTask);
        Task<List<Tarefa>> GetTasksByUsuarioEquipe(int usuarioId);
    }
}
