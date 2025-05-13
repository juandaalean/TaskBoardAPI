using Application.Services.Auth;
using Application.Services.TaskListService;
using Application.Services.ToDoItemService;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskBoardContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TaskBoardDb")));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            services.AddScoped<ITaskListRepository, TaskListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IToDoItemService, ToDoItemService>();
            services.AddScoped<ITaskListService, TaskListService>();

            return services;
        }
    }
}
