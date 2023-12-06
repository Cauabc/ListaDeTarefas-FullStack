using ListaDeTarefas.API.Data;
using ListaDeTarefas.API.Interfaces;
using ListaDeTarefas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDataContext _dataContext;

        public UserRepository(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(ApplicationUser user)
        {
            _dataContext.ApplicationUsers.Add(user);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<ApplicationUser> GetUserByUsername(string username)
        {
            return await _dataContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
