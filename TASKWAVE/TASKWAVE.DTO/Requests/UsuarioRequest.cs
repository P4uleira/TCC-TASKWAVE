namespace TASKWAVE.DTO.Requests
{
    public class UsuarioRequest {        
        public string userName { get; set; } = string.Empty;

        public string userEmail { get; set; } = string.Empty;

        public string userPassword { get; set; } = string.Empty;

        public DateTime userCreationDate { get; set; } = DateTime.Now;

        public bool? newPassword { get; set; }
    }
}
