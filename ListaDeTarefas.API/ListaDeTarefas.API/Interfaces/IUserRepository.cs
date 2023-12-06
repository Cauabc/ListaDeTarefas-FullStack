using ListaDeTarefas.API.Models;

namespace ListaDeTarefas.API.Interfaces
{
    public interface IUserRepository
    {
        void Add(ApplicationUser user);
        Task<bool> SaveAllAsync();
        Task<ApplicationUser> GetUserByUsername(string username);
    }
}
