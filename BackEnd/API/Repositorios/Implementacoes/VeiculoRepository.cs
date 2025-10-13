using API.Contexto;
using API.Entidades;
using API.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace API.Repositorios.Implementacoes
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly AdSetContext _context;

        public VeiculoRepository(AdSetContext context)
        {
            _context = context;
        }

        public async Task<Veiculo> Inserir(Veiculo veiculo)
        {
            await _context.Veiculo.AddAsync(veiculo);
            await _context.SaveChangesAsync();
            return veiculo;
        }

        public async Task Atualizar(Veiculo veiculo)
        {
            _context.Veiculo.Update(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Veiculo veiculo)
        {
            _context.Veiculo.Remove(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Veiculo>> Listar()
        {
            return  await _context.Veiculo
                    .Include(v => v.RelacaoVeiculoOpcional)
                        .ThenInclude(r => r.Opcional)
                    .Include(v => v.Foto)
                    .Include(v => v.RelacaoVeiculoPacotePortal)
                    .ThenInclude(p => p.Pacote)
                    .ThenInclude(p => p.Portal)
                    .ToListAsync();

        }

        public async Task<Veiculo?> ObterPeloId(int id)
        {
            Veiculo? veiculo = await _context.Veiculo
                    .Include(v => v.RelacaoVeiculoOpcional)
                        .ThenInclude(r => r.Opcional)
                    .Include(v => v.Foto)
                    .Include(v => v.RelacaoVeiculoPacotePortal)
                        .ThenInclude(r => r.Pacote)
                        .ThenInclude(r=> r.Portal)
                    .FirstOrDefaultAsync(v => v.Id == id);
            return veiculo;
        }

        public async Task<List<Opcional>> ListarOpcionais()
        {

            return await _context.Opcional.ToListAsync();
        }
    }

}
