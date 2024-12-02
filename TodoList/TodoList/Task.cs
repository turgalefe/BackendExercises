using System.ComponentModel.DataAnnotations;

namespace TodoList
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
