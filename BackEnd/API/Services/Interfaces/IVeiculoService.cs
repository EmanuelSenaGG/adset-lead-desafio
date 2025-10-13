using API.Dtos;
using API.Dtos.Veiculo;

namespace API.Services.Interfaces
{
    public interface IVeiculoService
    {
        Task<CadastrarVeiculoDto> CadastrarVeiculoAsync(CadastrarVeiculoDto veiculo);
        Task<AtualizarVeiculoDto> AtualizarVeiculoAsync(AtualizarVeiculoDto veiculo);
        Task DeletarVeiculoAsync(int id);
        Task<VeiculoDto> ObterPorIdAsync(int id);
        Task<List<VeiculoDto>> ListarVeiculosAsync();
        Task<List<OpcionalDto>> ListarOpcionaisAsync();
        Task<InformacoesVeiculosDto> ObterInformacoesAsync();
    }
}
