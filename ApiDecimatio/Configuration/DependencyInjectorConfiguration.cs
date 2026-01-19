using Decimatio.Application;
using Decimatio.Common;
using Decimatio.Domain.ValueObjects;
using PdfSharp.Charting;

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

            var mercadoPagoConfig = configuration.GetSection("MercadoPagoOptions").Get<MercadoPagoOptions>();
            service.Configure<MercadoPagoOptions>(configuration.GetSection("MercadoPagoOptions"));

            service.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            service.Configure<BasicAuthCredentials>(configuration.GetSection("BasicAuthCredentials"));
            service.Configure<PasswordOptions>(configuration.GetSection("PasswordOptions"));

            service.AddApplicationDependencies(configuration);
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
