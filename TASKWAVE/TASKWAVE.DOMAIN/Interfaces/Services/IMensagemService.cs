using TASKWAVE.DOMAIN.ENTITY;

namespace TASKWAVE.DOMAIN.Interfaces.Services
{
    public interface IMensagemService
    {
        Task CreateMessage(Mensagem Message);
        Task UpdateMessage(Mensagem Message);
        Task DeleteMessage(int idMessage);
        Task<IEnumerable<Mensagem>> GetAllMessages();
        Task<Mensagem> GetMessageById(int idMessage);
        Task<IEnumerable<Mensagem>> GetMensagensPorTarefaAsync(int idTarefa);
    }
}
