using ListaDeTarefas.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.API.Data
{
    public class ApplicationDataContext : IdentityDbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options){}

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
    }
}
