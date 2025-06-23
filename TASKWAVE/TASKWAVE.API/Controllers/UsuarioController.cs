using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Services;
using TASKWAVE.DOMAIN.Services;
using TASKWAVE.DTO.Requests;
using TASKWAVE.DTO.Responses;

namespace TASKWAVE.API.Controllers
{
    [Authorize(Policy = "AcessoAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _userService;

        public UsuarioController(IUsuarioService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetAll()
        {
            var users = await _userService.GetAllUsuarios();
            var response = users.Select(users => new UsuarioResponse(users.IdUsuario, users.NomeUsuario, users.EmailUsuario, users.SenhaUsuario, users.DataCriacaoUsuario));
            return Ok(response);
        }

        [HttpGet("{idUser}")]
        public async Task<ActionResult<UsuarioResponse>> GetById(int idUser)
        {
            var users = await _userService.GetUsuarioById(idUser);
            if (users == null)
                return NotFound();
            return Ok(new UsuarioResponse(users.IdUsuario, users.NomeUsuario, users.EmailUsuario, users.SenhaUsuario, users.DataCriacaoUsuario));
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsuarioRequest projectRequest)
        {
            var users = new Usuario(projectRequest.userName, projectRequest.userEmail, projectRequest.userPassword, projectRequest.userCreationDate);
            await _userService.CreateUsuario(users);
            return CreatedAtAction(nameof(GetById), new { idUser = users.IdUsuario }, null);
        }

        [HttpPost("AddUserInEquip")]
        public async Task CreateUserToEquip(UsuarioRequest users, int teamId)
        {
            var newUsuario = new Usuario(users.userName, users.userEmail, users.userPassword, users.userCreationDate);
            await _userService.CreateUserToEquip(newUsuario, teamId);
        }
        [HttpPut("{idUser}")]
        public async Task<ActionResult> Update(int idUser, UsuarioRequest projectRequest)
        {

            var existingUser = await _userService.GetUsuarioById(idUser);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.NomeUsuario = projectRequest.userName;
            existingUser.EmailUsuario = projectRequest.userEmail;

            if (projectRequest.newPassword == true)
            {
                existingUser.TokenRedefinicaoSenha = "1";
            }
            existingUser.SenhaUsuario = projectRequest.userPassword;

            await _userService.UpdateUsuario(existingUser);
            return NoContent();
        }

        [HttpDelete("{idUser}")]
        public async Task<ActionResult> Delete(int idUser)
        {
            await _userService.DeleteUsuario(idUser);
            return NoContent();
        }

        [HttpGet("{id}/Equipes")]
        public async Task<ActionResult<List<EquipeResponse>>> GetEquipesDoUsuario(int id)
        {
            try
            {
                var equipes = await _userService.BuscarEquipesDoUsuarioAsync(id);
                return Ok(equipes);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}
