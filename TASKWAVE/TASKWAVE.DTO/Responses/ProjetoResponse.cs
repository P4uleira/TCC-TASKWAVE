namespace TASKWAVE.DTO.Responses
{
    public record ProjetoResponse(int projectId, string projectName, string projectDescription, DateTime projectCreationDate, int? teamId);
}
