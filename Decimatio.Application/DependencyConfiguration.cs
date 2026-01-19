using Decimatio.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Decimatio.Application
{
    public static class DependencyConfiguration
    {
        public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
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
            services.AddScoped<IPayPalService, PayPalService>();


        }
    }
}
