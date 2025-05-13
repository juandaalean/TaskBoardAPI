using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Domain.Enum;

namespace Infrastructure.Data
{
    public class TaskBoardContext : DbContext
    {
        public TaskBoardContext(DbContextOptions<TaskBoardContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public static readonly Guid AdminId = Guid.Parse("d3a637e3-f1a5-49f6-b19e-6a2e8b39b7c1");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var passwordHasher = new PasswordHasher<User>();

            var adminUser = new User
            {
                Id = AdminId,
                UserName = "admin",
                Email = "admin@taskboard.com",
                Role = Roles.Admin,
                PasswordHash = "AQAAAAIAAYagAAAAEErJkZjok7c5zFaZUX44OtDJ0y0kzNDE2MGvc2AwFd83U+F0LzrtGPMVaUG/Jw+vcw=="
            };
            modelBuilder.Entity<User>().HasData(adminUser);
        }
    }
}
