using Application.Dtos.Tasks;
using Domain.Entities;
using Domain.Enum;
using ErrorOr;

namespace Application.Services.ToDoItemService
{
    public interface IToDoItemService
    {
        Task<ErrorOr<IEnumerable<ToDoItemDto>>> GetByStateAsync(States state, Guid userId);
        Task<ErrorOr<TaskListResponseDto>> GetAllToDoItemsAsync(Guid taskListId);
        Task<ErrorOr<ToDoItem>> CreateToDoItemAsync(ToDoItemDto toDoItemDto, Guid taskListId);
        Task<ErrorOr<Updated>> UpdateToDoItemAsync(ToDoItemDto toDoItemDto, Guid toDoItemId);
        Task<ErrorOr<Deleted>> DeleteToDoItemAsync(Guid userId);
    }
}
