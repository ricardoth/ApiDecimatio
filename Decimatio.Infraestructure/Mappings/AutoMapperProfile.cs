namespace Decimatio.Infraestructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<Ticket, TicketBodyQRDto>().ReverseMap();
            CreateMap<TicketQR, TicketQRDto>().ReverseMap();    
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();
            CreateMap<UsuarioPass, UsuarioPassDto>().ReverseMap();
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Evento, EventoPostDto>().ReverseMap();
            CreateMap<Sector, SectorDto>().ReverseMap();
            CreateMap<MedioPago, MedioPagoDto>().ReverseMap();
            CreateMap<Lugar, LugarDto>().ReverseMap();
            CreateMap<Domain.Entities.Region, RegionDto>().ReverseMap();
            CreateMap<Comuna, ComunaDto>().ReverseMap();
            CreateMap<AccesoEvento, AccesoEventoDto>().ReverseMap();
            CreateMap<TicketAcceso, TicketAccesoDto>().ReverseMap();
            CreateMap<AccesoEventoTicket, AccesoEventoTicketDto>().ReverseMap();
            CreateMap<TipoUsuario, TipoUsuarioDto>().ReverseMap();  
            CreateMap<PreferenceTicket, PreferenceTicketDto>().ReverseMap();    
        }
    }
}
