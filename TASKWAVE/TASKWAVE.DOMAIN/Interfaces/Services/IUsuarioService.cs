using TASKWAVE.DOMAIN.ENTITY;

namespace TASKWAVE.DOMAIN.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task CreateUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(int id);
        Task<IEnumerable<Usuario>> GetAllUsuarios();
        Task<Usuario> GetUsuarioById(int id);
        Task CreateUserToEquip(Usuario usuario, int teamId);
        Task<Usuario> GetByEmailWithAccessesAsync(string email);
    }
}
