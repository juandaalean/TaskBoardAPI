using Application.Dtos.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using ErrorOr;
using System.Collections.Generic;

namespace Application.Services.ToDoItemService
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemRepository _itemRepo;
        private readonly ITaskListRepository _taskListRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ToDoItemService(IToDoItemRepository itemRepo, IUnitOfWork unitOfWork, IMapper mapper, ITaskListRepository taskListRepo)
        {
            _itemRepo = itemRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _taskListRepo = taskListRepo;
        }

        public async Task<ErrorOr<ToDoItem>> CreateToDoItemAsync(ToDoItemDto toDoItemDto, Guid taskListId)
        {
            var taskList = await _taskListRepo.GetByIdAsync(taskListId);
            if (taskList is null)
            {
                return Error.NotFound("TaskList.NotFound", "The tasklist was not found.");
            }

            var toDoItem = _mapper.Map<ToDoItem>(toDoItemDto);
            toDoItem.TaskListId = taskListId;

            await _itemRepo.AddAsync(toDoItem);
            await _unitOfWork.SaveChangerAsync();

            return toDoItem;
        }

        public async Task<ErrorOr<Deleted>> DeleteToDoItemAsync(Guid userId)
        {
            var item = await _itemRepo.GetByIdAsync(userId);
            if (item is null)
            {
                return Error.NotFound("ToDoItem.Notfound", "Task not found");
            }

            _itemRepo.Delete(item);
            await _unitOfWork.SaveChangerAsync();

            return Result.Deleted;
        }

        public async Task<ErrorOr<TaskListResponseDto>> GetAllToDoItemsAsync(Guid taskListId)
        {
            var taskList = await _taskListRepo.GetByIdAsync(taskListId);
            if (taskList is null)
            {
                return Error.NotFound("TaskList.NotFound", "The tasklist was not found.");
            }

            var dto = new TaskListResponseDto
            {
                Id = taskList.Id,
                Name = taskList.Name,
                Tasks = taskList.ToDoItems.Select(item => new ToDoItemResponseDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    State = item.State,
                    StartDate = item.StartDate,
                    LimitDate = item.LimitDate
                }).ToList()
            };

            return dto;
        }

        public async Task<ErrorOr<IEnumerable<ToDoItemDto>>> GetByStateAsync(States state, Guid taskListId, Guid userId)
        {
            var taskList = await _taskListRepo.GetByIdAsync(taskListId);

            if (taskList is null || taskList.UserId != userId)
            {
                return Error.Unauthorized("TaskList.Unauthorized", "Unauthorized tasklist");
            }

            var items = await _itemRepo.GetByTaskListAndStateAsync(taskListId, state);
            var mapped = _mapper.Map<IEnumerable<ToDoItemDto>>(items);
            return ErrorOrFactory.From(mapped);
        }

        public async Task<ErrorOr<Updated>> UpdateToDoItemAsync(ToDoItemDto toDoItemDto, Guid toDoItemId)
        {
            var item = await _itemRepo.GetByIdAsync(toDoItemId);
            if (item is null)
            {
                return Error.NotFound("ToDoItem.NotFound", "Task not found");
            }

            item.Title = toDoItemDto.Title;
            item.State = toDoItemDto.State;
            item.Description = toDoItemDto.Description;
            item.LimitDate = toDoItemDto.LimitDate;

            _itemRepo.Update(item);
            await _unitOfWork.SaveChangerAsync();

            return Result.Updated;
        }
    }
}
