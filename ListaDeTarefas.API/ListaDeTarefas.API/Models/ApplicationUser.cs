namespace ListaDeTarefas.API.Models
{
    public class ApplicationUser
    {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string Username { get; set; } = string.Empty;
            public List<TaskModel>? Tasks { get; set; }
    }
}
