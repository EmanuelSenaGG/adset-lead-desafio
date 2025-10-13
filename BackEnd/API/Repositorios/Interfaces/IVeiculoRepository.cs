using API.Entidades;

namespace API.Repositorios.Interfaces
{
    public interface IVeiculoRepository : IBaseRepository<Veiculo>
    {
        Task<List<Opcional>> ListarOpcionais();
     
    }
}
