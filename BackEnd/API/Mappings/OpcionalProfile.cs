using API.Dtos;
using API.Entidades;
using AutoMapper;

namespace API.Mappings
{
    public class OpcionalProfile : Profile
    {
        public OpcionalProfile() {
            CreateMap<Opcional, OpcionalDto>().ReverseMap();
        }
    }
}
