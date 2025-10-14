using API.Repositorios.Interfaces;
using API.Services.Interfaces;


namespace API.Services.Implementacoes
{
    public class CorService : ICorService
    {
        private readonly ICorRepository _repository;

        public CorService(ICorRepository repository)
        {
            _repository = repository;

        }
        public async Task<List<string>> ObterCoresAsync()
        {
            List<string> cores = await _repository.Listar();
            return cores;
        }
    }
}
