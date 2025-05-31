using System.ComponentModel.DataAnnotations;

namespace ProjetoTarefas.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatório.")]
        [StringLength(150, ErrorMessage = "A descrição deve ter no máximo 150 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A flag de conclusão é obrigatória.")]
        public bool Concluida { get; set; }
    }
}