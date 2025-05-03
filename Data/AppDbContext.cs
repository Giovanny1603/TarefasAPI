using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using ProjetoTarefas.Models;

namespace ProjetoTarefas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}
      
        public DbSet<Tarefa> Tarefas { get; set; } = null!;
    }
}