using API.Dtos;
using API.Dtos.Portal;
using API.Dtos.Veiculo;
using API.Filtro;

namespace API.Services.Interfaces
{
    public interface IVeiculoService
    {
        Task<CadastrarVeiculoDto> CadastrarVeiculoAsync(CadastrarVeiculoDto veiculo);
        Task<AtualizarVeiculoDto> AtualizarVeiculoAsync(AtualizarVeiculoDto veiculo);
        Task<PaginacaoResultado<VeiculoDto>> ListarVeiculosAsync(VeiculoFiltroDto veiculoFiltro);
        Task DeletarVeiculoAsync(int id);
        Task<VeiculoDto> ObterPorIdAsync(int id);
        Task<List<OpcionalDto>> ListarOpcionaisAsync();
        Task<InformacoesVeiculosDto> ObterInformacoesAsync();
        Task<List<PortalDto>> ListarPortaisAsync();
        Task<DetalharPortalDto> ObterPortalPorIDAsync(int id);
        Task<List<string>> ObterCoresAsync();
    }
}
