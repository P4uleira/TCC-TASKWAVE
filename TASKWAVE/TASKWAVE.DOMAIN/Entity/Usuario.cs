using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TASKWAVE.DOMAIN.ENTITY
{
    public class Usuario
    {
        [JsonPropertyName("userID")]
        public int IdUsuario { get; set; }

        [JsonPropertyName("userName")]
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [JsonPropertyName("userEmail")]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [JsonPropertyName("userPassword")]
        public string SenhaUsuario { get; set; }

        [JsonPropertyName("userCreationDate")]
        public DateTime DataCriacaoUsuario { get; set; }
        public ICollection<Equipe> Equipes { get; set; }
        public ICollection<Acesso> Acessos { get; set; }
        public string TokenRedefinicaoSenha { get; set; }
        public DateTime? DataExpiracaoToken { get; set; }

        public Usuario()
        {
            Equipes = default!;
            TokenRedefinicaoSenha = string.Empty;
        }
        public Usuario( string userName, string userEmail, string userPassword, DateTime userCreationDate)
        {
            NomeUsuario = userName;
            EmailUsuario = userEmail;
            SenhaUsuario = userPassword;
            DataCriacaoUsuario = userCreationDate;
            Equipes = default!;
            TokenRedefinicaoSenha = string.Empty;
        }        
        public Usuario(int userID, string userName, string userEmail, string userPassword, DateTime userCreationDate)
        {
            IdUsuario = userID;
            NomeUsuario = userName;
            EmailUsuario = userEmail;
            SenhaUsuario = userPassword;
            DataCriacaoUsuario = userCreationDate;
        }

        public Usuario(string userName, string userEmail, string userPassword, DateTime userCreationDate, ICollection<Equipe> teams)
        {
            NomeUsuario = userName;
            EmailUsuario = userEmail;
            SenhaUsuario = userPassword;
            DataCriacaoUsuario = userCreationDate;
            Equipes = teams;
        }
    }    
}
