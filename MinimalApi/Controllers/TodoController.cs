using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Dtos;
using MinimalApi.Services;

namespace MinimalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _svc;
        public TodoController(ITodoService svc) => _svc = svc;

        /// <summary>
        /// Get All To Do
        /// </summary>
        /// <returns> List To Do</returns>
        [HttpGet]
        public ActionResult<IEnumerable<TodoDto>> GetAll()
        {
            var items = _svc.GetAll().Select(x => new TodoDto(x));
            return Ok(items);
        }

        /// <summary>
        /// Get To Do By Id
        /// </summary>
        /// <param name="id">To Do Id</param>
        /// <returns>To Do</returns>
        [HttpGet("{id}", Name = "GetTodoById")]
        public ActionResult<TodoDto> GetById(int id)
        {
            var t = _svc.GetById(id);
            if (t is null) return NotFound();
            return Ok(new TodoDto(t));
        }

        /// <summary>
        /// Create New Todo
        /// </summary>
        /// <param name="dto">To Do title</param>
        /// <returns> to do  </returns>
        [HttpPost]
        public ActionResult<TodoDto> Create([FromBody] TodoCreateDto dto)
        {
            var t = _svc.Create(dto.Title, dto.IsCompleted);
            return CreatedAtRoute("GetTodoById", new { id = t.Id }, new TodoDto(t));
        }

        /// <summary>
        /// Delete To Do
        /// </summary>
        /// <param name="id">To Do Id</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_svc.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
