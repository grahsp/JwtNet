namespace JwtNet
{
    public class JwtNetOptions
    {
        public required ISigningStrategy SigningStrategy { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiresInMinutes { get; set; } = 30;
    }
}
