using TASKWAVE.DOMAIN.ENTITY;

namespace TASKWAVE.DOMAIN.Interfaces.Services
{
    public interface IEquipeService
    {
        Task CreateTeam(Equipe equipe);
        Task UpdateTeam(Equipe equipe);
        Task DeleteTeam(int id);
        Task<IEnumerable<Equipe>> GetAllTeams();
        Task<Equipe> GetTeamById(int id);
        Task InsertProjectToTeam(int projectId, int teamId);
        Task InsertUserToTeam(int userId, int teamId);
        Task DeleteUserInTeam(int userId, int teamID);
    }
}
