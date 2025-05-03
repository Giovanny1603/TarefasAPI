using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CriarTarefa(Tarefa tarefa)
        {
            if (tarefa == null)
            {
                return BadRequest("Tarefa não pode ser nula.");
            }

            _appDbContext.Tarefas.Add(tarefa);
            await _appDbContext.SaveChangesAsync();

            return Ok(tarefa);
        }

    }
}