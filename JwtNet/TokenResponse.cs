namespace JwtNet
{
    public class TokenResponse
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }
        public DateTime Expires { get; }

        public TokenResponse(string accessToken, string refreshToken, DateTime expires)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Expires = expires;
        }
    }
}
