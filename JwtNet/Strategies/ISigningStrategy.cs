using Microsoft.IdentityModel.Tokens;

namespace JwtNet.Strategies
{
    public interface ISigningStrategy
    {
        SecurityKey GetSecurityKey();
        SigningCredentials GetSigningCredentials();
        string Algorithm { get; }
    }
}
