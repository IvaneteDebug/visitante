using Dev.visitante.Aplication.Services;
using Dev.visitante.Domain.Models;
using Dev.visitante.Infrastructe.Repositories;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _pessoaRepository;

    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository ?? throw new ArgumentNullException(nameof(pessoaRepository));
    }

    public async Task<IList<Pessoa>> ObterTodasAsPessoasAsync()
    {
        return await _pessoaRepository.ObterTodasAsPessoasAsync();
    }

    public async Task<Pessoa> ObterPessoaPorIdAsync(int id)
    {
        return await _pessoaRepository.ObterPessoaPorIdAsync(id) ?? throw new Exception("Pessoa não encontrada");
    }

    public async Task AdicionarPessoaAsync(Pessoa pessoa)
    {
        await _pessoaRepository.AdicionarPessoaAsync(pessoa);
    }

    public async Task AdicionarPessoaComIdAsync(int id, Pessoa pessoa)
    {
        await _pessoaRepository.AdicionarPessoaComIdAsync(id, pessoa);
    }

    public async Task<Pessoa> RemoverPessoaAsync(int id)
    {
        var pessoa = await ObterPessoaPorIdAsync(id);
        await _pessoaRepository.RemoverPessoaAsync(id);
        return pessoa;
    }

    public async Task AtualizarPessoaAsync(int id, Pessoa pessoaAtualizada)
    {
        // Adicione lógica de validação se necessário
        await _pessoaRepository.AtualizarPessoaAsync(pessoaAtualizada);
    }
}