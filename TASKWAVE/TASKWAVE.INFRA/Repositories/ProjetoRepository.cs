using Microsoft.EntityFrameworkCore;
using TASKWAVE.INFRA.Data;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;


namespace TASKWAVE.INFRA.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly TaskWaveContext _context;

        public ProjetoRepository(TaskWaveContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Projeto entity)
        {
            await _context.Projetos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateProjectToTeam(Projeto entity, int teamId)
        {
            await _context.Projetos.AddAsync(entity);
            await _context.SaveChangesAsync();

            var equipes = await _context.Equipes
            .Include(equipe => equipe.Projetos)
            .FirstOrDefaultAsync(equipe => equipe.IdEquipe == teamId);

            var project = await _context.Projetos.FindAsync(entity.IdProjeto);

            if (!equipes.Projetos.Contains(project))
            {
                equipes.Projetos.Add(project);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _context.Projetos.FindAsync(id);
            if (project != null)
            {
                _context.Projetos.Remove(project);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Projeto entity)
        {
            _context.Projetos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Projeto>> GetAllAsync()
        {
            return await _context.Projetos.Include(p => p.Equipes).ToListAsync();
        }

        public async Task<Projeto> GetByIdAsync(int id)
        {
            return await _context.Projetos.Include(p => p.Equipes).FirstOrDefaultAsync(p => p.IdProjeto == id);
        }
    }
}

