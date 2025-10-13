using API.Dtos;
using API.Entidades;
using AutoMapper;

namespace API.Mappings
{
    public class PortalProfile : Profile
    {
        public PortalProfile() {

            CreateMap<Portal, PortalDto>().ReverseMap();
        }
    }
}
