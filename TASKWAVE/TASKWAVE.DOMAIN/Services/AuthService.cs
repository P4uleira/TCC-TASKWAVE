using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TASKWAVE.DOMAIN.Entity;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;
using TASKWAVE.DOMAIN.Interfaces.Services;
using TASKWAVE.DOMAIN.Settings;

namespace TASKWAVE.DOMAIN.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUsuarioRepository usuarioRepository, IOptions<JwtSettings> jwtOptions)
        {
            _usuarioRepository = usuarioRepository;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<AuthResult> LoginAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.GetByEmailWithAccessesAsync(email);

            var hasher = new PasswordHasher<Usuario>();
            var resultado = hasher.VerifyHashedPassword(usuario, usuario.SenhaUsuario, senha);

            if (resultado != PasswordVerificationResult.Success)
                throw new Exception("Credenciais inválidas.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.EmailUsuario),
                new Claim(ClaimTypes.Name, usuario.NomeUsuario)
            };

            foreach (var acesso in usuario.Acessos)
            {
                claims.Add(new Claim("access", acesso.NomeAcesso));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new AuthResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
