using Microsoft.EntityFrameworkCore;
using TASKWAVE.INFRA.Data;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;


namespace TASKWAVE.INFRA.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly TaskWaveContext _context;

        public TarefaRepository(TaskWaveContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Tarefa task)
        {
            await _context.Tarefas.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int idTask)
        {
            var task = await _context.Tarefas.FindAsync(idTask);
            if (task != null)
            {
                _context.Tarefas.Remove(task);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Tarefa task)
        {
            _context.Tarefas.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<Tarefa> GetByIdAsync(int idTask)
        {
            return await _context.Tarefas.FindAsync(idTask);
        }

        public async Task<List<Tarefa>> GetTasksByUsuarioEquipe(int usuarioId)
        {
            return await _context.Tarefas
                .Where(t => t.Projeto.Equipes
                    .Any(e => e.Usuarios.Any(u => u.IdUsuario == usuarioId)))
                .ToListAsync();
        }
    }
}

