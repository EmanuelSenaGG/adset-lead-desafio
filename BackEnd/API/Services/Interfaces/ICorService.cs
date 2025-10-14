namespace API.Services.Interfaces
{
    public interface ICorService
    {
        Task<List<string>> ObterCoresAsync();
    }
}
