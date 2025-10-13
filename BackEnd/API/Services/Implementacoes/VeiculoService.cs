using API.Dtos;
using API.Dtos.Veiculo;
using API.Entidades;
using API.Exceptions;
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
            if (!veiculoDto.Id.HasValue)
                throw new ArgumentException("É necessário informar o ID do veículo no corpo.");

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

        public async Task<List<VeiculoDto>> ListarVeiculosAsync()
        {
            List<Veiculo> veiculos = await _repository.Listar();
            List<VeiculoDto> veiculosDto = _mapper.Map<List<VeiculoDto>>(veiculos);
            return veiculosDto;
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
    }
}
