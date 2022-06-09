using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<ItemData>> GetAll();
        Task<ItemData> GetById(int id);
        Task Create(ItemData item);
        Task Delete(ItemData item);
        Task Save();
    }
}
