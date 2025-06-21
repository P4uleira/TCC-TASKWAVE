using Microsoft.AspNetCore.Mvc;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Services;
using TASKWAVE.DTO.Requests;
using TASKWAVE.DTO.Responses;

namespace TASKWAVE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmbienteController : ControllerBase
    {
        private readonly IAmbienteService _environmentService;

        public AmbienteController(IAmbienteService environmentService)
        {
            _environmentService = environmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmbienteResponse>>> GetAll()
        {
            var environments = await _environmentService.GetAllEnvironments();
            var response = environments.Select(environment => new AmbienteResponse(environment.IdAmbiente, environment.NomeAmbiente, environment.DescricaoAmbiente));
            return Ok(response);
        }

        [HttpGet("{idEnvironment}")]
        public async Task<ActionResult<AmbienteResponse>> GetById(int idEnvironment)
        {
            var environment = await _environmentService.GetEnvironmentById(idEnvironment);
            if (environment == null)
                return NotFound();
            return Ok(new AmbienteResponse(environment.IdAmbiente, environment.NomeAmbiente, environment.DescricaoAmbiente));
        }

        [HttpPost]
        public async Task<ActionResult> Create(AmbienteRequest environmentRequest)
        {
            var environment = new Ambiente(environmentRequest.environmentName, environmentRequest.environmentDescription);
            await _environmentService.CreateEnvironment(environment);
            return Ok();
        }


        [HttpPut("{idEnvironment}")]
        public async Task<ActionResult> Update(int idEnvironment, AmbienteRequest environmentRequest)
        {

            var existingEnvironment = await _environmentService.GetEnvironmentById(idEnvironment);
            if (existingEnvironment == null)
            {
                return NotFound();
            }

            existingEnvironment.NomeAmbiente = environmentRequest.environmentName;
            existingEnvironment.DescricaoAmbiente = environmentRequest.environmentDescription;
            
            await _environmentService.UpdateEnvironment(existingEnvironment);
            return NoContent();
        }

        [HttpDelete("{idEnvironment}")]
        public async Task<ActionResult> Delete(int idEnvironment)
        {
            await _environmentService.DeleteEnvironment(idEnvironment);
            return NoContent();
        }
    }
}
