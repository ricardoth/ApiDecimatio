using Decimatio.Domain.Interfaces;
using Decimatio.Infraestructure;
using Decimatio.Infraestructure.Contracts;
using Decimatio.Infraestructure.Repositories;
using System.Reflection;

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
            });

            //service.AddMvc().AddFluentValidation();

            #endregion

            #region Others Dependencies

            service.AddTransient<ITicketRepository, TicketRepository>();

            service.AddRepositories(configuration);

            #endregion
        }
    }
}
