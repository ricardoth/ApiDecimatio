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

            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //service.AddMvc().AddFluentValidation();

            #endregion

            #region Others Dependencies

            service.AddTransient<ITicketRepository, TicketRepository>();

            service.AddRepositories(configuration);

            #endregion
        }
    }
}
