using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;
using TASKWAVE.DOMAIN.Interfaces.Services;

namespace TASKWAVE.DOMAIN.Services
{
    public class EquipeService : IEquipeService
    {
        private readonly IEquipeRepository _teamRepository;

        public EquipeService(IEquipeRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task CreateTeam(Equipe team)
        {
            await _teamRepository.AddAsync(team);
        }

        public async Task UpdateTeam(Equipe team)
        {
            await _teamRepository.UpdateAsync(team);
        }

        public async Task InsertProjectToTeam(int idProject, int idTeam)
        {
            await _teamRepository.InsertProjectToTeam(idProject, idTeam);
        }

        public async Task InsertUserToTeam(int idUser, int idTeam)
        {
            await _teamRepository.InsertUserToTeam(idUser, idTeam);
        }

        public async Task DeleteUserInTeam(int idUser, int idTeam)
        {
            await _teamRepository.DeleteUserInTeam(idUser, idTeam);
        }
        public async Task DeleteTeam(int idTeam)
        {
            await _teamRepository.DeleteAsync(idTeam);
        }

        public async Task<IEnumerable<Equipe>> GetAllTeams()
        {
            return await _teamRepository.GetAllAsync();
        }

        public async Task<Equipe> GetTeamById(int idTeam)
        {
            return await _teamRepository.GetByIdAsync(idTeam);
        }

    }
}
