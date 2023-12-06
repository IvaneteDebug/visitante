using Dev.visitante.Domain.Models;
using Dev.visitante.Infrastructe.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dev.visitante.UI.Controllers
{
    [Route("api/pessoas")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private PessoaDbContext _dbContext;
        public PessoasController(PessoaDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
        }

        // Método GET para obter todas as pessoas de forma assíncrona
        [HttpGet]
        public async Task<IActionResult> ObterTodasAsPessoasAsync()
        {
            try
            {
                var pessoas = await _dbContext.Pessoas!.ToListAsync();
                return Ok(pessoas);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Falha ao obter todas as pessoas." });
            }
        }

        // Método GET para obter uma pessoa por ID de forma assíncrona.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pessoa = await _dbContext.Pessoas!.FirstOrDefaultAsync(p => p.Id == id);

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
        public async Task<IActionResult> Post([FromBody] Pessoa pessoa)
        {
            try
            {
                _ = await _dbContext.Pessoas!.AddAsync(pessoa);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Falha ao adicionar nova pessoa." });
            }
        }

        // Método POST para criar uma nova pessoa com ID específico, tabém de forma assíncrona.
        [HttpPost("{id}")]
        public async Task<IActionResult> AdicionarPessoaComIdAsync(int id, [FromBody] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest("O ID fornecido na URL não corresponde ao ID no corpo da solicitação.");
            }

            await _dbContext.Pessoas!.AddAsync(pessoa);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Falha ao adicionar pessoa no banco de dados." });
            }

            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        // Método PUT para atualizar uma pessoa por ID de forma assíncrona.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pessoa pessoaAtualizada)
        {
            try
            {
                var pessoaExistente = await _dbContext.Pessoas!.FindAsync(id);

                if (pessoaExistente == null)
                {
                    return NotFound(new { message = "Nenhum id/pessoa não encontrado" });
                }

                pessoaExistente.Nome = pessoaAtualizada.Nome;
                pessoaExistente.Profissao = pessoaAtualizada.Profissao;

                _dbContext.Pessoas.Update(pessoaExistente);
                await _dbContext.SaveChangesAsync();

                return Ok(new { Mensagem = "Pessoa atualizada com sucesso", Pessoa = pessoaAtualizada });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Falha ao atualizar pessoa." });
            }
        }

        // Método DELETE para excluir uma pessoa por ID de forma assíncrona.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var pessoa = await _dbContext.Pessoas!.FirstOrDefaultAsync(p => p.Id == id);

                if (pessoa == null)
                {
                    return NotFound(new { message = "Id pessoa não encontrado" });
                }

                _dbContext.Pessoas!.Remove(pessoa);
                await _dbContext.SaveChangesAsync();

                return Ok(new { Mensagem = "Pessoa removida com sucesso", Pessoa = pessoa });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Falha ao excluir pessoa." });
            }
        }
    }
}
