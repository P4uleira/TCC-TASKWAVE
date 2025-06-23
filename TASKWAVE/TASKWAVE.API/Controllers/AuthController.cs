using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TASKWAVE.DOMAIN.Interfaces.Services;
using TASKWAVE.DTO.Responses;
using TASKWAVE.DTO.Requests;

namespace TASKWAVE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request.Email, request.Senha);

                var response = new LoginResponse
                {
                    Token = result.Token,
                    Expiration = result.Expiration
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { mensagem = ex.Message });
            }
        }
        
    }
}
