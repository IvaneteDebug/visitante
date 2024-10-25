using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dev.visitante.Domain.Models;
using Dev.visitante.Infrastructe.Persitence;
using Microsoft.EntityFrameworkCore;

namespace Dev.visitante.Infrastructe.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly PessoaDbContext _dbContext;

        public PessoaRepository(PessoaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IList<Pessoa>> ObterTodasAsPessoasAsync()
        {
            return await _dbContext.Pessoas!.ToListAsync();
        }

        public async Task<Pessoa?> ObterPessoaPorIdAsync(int id)
        {
            return await _dbContext.Pessoas!.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AdicionarPessoaAsync(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                throw new ArgumentNullException(nameof(pessoa), "A pessoa não pode ser nula.");
            }

            await _dbContext.Pessoas!.AddAsync(pessoa);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoverPessoaAsync(int id)
        {
            var pessoa = await _dbContext.Pessoas!.FirstOrDefaultAsync(p => p.Id == id);
            if (pessoa != null)
            {
                _dbContext.Pessoas!.Remove(pessoa);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AtualizarPessoaAsync(Pessoa pessoaAtualizada)
        {
            if (pessoaAtualizada == null)
            {
                throw new ArgumentNullException(
                    nameof(pessoaAtualizada),
                    "A pessoa atualizada não pode ser nula."
                );
            }

            var pessoaExistente = await _dbContext.Pessoas!.FirstOrDefaultAsync(p =>
                p.Id == pessoaAtualizada.Id
            );
            if (pessoaExistente != null)
            {
                pessoaExistente.Nome = pessoaAtualizada.Nome;
                pessoaExistente.Profissao = pessoaAtualizada.Profissao;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
