using System.Collections.Generic;

namespace Application.Dtos.Tasks
{
    public class TaskListResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<ToDoItemResponseDto> Tasks { get; set; } = new();
    }
}
