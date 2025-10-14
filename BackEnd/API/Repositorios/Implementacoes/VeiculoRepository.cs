using API.Contexto;
using API.Dtos.Veiculo;
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

        public async Task Deletar(int id)
        {
            Veiculo veiculoAtual = await _context.Veiculo
                    .Include(v => v.RelacaoVeiculoOpcional)
                    .Include(v => v.RelacaoVeiculoPacotePortal)
                    .FirstAsync(v => v.Id.Equals(id));

            if (veiculoAtual.RelacaoVeiculoOpcional?.Any() == true)
                _context.RelacaoVeiculoOpcional.RemoveRange(veiculoAtual.RelacaoVeiculoOpcional);

            if (veiculoAtual.RelacaoVeiculoPacotePortal?.Any() == true)
                _context.RelacaoVeiculoPacotePortal.RemoveRange(veiculoAtual.RelacaoVeiculoPacotePortal);

            _context.Veiculo.Remove(veiculoAtual);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Veiculo>> Listar()
        {
            return await _context.Veiculo
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
                        .ThenInclude(r => r.Portal)
                    .FirstOrDefaultAsync(v => v.Id == id);
            return veiculo;
        }

        public async Task<(IEnumerable<Veiculo>, int totalRegistros)> ListarPaginadoAsync(VeiculoFiltroDto filtro)
        {
            IQueryable<Veiculo> query = _context.Veiculo
                .Include(v => v.Foto)
                .Include(v => v.RelacaoVeiculoOpcional)
                    .ThenInclude(r => r.Opcional)
                .Include(v => v.RelacaoVeiculoPacotePortal)
                    .ThenInclude(r => r.Pacote)
                    .ThenInclude(r => r.Portal)
                .AsQueryable();


            if (!string.IsNullOrEmpty(filtro.Marca))
                query = query.Where(v => v.Marca.ToLower().Contains(filtro.Marca.ToLower()));

            if (!string.IsNullOrEmpty(filtro.Modelo))
                query = query.Where(v => v.Modelo.ToLower().Contains(filtro.Modelo.ToLower()));

            if (filtro.AnoMin.HasValue)
                query = query.Where(v => v.Ano >= filtro.AnoMin.Value);

            if (filtro.AnoMax.HasValue)
                query = query.Where(v => v.Ano <= filtro.AnoMax.Value);

            if (!string.IsNullOrEmpty(filtro.Cor))
                query = query.Where(v => v.Cor.ToLower().Contains(filtro.Cor.ToLower()));

            if (filtro.PrecoMin.HasValue)
                query = query.Where(v => v.Preco >= filtro.PrecoMin.Value);

            if (filtro.PrecoMax.HasValue)
                query = query.Where(v => v.Preco <= filtro.PrecoMax.Value);
            if(filtro.Fotos)
                query = query.Where(v => v.Foto.Any() == true);
            if (!filtro.Fotos)
                query = query.Where(v => v.Foto.Any() == true);


            int totalRegistros = await query.CountAsync();


            List<Veiculo> veiculos = await query
                .OrderBy(v => v.Id)
                .Skip((filtro.Pagina - 1) * filtro.TamanhoPagina)
                .Take(filtro.TamanhoPagina)
                .ToListAsync();

            return (veiculos, totalRegistros);
        }

        public async Task<List<Opcional>> ListarOpcionais()
        {
            return await _context.Opcional.ToListAsync();
        }



    }

}
