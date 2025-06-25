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

        [HttpPost]
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

            var claims = new List<Claim>
            {
                new Claim("user", accessList.IdUsuario.ToString()),
                new Claim("nome", accessList.NomeUsuario)
            };


            foreach (var access in accessList.Acessos)
            {
                claims.Add(new Claim("access", access.NomeAcesso));
            }

            string secretKey = _configuration.GetValue<string>("Jwt:Key");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            string issuerT = _configuration.GetValue<string>("Jwt:Issuer");
            string audienceT = _configuration.GetValue<string>("Jwt:Audience");

            var token = new JwtSecurityToken(
                issuer: issuerT,
                audience: audienceT,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
