

namespace API.Repositorios.Interfaces
{
    public interface ICorRepository
    {
        Task<List<string>> Listar();
    }
}
