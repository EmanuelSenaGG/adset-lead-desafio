using API.Contexto;
using API.Entidades;
using API.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace API.Repositorios.Implementacoes
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly AdSetDbContext _context;

        public VeiculoRepository(AdSetDbContext context)
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

        public async Task<ICollection<Veiculo>> Listar()
        {
            return await _context.Veiculo.ToListAsync();
        }

        public async Task<Veiculo?> ObterPeloId(int id)
        {
            return await _context.Veiculo.FindAsync(id);
        }
    }

}
