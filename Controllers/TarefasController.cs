using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTarefas.Data;
using ProjetoTarefas.Models;

namespace ProjetoTarefas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public TarefasController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CriarTarefa([FromBody] Tarefa tarefa)
        {
            if (!ModelState.IsValid) // Verifica se os dados enviados são válidos
            {
                return BadRequest(ModelState); // Retorna um erro 400 com os detalhes das validações que falharam
            }

            _appDbContext.Tarefas.Add(tarefa);
            await _appDbContext.SaveChangesAsync();

            return Created("Tarefa adicionado com sucesso!", tarefa);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ObterTarefas() //Async pois o método não é instantaneo 
        {
            var tarefas = await _appDbContext.Tarefas.ToListAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> ObterTarefa(int id) 
        {
            var tarefa = await _appDbContext.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTarefa(int id, [FromBody] Tarefa tarefaAtualizada)
        {
            var tarefaExistente = await _appDbContext.Tarefas.FindAsync(id);

            if (tarefaExistente == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            _appDbContext.Entry(tarefaExistente).CurrentValues.SetValues(tarefaAtualizada);

            await _appDbContext.SaveChangesAsync();

            return StatusCode(201, tarefaExistente); // 201 Created
        }
        


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletePersonagem(int id)
        {
            var tarefa = await _appDbContext.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            _appDbContext.Tarefas.Remove(tarefa);

            await _appDbContext.SaveChangesAsync();

            return Ok("Personagem deletado com sucesso!"); 
        }
    }
}

