using Microsoft.AspNetCore.Mvc;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DTO.Requests;
using TASKWAVE.DOMAIN.Interfaces.Services;
using TASKWAVE.DTO.Responses;

namespace TASKWAVE.DTO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessoController : ControllerBase
    {
        private readonly IAcessoService _accessService;

        public AcessoController(IAcessoService accessService)
        {
            _accessService = accessService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcessoResponse>>> GetAll()
        {
            var access = await _accessService.GetAllAccesses();
            var accessResponse = access.Select(access => new AcessoResponse(access.IdAcesso,access.NomeAcesso, access.DescricaoAcesso, access.DataCriacaoAcesso));
            return Ok(accessResponse);
        }

        [HttpGet("{idAccess}")]
        public async Task<ActionResult<AcessoResponse>> GetById(int idAccess)
        {
            var access = await _accessService.GetAccessById(idAccess);
            if (access == null)
                return NotFound();
            return Ok(new AcessoResponse(access.IdAcesso, access.NomeAcesso, access.DescricaoAcesso, access.DataCriacaoAcesso));
        }

        [HttpPost]
        public async Task<ActionResult> Create(AcessoRequest accessRequest)
        {
            var access = new Acesso(accessRequest.accessName, accessRequest.accessDescription, accessRequest.accessCreationDate);
            await _accessService.CreateAccess(access);
            return CreatedAtAction(nameof(GetById), new { idAccess = access.IdAcesso }, null);
        }

        [HttpPost("AddAccessToUser/{idAccess}/{idUser}")]
        public async Task InsertAccessToUser(int idAccess, int idUser)
        {
            await _accessService.InsertAccessToUser(idAccess, idUser);
        }

        [HttpPut("{idAccess}")]
        public async Task<ActionResult> Update(int idAccess, AcessoRequest accessRequest)
        {

            var accessExist = await _accessService.GetAccessById(idAccess);
            if (accessExist == null)
            {
                return NotFound();
            }

            accessExist.NomeAcesso = accessRequest.accessName;
            accessExist.DescricaoAcesso = accessRequest.accessDescription;

            await _accessService.UpdateAccess(accessExist);
            return NoContent();
        }

        [HttpDelete("{idAccess}")]
        public async Task<ActionResult> Delete(int idAccess)
        {
            await _accessService.DeleteAccess(idAccess);
            return NoContent();
        }
    }
}
