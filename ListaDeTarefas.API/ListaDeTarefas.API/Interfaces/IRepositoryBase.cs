using ListaDeTarefas.API.Models;

namespace ListaDeTarefas.API.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        void Add(T item);
        void Update(Guid id, TaskModel item);
        void Delete(Guid id);
        Task<T> GetById(Guid id);
        Task<bool> SaveAllAsync();
    }
}
