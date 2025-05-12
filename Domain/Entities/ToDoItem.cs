using Domain.Enum;

namespace Domain.Entities
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public States State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LimitDate { get; set; }
        public Guid TaskListId { get; set; }
        public TaskList TaskList { get; set; } = null!;
    }
}
