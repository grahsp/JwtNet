using JwtNet.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace JwtNet.Builderss
{
    public class JwtNetBuilder
    {
        private readonly IServiceCollection _services;

        public JwtNetBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public JwtNetBuilder AddSigningStrategy(ISigningStrategy strategy)
        {
            _services.AddSingleton(strategy);

            return this;
        }

        public JwtNetBuilder AddDefaultSigningStrategy(string secret)
        {
            _services.AddSingleton<ISigningStrategy>(new HmacSha256Strategy(secret));

            return this;
        }
    }
}
