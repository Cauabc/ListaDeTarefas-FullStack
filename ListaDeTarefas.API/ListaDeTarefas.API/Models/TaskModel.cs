using OfficeOpenXml.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ListaDeTarefas.API.Models
{
    [EpplusTable]
    public class TaskModel
    {
        [Key]  
        public Guid Id { get; private set; } = Guid.NewGuid();
        [Required]
        [NotNull]
        [DisplayName("Nome da Tarefa")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [NotNull]
        [DisplayName("Foi completa?")]
        public bool IsCompleted { get; set; }
        [EpplusIgnore]
        public Guid UserId { get; set; }
        [EpplusIgnore]
        [JsonIgnore]
        public ApplicationUser? User { get; set; }
    }
}
