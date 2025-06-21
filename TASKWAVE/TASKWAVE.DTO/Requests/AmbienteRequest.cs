using System.ComponentModel.DataAnnotations;

namespace TASKWAVE.DTO.Requests
{
    public class AmbienteRequest
    {
        public int environmentId { get; set; }

        [Required(ErrorMessage = "O nome do ambiente é obrigatório.")]
        public string environmentName { get; set; }

        [Required(ErrorMessage = "A descrição do ambiente é obrigatória.")]
        public string environmentDescription { get; set; }

    }
}