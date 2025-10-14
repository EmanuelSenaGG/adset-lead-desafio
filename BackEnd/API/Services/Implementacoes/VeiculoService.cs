using API.Dtos;
using API.Dtos.Portal;
using API.Dtos.Veiculo;
using API.Entidades;
using API.Exceptions;
using API.Filtro;
using API.Repositorios.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementacoes
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _repository;
        private readonly IMapper _mapper;

        public VeiculoService(IVeiculoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AtualizarVeiculoDto> AtualizarVeiculoAsync(AtualizarVeiculoDto veiculoDto)
        {
       
            Veiculo? veiculoAtual = await _repository.ObterPeloId(veiculoDto.Id.Value);

            if (veiculoAtual == null)
                throw new NotFoundException("Veículo não encontrado.");


            _mapper.Map(veiculoDto, veiculoAtual);

            await _repository.Atualizar(veiculoAtual);

    
            AtualizarVeiculoDto veiculoAtualizadoDto = _mapper.Map<AtualizarVeiculoDto>(veiculoAtual);

            return veiculoAtualizadoDto;
        }


        public async Task<CadastrarVeiculoDto> CadastrarVeiculoAsync(CadastrarVeiculoDto cadastrarVeiculoDto)
        {
            Veiculo veiculo = _mapper.Map<Veiculo>(cadastrarVeiculoDto);
     
            Veiculo veiculoInserido = await _repository.Inserir(veiculo);
   
            return _mapper.Map<CadastrarVeiculoDto>(veiculoInserido);

        }

        public async Task DeletarVeiculoAsync(int id)
        {
            Veiculo? veiculo = await _repository.ObterPeloId(id);
            if (veiculo == null)
            {
                throw new NotFoundException("Veículo não encontrado");
            }

            await _repository.Deletar(veiculo);
        }

        public async Task<VeiculoDto> ObterPorIdAsync(int id)
        {
            Veiculo? veiculo = await _repository.ObterPeloId(id);

            if(veiculo == null)
            {
                throw new NotFoundException("Veículo não encontrado");
            }
            return _mapper.Map<VeiculoDto>(veiculo);
        }

   
        public async Task<List<OpcionalDto>> ListarOpcionaisAsync()
        {
            List<Opcional> opcionais = await _repository.ListarOpcionais();

            List<OpcionalDto> opcionaisDto = _mapper.Map<List<OpcionalDto>>(opcionais);

            return opcionaisDto;
        }

        public async Task<InformacoesVeiculosDto> ObterInformacoesAsync()
        {
            List<Veiculo> veiculos = await _repository.Listar();
            int totalVeiculos = veiculos.Count;
            int totalComFotos = veiculos.Count(v => v.Foto.Any());
            int totalSemFotos = totalVeiculos - totalComFotos;

            InformacoesVeiculosDto informacoes = new InformacoesVeiculosDto
            {
                Ids = veiculos.Select(v => v.Id).ToList(),
                Total = totalVeiculos,
                TotalFotos = totalComFotos,
                TotalSemFotos = totalSemFotos
            };

            return informacoes;
        }

        public async Task<PaginacaoResultado<VeiculoDto>> ListarVeiculosAsync(VeiculoFiltroDto filtro)
        {
            const int TAMANHO_MAXIMO_PAGINA = 10;
            if (filtro.TamanhoPagina <= 0)
                filtro.TamanhoPagina = 10; 
            else if (filtro.TamanhoPagina > TAMANHO_MAXIMO_PAGINA)
                filtro.TamanhoPagina = TAMANHO_MAXIMO_PAGINA;

            if (filtro.Pagina <= 0)
                filtro.Pagina = 1;

            var (veiculos, totalRegistros) = await _repository.ListarPaginadoAsync(filtro);

            List<VeiculoDto> dtos = _mapper.Map<List<VeiculoDto>>(veiculos);

            return new PaginacaoResultado<VeiculoDto>
            {
                Itens = dtos,
                PaginaAtual = filtro.Pagina,
                TamanhoPagina = filtro.TamanhoPagina,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<List<PortalDto>> ListarPortaisAsync()
        {
            List<Portal> portais = await _repository.ListarPortais();
            List<PortalDto> listaPortalDtos = _mapper.Map<List<PortalDto>>(portais);
            return listaPortalDtos;
        }

        public async Task<DetalharPortalDto> ObterPortalPorIDAsync(int id)
        {
            Portal? portal = await _repository.ObterPortalPorId(id);
            if (portal == null)
            {
                throw new NotFoundException("Portal não encontrado");
            }

            DetalharPortalDto detalharPortalDto = _mapper.Map<DetalharPortalDto>(portal);
            return detalharPortalDto;
 

        }

        public async Task<List<string>> ObterCoresAsync()
        {
            List<string> cores = await _repository.ObterCoresDisponiveis();
            return cores;
        }
    }
}
