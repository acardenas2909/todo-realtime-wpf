using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using TodoServer.Models;
using TodoServer.Repositories;
using TodoServer.Hubs;

namespace TodoServer.Controllers
{
    /// <summary>
    /// Controller that manages task operations for the To-Do application.
    /// Supports real-time synchronization via SignalR.
    /// </summary>
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private readonly TaskRepository _repo = new TaskRepository();
        private readonly IHubContext _hub = GlobalHost.ConnectionManager.GetHubContext<TaskHub>();

        /// <summary>
        /// Retrieves all tasks in the system.
        /// </summary>
        /// <returns>A list of tasks.</returns>
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> GetAll() =>
            Ok(await _repo.GetAllAsync());

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="task">The task entity to create.</param>
        /// <returns>The created task object.</returns>
        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Create(TaskEntity task)
        {
            task.LastModified = System.DateTime.UtcNow;
            await _repo.AddAsync(task);
            _hub.Clients.All.broadcastChange("create", task);
            return Ok(task);
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="task">The updated task entity.</param>
        /// <returns>The updated task object, or 404 if not found.</returns>
        [HttpPut, Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, TaskEntity task)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            task.Id = id;
            task.LastModified = System.DateTime.UtcNow;
            await _repo.UpdateAsync(task);
            _hub.Clients.All.broadcastChange("update", task);
            return Ok(task);
        }

        /// <summary>
        /// Deletes a task by ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>200 OK if deleted.</returns>
        [HttpDelete, Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            _hub.Clients.All.broadcastChange("delete", id);
            return Ok();
        }
    }
}
