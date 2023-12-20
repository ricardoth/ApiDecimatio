using Decimatio.Common;
using Decimatio.Infraestructure.Options;
using MercadoPago.Config;

namespace Decimatio.WebApi.Configuration
{
    public static class DependencyInjectorConfiguration
    {
        public static void UseDependencyInjectorConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            #region Main Dependencies (don't touch)
            service.AddControllers(options =>
            {
                //options.Filters.Add(new ApiValidStateFilterAttribute());
                //options.Filters.Add(new ApiExceptionFilterAttribute(trace));
            }).AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            #endregion

            #region Others Dependencies
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            service.Configure<PaginationOptions>(configuration.GetSection("Pagination"));

            service.Configure<MercadoPagoOptions>(configuration.GetSection("MercadoPagoOptions"));
            MercadoPagoConfig.AccessToken = "TEST-2316722112827129-121918-2f6270125458fded4a2c767099d9aafa-437710298";


                        service.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            service.Configure<BasicAuthCredentials>(configuration.GetSection("BasicAuthCredentials"));
            service.Configure<PasswordOptions>(configuration.GetSection("PasswordOptions"));

            service.AddRepositories(configuration);
            service.AddCommonDependencies(configuration);
            #endregion
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}
