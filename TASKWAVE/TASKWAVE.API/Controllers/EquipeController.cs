using Microsoft.AspNetCore.Mvc;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Services;
using TASKWAVE.DTO.Requests;
using TASKWAVE.DTO.Responses;

namespace TASKWAVE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeService _teamService;

        public EquipeController(IEquipeService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipeResponse>>> GetAll()
        {
            var teams = await _teamService.GetAllTeams();
            var response = teams.Select(team => new EquipeResponse(team.IdEquipe, team.NomeEquipe, team.DescricaoEquipe, team.SetorId));
            return Ok(response);
        }

        [HttpGet("{idTeam}")]
        public async Task<ActionResult<EquipeResponse>> GetById(int idTeam)
        {
            var team = await _teamService.GetTeamById(idTeam);
            if (team == null)
                return NotFound();
            return Ok(new EquipeResponse(team.IdEquipe, team.NomeEquipe, team.DescricaoEquipe, team.SetorId));
        }

        [HttpPost("AddProjectToTeam/{TeamId}/{ProjectId}")]
        public async Task InsertProjectToTeam(int TeamId, int ProjectId)
        {
            await _teamService.InsertProjectToTeam(ProjectId, TeamId);
        }

        [HttpPost("AddUserToTeam/{TeamId}/{userId}")]
        public async Task InsertUserToTeam(int TeamId, int userId)
        {
            await _teamService.InsertUserToTeam(userId, TeamId);
        }

        [HttpPost]
        public async Task<ActionResult> Create(EquipeRequest teamRequest)
        {
            var team = new Equipe(teamRequest.teamName, teamRequest.teamDescription, teamRequest.sectorId);
            await _teamService.CreateTeam(team);
            return CreatedAtAction(nameof(GetById), new { idTeam = team.IdEquipe }, null);
        }


        [HttpPut("{idTeam}")]
        public async Task<ActionResult> Update(int idTeam, EquipeRequest teamRequest)
        {

            var teamExists = await _teamService.GetTeamById(idTeam);
            if (teamExists == null)
            {
                return NotFound();
            }

            teamExists.NomeEquipe = teamRequest.teamName;
            teamExists.DescricaoEquipe = teamRequest.teamDescription;
            teamExists.SetorId = teamRequest.sectorId;

            await _teamService.UpdateTeam(teamExists);
            return NoContent();
        }

        [HttpDelete("{idTeam}")]
        public async Task<ActionResult> Delete(int idTeam)
        {
            await _teamService.DeleteTeam(idTeam);
            return NoContent();
        }
    }
}
