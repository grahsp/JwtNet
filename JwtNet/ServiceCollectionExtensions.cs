using Microsoft.Extensions.DependencyInjection;

namespace JwtNet
{
    public static class ServiceCollectionExtensions
    {
        public static JwtNetBuilder AddJwtNet(this IServiceCollection services)
        {
            return new JwtNetBuilder(services);
        }

        public static JwtNetBuilder AddJwtNet(this IServiceCollection services, Action<JwtNetOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<JwtNet>();

            return new JwtNetBuilder(services);
        }
    }
}
