using Microsoft.AspNetCore.Mvc;
using TaskMateAPI.Models;

namespace TaskMateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> tasks = new();

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetAllTasks() => tasks;

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            return task is null ? NotFound() : task;
        }

        [HttpPost]
        public ActionResult<TaskItem> CreateTask(TaskItem task)
        {
            task.Id = tasks.Count + 1;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task is null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsComplete = updatedTask.IsComplete;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task is null) return NotFound();

            tasks.Remove(task);
            return NoContent();
        }
    }
}
