using Application.Dtos.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using ErrorOr;

namespace Application.Services.TaskListService
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _taskListRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public TaskListService(ITaskListRepository taskListRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _taskListRepo = taskListRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<TaskList>> CreateTaskListAsync(CreateTaskListDto createTaskListDto, Guid userId)
        {
            var taskList = new TaskList
            {
                Id = Guid.NewGuid(),
                Name = createTaskListDto.Name,
                UserId = userId
            };
            await _taskListRepo.AddTaskListAsync(taskList);
            await _unitOfWork.SaveChangerAsync();

            return taskList;
        }

        public async Task<ErrorOr<IEnumerable<TaskListResponseDto>>> GetAllTasksList(Guid userId)
        {
            var lists = await _taskListRepo.GetByUserIdAsync(userId);
            var mapped = lists.Select(list => new TaskListResponseDto
            {
                Id = list.Id,
                Name = list.Name,
                Tasks = list.ToDoItems.Select(item => new ToDoItemResponseDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    State = item.State,
                    StartDate = item.StartDate,
                    LimitDate = item.LimitDate
                }).ToList()
            });
            return ErrorOrFactory.From(mapped);
        }
    }
}
