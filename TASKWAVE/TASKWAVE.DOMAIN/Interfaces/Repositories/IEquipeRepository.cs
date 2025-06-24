using TASKWAVE.DOMAIN.ENTITY;

namespace TASKWAVE.DOMAIN.Interfaces.Repositories
{
    public interface IEquipeRepository : IBaseRepository<Equipe>
    {
        public Task InsertProjectToTeam(int projectId, int teamId);
        public Task InsertUserToTeam(int userId, int teamId);
        public Task DeleteUserInTeam(int userId, int teamId);
        public Task<IEnumerable<(int TeamId, string TeamName, int ProjectId, string ProjectName)>> GetProjectTeamLinksAsync(int? teamId, int? projectId);
        public Task DeleteProjectFromTeam(int teamId, int projectId);
    }
}
