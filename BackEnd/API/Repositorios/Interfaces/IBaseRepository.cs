namespace API.Repositorios.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> ObterPeloId(int id);
        Task<ICollection<T>> Listar();
        Task<T> Inserir(T entity);
        Task Atualizar(T entity);
        Task Deletar(T entity);
    }
}
