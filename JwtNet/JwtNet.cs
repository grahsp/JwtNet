using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtNet
{
    public class JwtNet
    {
        private readonly JwtNetOptions _options;
        private readonly ISigningStrategy _signingStrategy;

        public JwtNet(IOptions<JwtNetOptions> options)
        {
            _options = options.Value;
            _signingStrategy = _options.SigningStrategy;
        }

        public string CreateToken(IEnumerable<Claim> claims)
        {
            var jwtToken = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiresInMinutes),
                signingCredentials: _signingStrategy.GetSigningCredentials()
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
