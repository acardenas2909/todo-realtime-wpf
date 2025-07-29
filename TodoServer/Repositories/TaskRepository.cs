// Repositories/TaskRepository.cs
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TodoServer.Data;
using TodoServer.Models;

namespace TodoServer.Repositories
{
    public class TaskRepository
    {
        private readonly TodoContext _context;

        public TaskRepository()
        {
            _context = new TodoContext();
        }

        public async Task<List<TaskEntity>> GetAllAsync() =>
            await _context.Tasks.OrderBy(t => t.Priority).ToListAsync();

        public async Task<TaskEntity> GetByIdAsync(int id) =>
            await _context.Tasks.FindAsync(id);

        public async Task AddAsync(TaskEntity task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
