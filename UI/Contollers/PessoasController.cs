using System;
using System.Threading.Tasks;
using Dev.visitante.Aplication.Services;
using Dev.visitante.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dev.visitante.UI.Controllers
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService =
                pessoaService ?? throw new ArgumentNullException(nameof(pessoaService));
        }

        // Método GET para obter todas as pessoas de forma assíncrona.
        [HttpGet]
        public async Task<IActionResult> ObterTodasAsPessoasAsync()
        {
            try
            {
                var pessoas = await _pessoaService.ObterTodasAsPessoasAsync();

                return Ok(pessoas);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Falha ao obter todas as pessoas." });
            }
        }

        // Método GET para obter uma pessoa por ID de forma assíncrona.
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPessoaPorIdAsync(int id)
        {
            try
            {
                var pessoa = await _pessoaService.ObterPessoaPorIdAsync(id);

                if (pessoa == null)
                {
                    return NotFound(new { message = "Nenhuma pessoa encontrada" });
                }

                return Ok(pessoa);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Falha ao obter pessoa por ID." });
            }
        }

        // Método POST para adicionar uma nova pessoa de forma assíncrona.
        [HttpPost]
        public async Task<IActionResult> AdicionarPessoaAsync([FromBody] Pessoa pessoa)
        {
            try
            {
                await _pessoaService.AdicionarPessoaAsync(pessoa);

                return CreatedAtAction(
                    nameof(ObterPessoaPorIdAsync),
                    new { id = pessoa.Id },
                    pessoa
                );
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Falha ao adicionar nova pessoa." });
            }
        }

        // Método POST para criar uma nova pessoa com ID específico, também de forma assíncrona.
        [HttpPost("{id}")]
        public async Task<IActionResult> AdicionarPessoaComIdAsync(int id, [FromBody] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest("O ID fornecido não corresponde ao ID no corpo da solicitação.");
            }

            try
            {
                await _pessoaService.AdicionarPessoaAsync(pessoa);
            }
            catch (Exception)
            {
                return StatusCode(
                    500,
                    new { error = "Ocorreu um erro ao processar a solicitação." }
                );
            }

            return CreatedAtAction(nameof(ObterPessoaPorIdAsync), new { id = pessoa.Id }, pessoa);
        }

        // Método PUT para atualizar uma pessoa por ID de forma assíncrona.
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPessoaAsync(
            int id,
            [FromBody] Pessoa pessoaAtualizada
        )
        {
            try
            {
                var pessoaExistente = await _pessoaService.ObterPessoaPorIdAsync(id);

                if (pessoaExistente == null)
                {
                    return NotFound(new { message = "Pessoa não encontrada." });
                }

                pessoaExistente.UpDate(
                    pessoaAtualizada.Nome,
                    pessoaAtualizada.Descricao,
                    pessoaAtualizada.Profissao,
                    pessoaAtualizada.Idade
                );

                await _pessoaService.AtualizarPessoaAsync(id, pessoaAtualizada);

                return Ok(
                    new { message = "Pessoa atualizada com sucesso", pessoa = pessoaAtualizada }
                );
            }
            catch (Exception ex)
            {
                // Log da exceção para saber o que está dando errado, não esquecer de remover depois
                Console.WriteLine($"Erro: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(
                    500,
                    new { error = $"Ocorreu um erro ao processar a solicitação: {ex.Message}" }
                );
            }
        }

        // Método DELETE para excluir uma pessoa por ID de forma assíncrona.
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverPessoaAsync(int id)
        {
            try
            {
                var pessoaExistente = await _pessoaService.ObterPessoaPorIdAsync(id);

                if (pessoaExistente == null)
                {
                    return NotFound(new { message = "Pessoa não encontrada." });
                }

                await _pessoaService.RemoverPessoaAsync(id);

                return Ok(new { message = "Pessoa removida com sucesso" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Falha ao remover a pessoa." });
            }
        }
    }
}
