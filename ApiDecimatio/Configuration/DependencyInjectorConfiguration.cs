using Decimatio.WebApi.Authentication;
using Decimatio.WebApi.Models;
using Microsoft.AspNetCore.Authentication;

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

            //service.AddMvc().AddFluentValidation();
            #endregion


            #region Others Dependencies
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            service.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            service.Configure<BasicAuthCredentials>(configuration.GetSection("BasicAuthCredentials"));


            service.AddRepositories(configuration);

            #endregion
        }
    }
}
