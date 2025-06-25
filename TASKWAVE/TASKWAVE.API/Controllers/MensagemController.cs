using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Services;
using TASKWAVE.DTO.Requests;
using TASKWAVE.DTO.Responses;

namespace TASKWAVE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        private readonly IMensagemService _messageService;

        public MensagemController(IMensagemService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MensagemResponse>>> GetAll()
        {
            var messages = await _messageService.GetAllMessages();
            var messageResponse = messages.Select(message => new MensagemResponse(message.ConteudoMensagem, message.DataEnvioMensagem, message.TarefaID));
            return Ok(messageResponse);
        }

        [HttpGet("{idMessage}")]
        public async Task<ActionResult<MensagemResponse>> GetById(int idMessage)
        {
            var message = await _messageService.GetMessageById(idMessage);
            if (message == null)
                return NotFound();
            return Ok(new MensagemResponse(message.ConteudoMensagem, message.DataEnvioMensagem, message.TarefaID));
        }        

        [HttpPost]
        public async Task<ActionResult> Create(MensagemRequest messageRequest)
        {
            var message = new Mensagem(messageRequest.messageContent, messageRequest.messageSentDate, messageRequest.taskId);
            await _messageService.CreateMessage(message);
            return CreatedAtAction(nameof(GetById), new { id = message.IdMensagem }, null);
        }

        [HttpGet("Tarefa/{idTarefa}/Mensagens")]
        public async Task<ActionResult<IEnumerable<MensagemResponse>>> GetMensagensPorTarefa(int idTarefa)
        {
            var mensagens = await _messageService.GetMensagensPorTarefaAsync(idTarefa);

            var response = mensagens.Select(m => new MensagemResponse(
                m.ConteudoMensagem,
                m.DataEnvioMensagem,
                m.TarefaID
            ));

            return Ok(response);
        }

        [HttpPut("{idMessage}")]
        public async Task<ActionResult> Update(int idMessage, MensagemRequest messageRequest)
        {

            var messageExist = await _messageService.GetMessageById(idMessage);
            if (messageExist == null)
            {
                return NotFound();
            }

            messageExist.ConteudoMensagem = messageRequest.messageContent;

            await _messageService.UpdateMessage(messageExist);
            return NoContent();
        }

        [HttpDelete("{idMessage}")]
        public async Task<ActionResult> Delete(int idMessage)
        {
            await _messageService.DeleteMessage(idMessage);
            return NoContent();
        }
    }
}
