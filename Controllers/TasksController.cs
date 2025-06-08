using Microsoft.AspNetCore.Mvc;
using TaskMateAPI.Models;

namespace TaskMateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Sample Task 1", IsComplete = false },
            new TaskItem { Id = 2, Title = "Sample Task 2", IsComplete = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> Get() => Ok(tasks);

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetById(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskItem> Create(TaskItem task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskItem updated)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            task.Title = updated.Title;
            task.IsComplete = updated.IsComplete;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            tasks.Remove(task);
            return NoContent();
        }
    }
}
