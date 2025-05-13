using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly TaskBoardContext _context;

        public TaskListRepository(TaskBoardContext context)
        {
            _context = context;
        }

        public async Task AddTaskListAsync(TaskList taskList)
        {
            await _context.TaskLists.AddAsync(taskList);
        }

        public void DeleteTaskList(TaskList taskList)
        {
            _context.TaskLists.Remove(taskList);
        }

        public async Task<TaskList?> GetByIdAsync(Guid id)
        {
            return await _context.TaskLists
                .Include(l => l.ToDoItems)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<TaskList>> GetByUserIdAsync(Guid userId)
        {
            return await _context.TaskLists
                .Include(l => l.ToDoItems)
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public void UpdateTaskList(TaskList taskList)
        {
            _context.TaskLists.Update(taskList);
        }
    }
}
