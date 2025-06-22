using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;
using TASKWAVE.DOMAIN.Interfaces.Services;

namespace TASKWAVE.DOMAIN.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task CreateUsuario(Usuario usuario)
        {
            // Hash da senha antes de salvar o usuário
            var hasher = new PasswordHasher<Usuario>();
            usuario.SenhaUsuario = hasher.HashPassword(usuario, usuario.SenhaUsuario);
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task CreateUserToEquip(Usuario usuario, int teamId)
        {
            var hasher = new PasswordHasher<Usuario>();
            usuario.SenhaUsuario = hasher.HashPassword(usuario, usuario.SenhaUsuario);
            await _usuarioRepository.CreateUserToEquip(usuario, teamId);
        }
        public async Task UpdateUsuario(Usuario usuario)
        {
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteUsuario(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task<Usuario?> AutenticarAsync(string email, string senhaDigitada)
        {
            var usuario = await _usuarioRepository.BuscarPorEmailAsync(email);

            if (usuario == null)
                return null;

            var hasher = new PasswordHasher<Usuario>();
            var resultado = hasher.VerifyHashedPassword(usuario, usuario.SenhaUsuario, senhaDigitada);

            if (resultado == PasswordVerificationResult.Success)
            {
                return usuario;
            }

            return null; // senha inválida
        }


    }
}
