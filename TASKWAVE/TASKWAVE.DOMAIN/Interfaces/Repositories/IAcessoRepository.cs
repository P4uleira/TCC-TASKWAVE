using TASKWAVE.DOMAIN.ENTITY;

namespace TASKWAVE.DOMAIN.Interfaces.Repositories
{
    public interface IAcessoRepository : IBaseRepository<Acesso>
    {
        public Task InsertAccessToUser(int idAccess, int idUser);
        public Task<IEnumerable<(int accessId, string accessName, int userId, string userName)>> GetAccessUserLinksAsync(int? accessId, int? userId);
        public Task DeleteAccessInUser(int accessId, int userId);
    }
}
