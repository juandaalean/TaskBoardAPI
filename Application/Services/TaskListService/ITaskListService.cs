using Application.Dtos.Tasks;
using Domain.Entities;
using ErrorOr;

namespace Application.Services.TaskListService
{
    public interface ITaskListService
    {
        Task<ErrorOr<IEnumerable<CreateTaskListDto>>> GetAllTasksList(Guid userId);
        Task<ErrorOr<TaskList>> CreateTaskListAsync(CreateTaskListDto createTaskListDto, Guid userId);
    }
}
