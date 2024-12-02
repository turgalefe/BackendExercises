using Microsoft.AspNetCore.Mvc;

using TodoList;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllTasks() => Ok(_context.Tasks.ToList());

        [HttpPost]
        public IActionResult AddTask([FromBody] TodoList.Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAllTasks), task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TodoList.Task updatedTask)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            task.IsCompleted = updatedTask.IsCompleted;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
