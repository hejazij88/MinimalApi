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


        [HttpGet]
        public ActionResult<IEnumerable<TodoDto>> GetAll()
        {
            var items = _svc.GetAll().Select(x => new TodoDto(x));
            return Ok(items);
        }


        [HttpGet("{id}", Name = "GetTodoById")]
        public ActionResult<TodoDto> GetById(int id)
        {
            var t = _svc.GetById(id);
            if (t is null) return NotFound();
            return Ok(new TodoDto(t));
        }


        [HttpPost]
        public ActionResult<TodoDto> Create([FromBody] TodoCreateDto dto)
        {
            var t = _svc.Create(dto.Title, dto.IsCompleted);
            return CreatedAtRoute("GetTodoById", new { id = t.Id }, new TodoDto(t));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_svc.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
