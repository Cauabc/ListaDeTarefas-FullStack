using ListaDeTarefas.API.Data;
using ListaDeTarefas.API.Interfaces;
using ListaDeTarefas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDataContext _dataContext;

        public TaskRepository(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Add(TaskModel item)
        {
            if (_dataContext.Tasks.Any(t => t.Name == item.Name))
            {
                throw new InvalidOperationException("A tarefa já existe!");
            }
            _dataContext.Tasks.Add(item);
        }

        public async void Delete(Guid id)
        {
            var taskToDelete = await GetById(id);
            _dataContext.Tasks.Remove(taskToDelete);
        }

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            return await _dataContext.Tasks.ToListAsync();
        }

        public async Task<TaskModel> GetById(Guid id)
        {
            return await _dataContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async void Update(Guid id, TaskModel item)
        {
            var taskToUpdate = await GetById(id);

            if (taskToUpdate != null)
            {
                taskToUpdate.Name = item.Name;
                taskToUpdate.IsCompleted = item.IsCompleted;
            }

        }
    }
}
