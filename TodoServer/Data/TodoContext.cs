// Data/TodoContext.cs
using System.Data.Entity;
using TodoServer.Models;

namespace TodoServer.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("TodoDbConnection") { }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
