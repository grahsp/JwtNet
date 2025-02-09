using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtNet.Strategies
{
    public class HmacSha256Strategy : ISigningStrategy
    {
        private readonly byte[] _secret;

        public HmacSha256Strategy(string secret)
        {
            _secret = Encoding.UTF8.GetBytes(secret);
        }

        public string Algorithm => SecurityAlgorithms.HmacSha256;

        public SecurityKey GetSecurityKey() =>
            new SymmetricSecurityKey(_secret);


        public SigningCredentials GetSigningCredentials() =>
            new SigningCredentials(GetSecurityKey(), Algorithm);
    }
}
