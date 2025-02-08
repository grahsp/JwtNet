using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace JwtNet
{
    public interface ISigningStrategy
    {
        SecurityKey GetSecurityKey();
        SigningCredentials GetSigningCredentials();
        string Algorithm { get; }
    }

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

    public class RsaSha256Strategy : ISigningStrategy
    {
        private readonly RSA? _privateKey;
        private readonly RSA? _publicKey;

        public RsaSha256Strategy(RSA? privateKey = null, RSA? publicKey = null)
        {
            _privateKey = privateKey;
            _publicKey = publicKey;
        }

        public string Algorithm => SecurityAlgorithms.RsaSha256;

        public SecurityKey GetSecurityKey() =>
            new RsaSecurityKey(_publicKey);

        public SigningCredentials GetSigningCredentials()
        {
            if (_privateKey == null)
                throw new InvalidOperationException("Cannot sign tokens without a private key.");

            return new SigningCredentials(new RsaSecurityKey(_privateKey), Algorithm);
        }
    }
}
