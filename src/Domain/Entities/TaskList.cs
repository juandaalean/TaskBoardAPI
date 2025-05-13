using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class TaskList
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [JsonIgnore]
        public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }
}
