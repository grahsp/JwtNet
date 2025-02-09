using JwtNet.Options;
using JwtNet.Strategies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtNet.Services
{
    public class JwtNet
    {
        private readonly JwtNetOptions _options;
        private readonly ISigningStrategy _signingStrategy;

        public JwtNet(ISigningStrategy signingStrategy, IOptions<JwtNetOptions> options)
        {
            _options = options.Value;
            _signingStrategy = signingStrategy;
        }

        public JwtNet(ISigningStrategy signingStrategy)
        {
            _options = new JwtNetOptions();
            _signingStrategy = signingStrategy;
        }

        private string? GenerateToken(IEnumerable<Claim>? claims = null)
        {
            try
            {
                var jwtToken = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    expires: _options.Expires,
                    signingCredentials: _signingStrategy.GetSigningCredentials()
                );

                return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }
            catch (SecurityTokenEncryptionFailedException ex)
            {
                throw new InvalidOperationException("Invalid Signing Strategy: The signing credentials or algorithm are incompatible or incorrectly configured.", ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Invalid argument(s) passed when generating the token. Please check the issuer, audience, or expiration time for correctness.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while generating the token.", ex);
            }
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var validationParams = new TokenValidationParameters
            {
                IssuerSigningKey = _signingStrategy.GetSecurityKey(),
                ValidateIssuer = _options.ValidateIssuer,
                ValidIssuer = _options.Issuer,
                ValidateAudience = _options.ValidateAudience,
                ValidAudience = _options.Audience,
                ValidateLifetime = _options.ValidateLifetime,
                ClockSkew = _options.ClockSkew
            };

            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token, validationParams, out var _);

            return principal;
        }
    }
}
