
using API.Dtos.Veiculo;
using API.Entidades;


namespace API.Repositorios.Interfaces
{
    public interface IVeiculoRepository : IBaseRepository<Veiculo>
    {
        Task<List<Opcional>> ListarOpcionais();
        Task<(IEnumerable<Veiculo>, int totalRegistros)> ListarPaginadoAsync(VeiculoFiltroDto filtro);
        Task<List<Portal>> ListarPortais();
        Task<Portal?> ObterPortalPorId(int id);


    }
}
