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
        public async Task<ActionResult<List<Tarefa>>> ObterTodasTarefasDireto()
        {
            var tarefas = await _appDbContext.Tarefas.ToListAsync();
           return Ok(tarefas);
        }


//Collections        

        [HttpGet("list")]
        public async Task<ActionResult<List<Tarefa>>> ObterTodasTarefas()
        {
            var tarefas = await _appDbContext.Tarefas.ToListAsync();
            return tarefas;
        }

        [HttpGet("Id-Titulo")]
        public async Task<ActionResult<Dictionary<int, string>>> IdTitulo()
        {
            var tarefas = await _appDbContext.Tarefas.ToListAsync();
            var mapa = tarefas.ToDictionary(t => t.Id, t => t.Titulo);
            return mapa;
        }

        [HttpGet("Concluidas")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> TarefasConcluidas()
        {
           var tarefas = await _appDbContext.Tarefas.Where(t => t.Concluida == true).ToListAsync();

            if (!tarefas.Any())
                return NotFound("Nenhuma tarefa concluída encontrada.");

           return Ok(tarefas);
        }

        [HttpGet("Nao concluidas")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> TarefasNaoConcluidas()
        {
           var tarefas = await _appDbContext.Tarefas.Where(t => t.Concluida == false).ToListAsync();

            if (!tarefas.Any())
           return NotFound("Nenhuma tarefa não concluída encontrada.");

            return Ok(tarefas);
        }

//Linq

        [HttpGet("Buscar Descricao")]
        public async Task<ActionResult<IEnumerable<string>>> BuscarDescricao(string palavra)
        {
            if (palavra == null)
            {
            return BadRequest("A palavra-chave não pode ser nula.");
            }

        var descricoes = await _appDbContext.Tarefas
        .Where(t => t.Descricao.Contains(palavra)).Select(t => t.Descricao).ToListAsync();

            return descricoes.Any() ? Ok(descricoes) : NotFound("Nenhuma descrição encontrada com essa palavra-chave.");
        }

        [HttpGet("Ordenar Por Titulo")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> OrdenarPorTitulo()
        {
            var tarefas = await _appDbContext.Tarefas.OrderBy(t => t.Titulo).ToListAsync();

            return Ok(tarefas);
        }

            [HttpGet("Primeira Não Concluida")]
        public async Task<ActionResult<Tarefa>> PrimeiraNaoConcluida()
        {
            var tarefa = await _appDbContext.Tarefas.Where(t => !t.Concluida).FirstOrDefaultAsync();

            if (tarefa == null)
                return NotFound("Nenhuma tarefa não concluída encontrada.");

            return Ok(tarefa);
        }

        [HttpGet("Contar Concluidas")]
        public async Task<ActionResult<int>> ContarConcluidas()
        {
            int total = await _appDbContext.Tarefas.CountAsync(t => t.Concluida);

            return Ok(total);
        }

//Métodos de consulta padrão

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
public async Task<IActionResult> DeletarTarefa(int id)
{
    Console.WriteLine($"Tentando deletar tarefa com ID: {id}");

    var tarefa = await _appDbContext.Tarefas.FindAsync(id);
    if (tarefa == null)
    {
        Console.WriteLine("Tarefa não encontrada.");
        return NotFound("Tarefa não encontrada.");
    }

    _appDbContext.Tarefas.Remove(tarefa);
    await _appDbContext.SaveChangesAsync();

    Console.WriteLine("Tarefa deletada com sucesso!");
    return Ok("Tarefa deletada com sucesso!");
}
    }
}

