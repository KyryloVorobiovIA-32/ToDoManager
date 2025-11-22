using System.Collections.Generic;
using System.Linq;
using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.Strategies
{
    public class SortByDateStrategy : ITaskSortStrategy
    {
        public IEnumerable<Task> Sort(IEnumerable<Task> tasks)
        {
            return tasks.OrderBy(t => t.DueDate);
        }
        
        public override string ToString()
        {
            return "За датою";
        }
    }
}