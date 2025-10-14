using API.Dtos.Portal;
using API.Entidades;
using AutoMapper;

namespace API.Mappings
{
    public class PortalProfile : Profile
    {
        public PortalProfile() {

            CreateMap<Portal, PortalDto>().ReverseMap();
            CreateMap<Portal, DetalharPortalDto>().ForMember(dest => dest.Pacotes, opt => opt.MapFrom(src => src.Pacote));

        }
    }
}
