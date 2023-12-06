namespace RelacoesComIdentity.DTOs
{
    public class TokenAndUserId
    {
        public string TokenString { get; set; }
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
    }
}
