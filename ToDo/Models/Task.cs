using System;
using System.Collections.Generic;
using ToDo.Enums;
using ToDo.Components; // Додано

namespace ToDo.Models
{
    public class Task : IComponent // Реалізуємо інтерфейс
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double EstimatedPoints { get; set; } // Додано (Оцінка складності)
        public DateTime DueDate { get; set; }
        public PriorityEnum Priority { get; set; }
        public StatusEnum Status { get; set; }

        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // Реалізація методу Composite: просто повертаємо своє значення
        public double GetEstimatedPoints()
        {
            return EstimatedPoints;
        }
    }
}