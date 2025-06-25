using TASKWAVE.DOMAIN.ENTITY;

namespace TASKWAVE.DOMAIN.Interfaces.Repositories
{
    public interface IEquipeRepository : IBaseRepository<Equipe>
    {
        public Task InsertProjectToTeam(int projectId, int teamId);
        public Task InsertUserToTeam(int userId, int teamId);
        public Task DeleteUserInTeam(int userId, int teamId);
        public Task<IEnumerable<(int teamId, string teamName, int projectId, string projectName)>> GetProjectTeamLinksAsync(int? teamId, int? projectId);
        public Task<IEnumerable<(int teamId, string teamName, int userId, string userName)>> GetUserTeamLinksAsync(int? teamId, int? userId);
        public Task DeleteProjectFromTeam(int teamId, int projectId);
    }
}
