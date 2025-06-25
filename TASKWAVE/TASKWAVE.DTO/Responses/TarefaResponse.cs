using TASKWAVE.DOMAIN.Enums;

namespace TASKWAVE.DTO.Responses
{
    public record TarefaResponse(int idTask, string taskName, string taskDescription, SituacaoTarefaEnum taskStatus, PrioridadeTarefaEnum taskPriority, DateTime taskCreationDate, DateTime? taskPlannedDate, DateTime? taskFinalDate, int projectId);
}
