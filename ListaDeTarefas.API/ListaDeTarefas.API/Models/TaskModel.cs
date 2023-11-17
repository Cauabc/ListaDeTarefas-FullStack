using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ListaDeTarefas.API.Models
{
    public class TaskModel
    {
        [Key]  
        public Guid Id { get; private set; } = Guid.NewGuid();
        [Required]
        [NotNull]
        [DisplayName("Nome")]
        public string Name { get; set; }
        [Required]
        [NotNull]
        [DisplayName("Foi completa?")]
        public bool IsCompleted { get; set; }

    }
}
