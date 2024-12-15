// Services/TodoServices.cs
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoAPI.AppDataContext;
using TodoAPI.Contracts;

// Services/TodoServices.cs
using TodoAPI.Interfaces;
using TodoAPI.Models;

namespace TodoAPI.Services {
    public class TodoServices : ITodoServices {
        private readonly TodoDbContext _context;
        private readonly ILogger<TodoServices> _logger;
        private readonly IMapper _mapper;

        public TodoServices(TodoDbContext context, ILogger<TodoServices> logger, IMapper maper) {
            _context = context;
            _logger = logger;
            _mapper = maper;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            var todo = await _context.Todos.ToListAsync() ?? throw new Exception("No Todo items found.");
            return todo;
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync() ?? throw new KeyNotFoundException($"Todo item with Id {id} not found.");
            return todo;
        }

        async Task ITodoServices.CreateTodoAsync(CreateTodoRequest request)
        {
            try {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.UtcNow;
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
            } catch (Exception e) {
                _logger.LogError(e, "An error occured while creating the Todo item.");
                throw new Exception("An error occured while creating the Todo item.");
            }
        }

        async Task ITodoServices.UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            try {
                var todo = await _context.Todos.FindAsync(id) ?? throw new KeyNotFoundException($"Todo item with Id {id} not found.");

                if(request.Title != null) {
                    todo.Title = request.Title;
                }

                if(request.Description != null) {
                    todo.Description = request.Description;
                }

                if(request.IsComplete != null) {
                    todo.IsComplete = request.IsComplete.Value;
                }

                todo.DueDate = request.DueDate;
                todo.Priority = request.Priority;
                todo.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

            } catch (Exception e) {
                _logger.LogError(e, "An error occured while updating the Todo item with Id {id}.", id);
                throw new Exception($"An error occured while updating the Todo item with Id {id}.");
            }

        }

        public async Task DeleteTodoAsync(Guid id) {
            var todo = await _context.Todos.FindAsync(id) ?? throw new KeyNotFoundException($"Todo item with Id {id} not found.");
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            // if(todo != null) {
            //     _context.Todos.Remove(todo);
            //     await _context.SaveChangesAsync();
            // } else {
            //     throw new KeyNotFoundException($"Todo item with Id {id} not found.");
            // } 
        }
    }
}