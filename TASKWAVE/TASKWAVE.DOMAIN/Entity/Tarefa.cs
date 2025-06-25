using TASKWAVE.DOMAIN.Enums;

namespace TASKWAVE.DOMAIN.ENTITY
{
    public class Tarefa
    {
        public int IdTarefa { get; set; }
        public string NomeTarefa { get; set; }
        public string? DescricaoTarefa { get; set; }
        public SituacaoTarefaEnum SituacaoTarefa { get; set; }
        public PrioridadeTarefaEnum PrioridadeTarefa { get; set; }
        public DateTime DataCriacaoTarefa { get; set; }
        public DateTime? DataPrevistaTarefa { get; set; }
        public DateTime? DataFinalTarefa { get; set; }
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }
        public ICollection<Mensagem> Mensagems { get; set; }
        public ICollection<HistoricoTarefa> HistoricoTarefas { get; set; }

        public Tarefa()
        {
        }

        public Tarefa(string taskName, string? taskDescription, SituacaoTarefaEnum taskStatus, PrioridadeTarefaEnum taskPriority, DateTime taskCreationDate, DateTime taskPlannedDate, DateTime taskFinalDate, int projectId)
        {
            NomeTarefa = taskName;
            DescricaoTarefa = taskDescription;
            SituacaoTarefa = taskStatus;
            PrioridadeTarefa = taskPriority;
            DataCriacaoTarefa = taskCreationDate;
            DataPrevistaTarefa = taskPlannedDate;
            DataFinalTarefa = taskFinalDate;
            ProjetoId = projectId;
        }
    }
}
