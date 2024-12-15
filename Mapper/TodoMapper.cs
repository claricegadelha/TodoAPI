// Mapper/TodoMapper.cs
using AutoMapper;
using TodoAPI.Contracts;
using TodoAPI.Models;

namespace TodoAPI.Mapper {
    public class TodoMapper : Profile {
        public TodoMapper() {
            CreateMap<CreateTodoRequest, Todo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<UpdateTodoRequest, Todo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
     
    }
}
