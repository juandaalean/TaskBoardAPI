using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly TaskBoardContext _context;

        public ToDoItemRepository(TaskBoardContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ToDoItem item)
        {
            await _context.ToDoItems.AddAsync(item);
        }

        public void Delete(ToDoItem item)
        {
            _context.ToDoItems.Remove(item);
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync(Guid taskListId)
        {
            return await _context.ToDoItems
                .Where(i => i.TaskListId == taskListId)
                .ToListAsync();
        }

        public async Task<ToDoItem?> GetByIdAsync(Guid id)
        {
            return await _context.ToDoItems.FindAsync(id);
        }

        public async Task<IEnumerable<ToDoItem>> GetByUserIdAndStateAsync(Guid userId, States state)
        {
            return await _context.ToDoItems
                .Include(i => i.TaskList)
                .Where(i => i.TaskList.UserId == userId && i.State == state)
                .ToListAsync();
        }

        public void Update(ToDoItem item)
        {
            _context.ToDoItems.Update(item);
        }
    }
}
