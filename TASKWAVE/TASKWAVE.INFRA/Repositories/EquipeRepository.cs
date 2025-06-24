using Microsoft.EntityFrameworkCore;
using TASKWAVE.INFRA.Data;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;


namespace TASKWAVE.INFRA.Repositories
{
    public class EquipeRepository : IEquipeRepository
    {
        private readonly TaskWaveContext _context;

        public EquipeRepository(TaskWaveContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Equipe team)
        {
            await _context.Equipes.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task InsertProjectToTeam(int idProject, int idTeam)
        {
            var team = await _context.Equipes
            .Include(e => e.Projetos)
            .FirstOrDefaultAsync(e => e.IdEquipe == idTeam);

            var project = await _context.Projetos.FindAsync(idProject);

            if (team == null || project == null)
            {
                throw new Exception("Equipe ou Projeto não encontrados.");
            }

            if (!team.Projetos.Any(p => p.IdProjeto == idProject))
            {
                team.Projetos.Add(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task InsertUserToTeam(int idUser, int idTeam)
        {
            var team = await _context.Equipes
            .Include(e => e.Usuarios)
            .FirstOrDefaultAsync(e => e.IdEquipe == idTeam);

            var user = await _context.Usuarios.FindAsync(idUser);

            if (team == null || user == null)
            {
                throw new Exception("Equipe ou Usuario não encontrados.");
            }

            if (!team.Usuarios.Any(p => p.IdUsuario == idUser))
            {
                team.Usuarios.Add(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteUserInTeam(int idUser, int idTeam)
        {
            var team = await _context.Equipes
                .Include(e => e.Usuarios)
                .FirstOrDefaultAsync(e => e.IdEquipe == idTeam);

            if (team == null)
                throw new Exception("Equipe não encontrada.");

            var user = team.Usuarios.FirstOrDefault(u => u.IdUsuario == idUser);

            if (user == null)
                throw new Exception("Usuário não está vinculado a esta equipe.");

            team.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int idTeam)
        {
            var team = await _context.Equipes.FindAsync(idTeam);
            if (team != null)
            {
                _context.Equipes.Remove(team);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Equipe team)
        {
            _context.Equipes.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Equipe>> GetAllAsync()
        {
            return await _context.Equipes.ToListAsync();
        }

        public async Task<Equipe> GetByIdAsync(int idTeam)
        {
            return await _context.Equipes.FindAsync(idTeam);
        }

        public async Task<List<Usuario>> BuscarUsuariosDaEquipeAsync(int idEquipe)
        {
            var equipe = await _context.Equipes
                .Include(e => e.Usuarios)
                .FirstOrDefaultAsync(e => e.IdEquipe == idEquipe);

            return equipe?.Usuarios.ToList() ?? new List<Usuario>();
        }

        public async Task<IEnumerable<(int TeamId, string TeamName, int ProjectId, string ProjectName)>> GetProjectTeamLinksAsync(int? teamId, int? projectId)
        {
            var query = _context.Equipes
                .Include(e => e.Projetos)
                .AsQueryable();

            if (teamId.HasValue)
                query = query.Where(e => e.IdEquipe == teamId.Value);

            var result = await query
                .SelectMany(e => e.Projetos
                    .Where(p => !projectId.HasValue || p.IdProjeto == projectId.Value)
                    .Select(p => new
                    {
                        TeamId = e.IdEquipe,
                        TeamName = e.NomeEquipe,
                        ProjectId = p.IdProjeto,
                        ProjectName = p.NomeProjeto
                    }))
                .ToListAsync();

            return result.Select(x => (x.TeamId, x.TeamName, x.ProjectId, x.ProjectName));
        }

        public async Task DeleteProjectFromTeam(int teamId, int projectId)
        {
            var equipe = await _context.Equipes
                .Include(e => e.Projetos)
                .FirstOrDefaultAsync(e => e.IdEquipe == teamId);

            if (equipe == null) return;

            var projeto = equipe.Projetos.FirstOrDefault(p => p.IdProjeto == projectId);
            if (projeto != null)
            {
                equipe.Projetos.Remove(projeto);
                await _context.SaveChangesAsync();
            }
        }
    }
}

