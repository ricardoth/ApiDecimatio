﻿namespace Decimatio.Infraestructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<Ticket, TicketBodyQRDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Sector, SectorDto>().ReverseMap();
            CreateMap<MedioPago, MedioPagoDto>().ReverseMap();
            CreateMap<Lugar, LugarDto>().ReverseMap();
            CreateMap<Domain.Entities.Region, RegionDto>().ReverseMap();
            CreateMap<Comuna, ComunaDto>().ReverseMap();
        }
    }
}
