using System.ComponentModel.DataAnnotations;

namespace TASKWAVE.DTO.Requests
{
    public class EquipeRequest
    {
        public int teamId { get; set; }

        [Required(ErrorMessage = "O nome da equipe é obrigatório.")]
        public string teamName { get; set; }

        [Required(ErrorMessage = "A descrição da equipe é obrigatória.")]
        public string teamDescription { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione um setor válido.")]
        public int sectorId { get; set; }
    }
}