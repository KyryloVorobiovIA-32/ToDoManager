using System.Collections.Generic;
using System.Linq;
using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.Strategies
{
    public class SortByPriorityStrategy : ITaskSortStrategy
    {
        public IEnumerable<Task> Sort(IEnumerable<Task> tasks)
        {
            return tasks.OrderByDescending(t => t.Priority);
        }
        
        public override string ToString()
        {
            return "За пріоритетом";
        }
    }
}