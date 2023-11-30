using ListaDeTarefas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.API.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options){}

        public DbSet<TaskModel> Tasks { get; set; }
    }
}
