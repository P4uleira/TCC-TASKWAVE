using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TASKWAVE.DOMAIN.Interfaces.Services;
using TASKWAVE.DTO.Responses;
using TASKWAVE.DTO.Requests;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace TASKWAVE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _userService;

        public AuthController(IConfiguration configuration, IUsuarioService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var userExist = await _userService.AutenticarAsync(loginRequest.Email, loginRequest.Senha);

            if (userExist != null)
            {
                var token = await GenerateJwtToken(loginRequest.Email);

                return Ok(new LoginResponse { Token = token });
            }

            return Unauthorized("Email ou senha inválidos");
        }

        private async Task<string> GenerateJwtToken(string email)
        {
            var accessList = await _userService.GetByEmailWithAccessesAsync(email);

            var now = DateTimeOffset.UtcNow;
            var expires = now.AddHours(1);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, accessList.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, accessList.NomeUsuario),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Exp, expires.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64) // Adicionado Exp correto
            };



            foreach (var access in accessList.Acessos)
            {
                claims.Add(new Claim(ClaimTypes.Role, access.NomeAcesso));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                notBefore: now.UtcDateTime,
                expires: expires.UtcDateTime,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
