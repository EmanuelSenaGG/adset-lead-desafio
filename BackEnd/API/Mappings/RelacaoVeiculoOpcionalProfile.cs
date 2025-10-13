using API.Dtos;
using API.Entidades;
using AutoMapper;

namespace API.Mappings
{
    public class RelacaoVeiculoOpcionalProfile : Profile
    {
        public RelacaoVeiculoOpcionalProfile() {

            CreateMap<RelacaoVeiculoOpcional, RelacaoVeiculoOpcionalDto>()
                .ForMember(dest => dest.OpcionalId, opt => opt.MapFrom(src => src.OpcionalId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Opcional.Descricao));
        }
    }
}
