using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IToDoItemRepository
    {
        Task<IEnumerable<ToDoItem>> GetByUserIdAndStateAsync(Guid userId, States state);
        Task<IEnumerable<ToDoItem>> GetAllAsync(Guid taskListId);
        Task<ToDoItem?> GetByIdAsync(Guid id);
        Task AddAsync(ToDoItem item);
        void Update(ToDoItem item);
        void Delete(ToDoItem item);
    }
}
