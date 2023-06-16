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
            services.AddScoped<IMedioPagoRepository, MedioPagoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ISectorRepository, SectorRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            
            services.AddScoped<IMedioPagoService, MedioPagoService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<IQRGeneratorService, QRGeneratorService>();
            services.AddScoped<IBlobFilesService, BlobFilesService>();
            var blobConfig = configuration.GetSection(nameof(BlobContainerConfig)).Get<BlobContainerConfig>();
            services.AddSingleton(blobConfig);
            // Connection
            services.AddScoped<IDataBaseConnection, DataBaseConnection>();
        }
    }
}
