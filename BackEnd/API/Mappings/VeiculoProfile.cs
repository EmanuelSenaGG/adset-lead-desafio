using API.Dtos;
using API.Dtos.Veiculo;
using API.Entidades;
using AutoMapper;

namespace API.Mappings
{
    public class VeiculoProfile : Profile
    {
        public VeiculoProfile()
        {

            CreateMap<Foto, FotoDto>().ReverseMap();
            CreateMap<Opcional, OpcionalDto>().ReverseMap();
            CreateMap<RelacaoVeiculoOpcional, RelacaoVeiculoOpcionalDto>()
                .ForMember(dest => dest.OpcionalId, opt => opt.MapFrom(src => src.OpcionalId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Opcional.Descricao));

            CreateMap<RelacaoVeiculoPacotePortal, RelacaoVeiculoPacotePortalDto>()
                .ForMember(dest => dest.PacoteId, opt => opt.MapFrom(src => src.PacoteId))
                .ForMember(dest => dest.NomePacote, opt => opt.MapFrom(src => src.Pacote.Nome))
                .ForMember(dest => dest.NomePortal, opt => opt.MapFrom(src => src.Pacote.Portal.Nome));

            CreateMap<Pacote, PacoteDto>().ReverseMap();
            CreateMap<Portal, PortalDto>().ReverseMap();




            CreateMap<Veiculo, VeiculoDto>()
                .ForMember(dest => dest.Fotos, opt => opt.MapFrom(src => src.Foto))
                .ForMember(dest => dest.Opcionais, opt => opt.MapFrom(src => src.RelacaoVeiculoOpcional))
                .ForMember(dest => dest.PacotesPortal, opt => opt.MapFrom(src => src.RelacaoVeiculoPacotePortal));

            CreateMap<CadastrarVeiculoDto, Veiculo>()
                .ForMember(dest => dest.RelacaoVeiculoOpcional,
                 opt => opt.MapFrom(src => src.Opcionais != null
                     ? src.Opcionais.Select(id => new RelacaoVeiculoOpcional { OpcionalId = id }).ToList()
                     : new List<RelacaoVeiculoOpcional>()));

         
            CreateMap<Veiculo, CadastrarVeiculoDto>()
                .ForMember(dest => dest.Opcionais, opt => opt.MapFrom(src => src.RelacaoVeiculoOpcional
                                                       .Select(r => r.OpcionalId)
                                                       .ToList()));

        }
    }
}
