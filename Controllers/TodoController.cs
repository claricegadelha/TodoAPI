// Controlers.TodoControlers.cs
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Contracts;
using TodoAPI.Interfaces;

namespace TodoAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase {
        private readonly ITodoServices _todoServices;
        public TodoController(ITodoServices todoServices) {
            _todoServices = todoServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            try {
                var todo = await _todoServices.GetAllAsync();
                if(todo == null || !todo.Any()) {
                    return Ok(new { message = "No Todo Items found." });
                } 
                return Ok(new { message = "Successfully retrieved all Todo Items.", data = todo });
            } catch (Exception e) {
                return StatusCode(500, new { message = "An error occurred while retrieving the Todo Items.", error = e.Message });
            }
        }

        [HttpGet("id:guid")]
        public async Task<IActionResult> GetByIdAsync(Guid id) {
            try {
                var todo = await _todoServices.GetByIdAsync(id);
                if(todo == null) {
                    return NotFound(new { message = "No Todo Item with Id {Id} found.", id });
                } 
                return Ok(new { message = "Successfully retrieved the Todo Item with Id {Id}.", data = todo });
            } catch (Exception e) {
                return StatusCode(500, new { message = $"An error occurrec while retrieving the Todo Item with Id {id}.", error = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            try {
                await _todoServices.CreateTodoAsync(request);
                return Ok(new { message = "Todo Item successfully created." });
            } catch (Exception e) {
                return StatusCode(500, new { message = "An error occurred while creating the Todo Item.", error = e.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest request) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            try {
                await _todoServices.UpdateTodoAsync(id, request);
                return Ok(new { message = "Todo Item successfully updated." });
            } catch (Exception e) {
                return StatusCode(500, new { message = "An error occurred while updating the Todo Item.", error = e.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTodoAsync(Guid id) {
            try {
                await _todoServices.DeleteTodoAsync(id);
                return Ok(new { message = $"Todo Item Id {id} successfully deleted." });
            } catch (Exception e) {
                return StatusCode(500, new { message = "An error occurred while deleting the Todo Item Id {id}.", error = e.Message });
            }
        }
    }
}