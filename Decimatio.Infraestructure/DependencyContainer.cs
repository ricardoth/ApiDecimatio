﻿namespace Decimatio.Infraestructure
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
            services.AddScoped<IAccesoEventoRepository, AccesoEventoRepository>();
            services.AddScoped<ILugarRepository, LugarRepository>();
            services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IComunaRepository, ComunaRepository>();

            services.AddScoped<IMedioPagoService, MedioPagoService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<IAccesoEventoService, AccesoEventoService>();
            services.AddScoped<ILugarService, LugarService>();
            services.AddScoped<ITipoUsuarioService, TipoUsuarioService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IComunaService, ComunaService>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddScoped<IMercadoPagoService, MercadoPagoService>();
            services.AddScoped<IPayPalService, PayPalService>();

            //Commons & Configurations
            //services.AddScoped<IQRGeneratorService, QRGeneratorService>();
            //services.AddScoped<IEmailService, EmailService>();  
            //services.AddScoped<IBlobFilesService, BlobFilesService>();
            //services.AddScoped<IPDFGeneratorService, PDFGeneratorService>();
            //var blobConfig = configuration.GetSection(nameof(BlobContainerConfig)).Get<BlobContainerConfig>();
            //services.AddSingleton(blobConfig);

            //var emailConfig = configuration.GetSection(nameof(EmailConfig)).Get<EmailConfig>();
            //services.AddSingleton(emailConfig);

            //var encryptedConfig = configuration.GetSection(nameof(EncryptedTicketConfig)).Get<EncryptedTicketConfig>();
            //services.AddSingleton(encryptedConfig);
        }
    }
}
