using Application.Dtos.Tasks;
using Application.Services.ToDoItemService;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using ErrorOr;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskBoard_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ToDoItemController : ApiController
    {

        private readonly IToDoItemService _itemService;

        public ToDoItemController(IToDoItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("task-list/{taskListId}")]
        public async Task<IActionResult> GetAll(Guid taskListId)
        {
            var result = await _itemService.GetAllToDoItemsAsync(taskListId);

            return result.Match(
                items => Ok(items),
                errors => Problem(errors)
            );
        }

        [HttpGet("by-state")]
        public async Task<IActionResult> GetByState([FromQuery] States state)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _itemService.GetByStateAsync(state, userId);

            return result.Match(
                value => Ok(value),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromQuery] Guid taskListId, [FromBody] ToDoItemDto toDoItem)
        {
            var result = await _itemService.CreateToDoItemAsync(toDoItem, taskListId);

            return result.Match(
                item =>
                {
                    var dto = new ToDoItemDto
                    {
                        Title = toDoItem.Title,
                        Description = toDoItem.Description,
                        StartDate = toDoItem.StartDate,
                        LimitDate = toDoItem.LimitDate,
                        State = toDoItem.State
                    };
                    return CreatedAtAction(nameof(GetAll), new { taskListId = item.TaskListId }, dto);
                },
                errors => Problem(errors)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ToDoItemDto toDoItem)
        {
            var result = await _itemService.UpdateToDoItemAsync(toDoItem, id);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _itemService.DeleteToDoItemAsync(id);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}
