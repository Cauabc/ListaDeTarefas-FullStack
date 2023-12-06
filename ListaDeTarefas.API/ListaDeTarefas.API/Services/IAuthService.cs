using ListaDeTarefas.API.Models;

namespace ListaDeTarefas.API.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(User user);
        Task<bool> Login(User user);
        Task<bool> RegisterUser(User user);
    }
}