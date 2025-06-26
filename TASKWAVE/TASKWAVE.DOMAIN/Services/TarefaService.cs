using Microsoft.EntityFrameworkCore;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;
using TASKWAVE.DOMAIN.Interfaces.Services;

namespace TASKWAVE.DOMAIN.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _taskRepository;
        private readonly IHistoricoTarefaRepository _taskHistoryRepository;

        public TarefaService(ITarefaRepository taskRepository, IHistoricoTarefaRepository taskHistoryRepository)
        {
            _taskRepository = taskRepository;
            _taskHistoryRepository = taskHistoryRepository;
        }

        public async Task CreateTask(Tarefa task)
        {
            await _taskRepository.AddAsync(task);

            var newTaskHistory = new HistoricoTarefa
            {
                DataMudancaTarefa = DateTime.Now,
                SituacaoAtualTarefa = task.SituacaoTarefa,
                SituacaoAnteriorTarefa = null,
                PrioridadeAtualTarefa = task.PrioridadeTarefa,
                PrioridadeAnteriorTarefa = null,
                TarefaID = task.IdTarefa
            };

            await _taskHistoryRepository.InsertTaskHistory(newTaskHistory);
        }
        public async Task UpdateTask(Tarefa task)
        {
            await _taskRepository.UpdateAsync(task);

            var taskHistoryToUpdate = new HistoricoTarefa
            {
                DataMudancaTarefa = DateTime.Now,
                SituacaoAtualTarefa = task.SituacaoTarefa,
                PrioridadeAtualTarefa = task.PrioridadeTarefa,
                TarefaID = task.IdTarefa
            };

            await _taskHistoryRepository.UpdateTaskHistory(taskHistoryToUpdate);
        }

        public async Task DeleteTask(int idTask)
        {
            await _taskRepository.DeleteAsync(idTask);
        }

        public async Task<IEnumerable<Tarefa>> GetAllTasks()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<Tarefa> GetTaskById(int idTask)
        {
            return await _taskRepository.GetByIdAsync(idTask);
        }

        public async Task<List<Tarefa>> GetTasksByUsuarioEquipe(int usuarioId)
        {
            return await _taskRepository.GetTasksByUsuarioEquipe(usuarioId);
        }
    }
}
