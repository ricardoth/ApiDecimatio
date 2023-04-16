

namespace Decimatio.Infraestructure
{
    public static class DependencyContainer
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection(nameof(DataBaseConfig)).Get<DataBaseConfig>();
            services.AddSingleton(config);

            // Repositories
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IQRGeneratorService, QRGeneratorService>();

            // Connection
            services.AddScoped<IDataBaseConnection, DataBaseConnection>();
        }
    }
}
