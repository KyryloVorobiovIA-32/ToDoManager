using System.Collections.Generic;

namespace ToDo.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}