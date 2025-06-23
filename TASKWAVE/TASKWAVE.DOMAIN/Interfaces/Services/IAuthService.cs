using TASKWAVE.DOMAIN.Entity;

namespace TASKWAVE.DOMAIN.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string senha);
    }
}
