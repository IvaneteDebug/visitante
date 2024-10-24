using System.Collections.Generic;
using System.Threading.Tasks;
using Dev.visitante.Domain.Models;

namespace Dev.visitante.Infrastructe.Repositories
{
    public interface IPessoaRepository
    {
        Task<IList<Pessoa>> ObterTodasAsPessoasAsync();
        Task<Pessoa?> ObterPessoaPorIdAsync(int id);
        Task AdicionarPessoaAsync(Pessoa pessoa);
        Task RemoverPessoaAsync(int id);
        Task AtualizarPessoaAsync(Pessoa pessoaAtualizada);
    }
}
