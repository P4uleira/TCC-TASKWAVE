using System.ComponentModel.DataAnnotations;

namespace TASKWAVE.DTO.Requests
{
    public class ProjetoRequest
    {
        public int projectId { get; set; }

        [Required(ErrorMessage = "O nome do projeto é obrigatório.")]
        public string projectName { get; set; }

        [Required(ErrorMessage = "A descrição do projeto é obrigatória.")]
        public string projectDescription { get; set; }

        public DateTime projectCreationDate { get; set; }

        [Required(ErrorMessage = "A escolha da equipe é obrigatória.")]
        public int? teamId { get; set; }
    }
}