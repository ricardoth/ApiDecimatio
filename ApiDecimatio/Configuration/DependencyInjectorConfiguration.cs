using Decimatio.Domain.CustomEntities;

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

            service.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            service.Configure<BasicAuthCredentials>(configuration.GetSection("BasicAuthCredentials"));

            service.AddRepositories(configuration);
            #endregion
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }
    }
}
