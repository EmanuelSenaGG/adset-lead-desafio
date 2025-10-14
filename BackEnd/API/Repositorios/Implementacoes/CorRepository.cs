using API.Contexto;
using API.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositorios.Implementacoes
{
    public class CorRepository : ICorRepository
    {
        private readonly AdSetContext _context;
        public CorRepository(AdSetContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Listar()
        {
            List<string> cores = await _context.Veiculo
                                    .Select(v => v.Cor)
                                    .Distinct()
                                    .ToListAsync();
            return cores;
        }
    }
}
