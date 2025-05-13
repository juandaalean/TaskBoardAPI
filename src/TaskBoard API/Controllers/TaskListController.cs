using Application.Dtos.Tasks;
using Application.Services.TaskListService;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskBoard_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskListController : ApiController
    {
        private readonly ITaskListService _taskService;

        public TaskListController(ITaskListService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskList()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _taskService.GetAllTasksList(userId);

            return result.Match(
                value => Ok(value),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskList([FromBody] CreateTaskListDto response)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _taskService.CreateTaskListAsync(response, userId);
            return result.Match(
                taskList => {
                    var dto = new CreateTaskListDto
                    {
                        Name = taskList.Name
                    };
                    return CreatedAtAction(nameof(GetAllTaskList), new { id = taskList.Id }, dto);
                },
                errors => Problem(errors)
            );
        }
    }
}
