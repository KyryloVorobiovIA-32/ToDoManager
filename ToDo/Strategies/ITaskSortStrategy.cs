using System.Collections.Generic;
using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.Strategies
{
    // Це наш інтерфейс "Strategy"
    public interface ITaskSortStrategy
    {
        IEnumerable<Task> Sort(IEnumerable<Task> tasks);
    }
}