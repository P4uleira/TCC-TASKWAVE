using System.ComponentModel.DataAnnotations;

namespace TASKWAVE.DTO.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Informe o e-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        public string Senha { get; set; }
    }
}
