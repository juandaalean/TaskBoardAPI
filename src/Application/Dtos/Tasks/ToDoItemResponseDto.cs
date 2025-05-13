using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Tasks
{
    public class ToDoItemResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public States State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LimitDate { get; set; }
    }
}
