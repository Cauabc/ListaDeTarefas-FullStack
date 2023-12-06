using ListaDeTarefas.API.Models;

namespace ListaDeTarefas.API.Interfaces
{
    public interface ITaskRepository : IRepositoryBase<TaskModel>
    {
        Task<List<TaskModel>> GetUserTasks(Guid id);
    }
}
