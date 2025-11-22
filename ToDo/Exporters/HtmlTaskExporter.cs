using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.Exporters
{
    public class HtmlTaskExporter : TaskExporter
    {
        protected override string GetHeader()
        {
            return "<html><head><title>Список завдань</title></head><body><h1>Мої Завдання</h1><ul>";
        }

        protected override string FormatTask(Task task)
        {
            return $"<li><b>{task.Title}</b> [{task.Priority}] - <i>{task.Status}</i></li>";
        }

        protected override string GetFooter(IEnumerable<Task> tasks)
        {
            return $"</ul><p>Всього завдань: {tasks.Count()}</p></body></html>";
        }
    }
}