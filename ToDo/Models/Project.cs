using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Components;

namespace ToDo.Models
{
    public class Project : IComponent // Реалізуємо інтерфейс
    {
        public int ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

        // Реалізація методу Composite: рекурсивна сума
        public double GetEstimatedPoints()
        {
            // Ми використовуємо поліморфізм: викликаємо GetEstimatedPoints у кожного Task
            return Tasks.Sum(t => t.GetEstimatedPoints());
        }
    }
}