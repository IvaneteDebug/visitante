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

        // Método PUT para atualizar uma pessoa por ID de forma assíncrona.
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPessoaAsync(
            int id,
            [FromBody] Pessoa pessoaAtualizada
        )
        {
            try
            {
                // Obtém a pessoa existente do serviço, já tratando a validação no serviço.
                var pessoaExistente = await _pessoaService.ObterPessoaPorIdAsync(id);

                // Se a pessoa não foi encontrada, retorna um erro 404
                if (pessoaExistente == null)
                {
                    return NotFound(new { message = "Pessoa não encontrada." });
                }

                // Chama o método UpDate para atualizar os dados da pessoa existente
                pessoaExistente.UpDate(
                    pessoaAtualizada.Nome,
                    pessoaAtualizada.Observacao,
                    pessoaAtualizada.Profissao,
                    pessoaAtualizada.Documento,
                    pessoaAtualizada.Telefone,
                    pessoaAtualizada.Email,
                    pessoaAtualizada.DataNascimento,
                    pessoaAtualizada.Bloco,
                    pessoaAtualizada.Apartamento,
                    pessoaAtualizada.MoradorResponsavel,
                    pessoaAtualizada.MotivoVisita,
                    pessoaAtualizada.DataVisita,
                    pessoaAtualizada.HoraEntrada,
                    pessoaAtualizada.HoraSaida,
                    pessoaAtualizada.VisitanteRecorrente,
                    pessoaAtualizada.PlacaCarro,
                    pessoaAtualizada.StatusAprovacao
                );

                // Atualiza os dados no serviço ou no banco de dados
                await _pessoaService.AtualizarPessoaAsync(pessoaExistente);

                // Retorna a pessoa atualizada com status 200 OK
                return Ok(pessoaExistente);
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro inesperado, retorna um erro 500
                return StatusCode(
                    500,
                    new { message = "Erro interno do servidor", details = ex.Message }
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
