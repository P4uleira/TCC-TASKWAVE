namespace TASKWAVE.DTO.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public long Expiration { get; set; }
    }
}
