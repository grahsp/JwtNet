using JwtNet.Builders;
using JwtNet.Options;
using JwtNet.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JwtNet.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static JwtNetBuilder AddJwtNet(this IServiceCollection services)
        {
            return new JwtNetBuilder(services);
        }

        public static JwtNetBuilder AddJwtNet(this IServiceCollection services, Action<TokenOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<TokenService>();

            return new JwtNetBuilder(services);
        }
    }
}
