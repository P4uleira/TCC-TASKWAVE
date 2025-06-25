using TASKWAVE.DOMAIN.ENTITY;

namespace TASKWAVE.DOMAIN.Interfaces.Repositories
{
    public interface IMensagemRepository : IBaseRepository<Mensagem>
    {
        Task<IEnumerable<Mensagem>> GetMensagensPorTarefaAsync(int idTarefa);
    }
}
