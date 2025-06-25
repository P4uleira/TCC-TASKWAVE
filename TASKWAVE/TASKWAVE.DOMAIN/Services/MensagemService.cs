using TASKWAVE.DOMAIN.ENTITY;
using TASKWAVE.DOMAIN.Interfaces.Repositories;
using TASKWAVE.DOMAIN.Interfaces.Services;

namespace TASKWAVE.DOMAIN.Services
{
    public class MensagemService : IMensagemService
    {
        private readonly IMensagemRepository _messageRepository;

        public MensagemService(IMensagemRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task CreateMessage(Mensagem message)
        {
            await _messageRepository.AddAsync(message);
        }
        

        public async Task UpdateMessage(Mensagem message)
        {
            await _messageRepository.UpdateAsync(message);
        }

        public async Task DeleteMessage(int idMessage)
        {
            await _messageRepository.DeleteAsync(idMessage);
        }

        public async Task<IEnumerable<Mensagem>> GetAllMessages()
        {
            return await _messageRepository.GetAllAsync();
        }

        public async Task<Mensagem> GetMessageById(int idMessage)
        {
            return await _messageRepository.GetByIdAsync(idMessage);
        }

        public async Task<IEnumerable<Mensagem>> GetMensagensPorTarefaAsync(int idTarefa)
        {
            return await _messageRepository.GetMensagensPorTarefaAsync(idTarefa);
        }
    }
}
