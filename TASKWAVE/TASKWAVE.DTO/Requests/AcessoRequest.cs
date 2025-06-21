using System.ComponentModel.DataAnnotations;

namespace TASKWAVE.DTO.Requests
{
    public class AcessoRequest
    {
        public int accessId { get; set; }

        [Required(ErrorMessage = "O nome do acesso é obrigatório.")]
        public string accessName { get; set; }

        [Required(ErrorMessage = "A descrição do acesso é obrigatória.")]
        public string accessDescription { get; set; }

        public DateTime accessCreationDate { get; set; }

    }
}