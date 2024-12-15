// Interfaces/ITodoServices.cs
using TodoAPI.Contracts;
using TodoAPI.Models;

namespace TodoAPI.Interfaces {
    public interface ITodoServices {
            Task<IEnumerable<Todo>> GetAllAsync();
            Task<Todo> GetByIdAsync(Guid Id);
            Task CreateTodoAsync(CreateTodoRequest createTodoRequest);
            Task UpdateTodoAsync(Guid Id, UpdateTodoRequest updateTodoRequest);
            Task DeleteTodoAsync(Guid Id);    
    }
}
