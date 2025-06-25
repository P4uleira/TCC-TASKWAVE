using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;
using TASKWAVE.DOMAIN.Interfaces.Services;

namespace TASKWAVE.DOMAIN.Services
{
    public class AcessoService : IAcessoService
    {
        private readonly IAcessoRepository _acessoRepository;

        public AcessoService(IAcessoRepository acessoRepository)
        {
            _acessoRepository = acessoRepository;
        }

        public async Task CreateAccess(Acesso Access)
        {
            await _acessoRepository.AddAsync(Access);
        }

        public async Task InsertAccessToUser(int idAcess, int idUser)
        {
            await _acessoRepository.InsertAccessToUser(idAcess, idUser);
        }
        public async Task UpdateAccess(Acesso Access)
        {
            await _acessoRepository.UpdateAsync(Access);
        }

        public async Task DeleteAccess(int id)
        {
            await _acessoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Acesso>> GetAllAccesses()
        {
            return await _acessoRepository.GetAllAsync();
        }

        public async Task<Acesso> GetAccessById(int id)
        {
            return await _acessoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<(int accessId, string accessName, int userId, string userName)>> GetAccessUserLinksAsync(int? accessId, int? userId)
        {
            return await _acessoRepository.GetAccessUserLinksAsync(accessId, userId);
        }

        public async Task DeleteAccessInUser(int accessId, int userId)
        {
            await _acessoRepository.DeleteAccessInUser(accessId, userId);
        }
    }
}
