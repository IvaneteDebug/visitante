using Dev.visitante.Domain.Models;

namespace Dev.visitante.Aplication.Services
{
    public interface IPessoaService
    {
        Task<IList<Pessoa>> ObterTodasAsPessoasAsync();
        Task<Pessoa> ObterPessoaPorIdAsync(int id);
        Task AdicionarPessoaAsync(Pessoa pessoa);
        Task AdicionarPessoaComIdAsync(int id, Pessoa pessoa);
        Task<Pessoa> RemoverPessoaAsync(int id);
        Task AtualizarPessoaAsync(int id, Pessoa pessoaAtualizada);
    }
}