using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITaskListRepository
    {
        Task<IEnumerable<TaskList>> GetByUserIdAsync(Guid userId);
        Task<TaskList?> GetByIdAsync(Guid id);
        Task AddTaskListAsync(TaskList taskList);
        void UpdateTaskList(TaskList taskList);
        void DeleteTaskList(TaskList taskList);
    }
}
