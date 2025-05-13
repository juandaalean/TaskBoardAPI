using Application.Dtos.Tasks;
using AutoMapper;
using Domain.Entities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TaskList, CreateTaskListDto>().ReverseMap();
        CreateMap<ToDoItem, ToDoItemDto>().ReverseMap();
    }
}


