// Models/TaskEntity.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoServer.Models
{
    [Table("Tasks")]
    public class TaskEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public int? Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public string Tags { get; set; }

        public string LockedBy { get; set; }

        public DateTime? LockTimestamp { get; set; }

        public DateTime LastModified { get; set; }
    }
}
