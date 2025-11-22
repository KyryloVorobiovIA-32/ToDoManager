using System.Collections.Generic;
using System.IO;
using System.Text;
using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.Exporters
{
    public abstract class TaskExporter
    {
        // Template Method
        // послідовність кроків, яку не можна змінити
        public void Export(string filePath, IEnumerable<Task> tasks)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine(GetHeader());

                foreach (var task in tasks)
                {
                    writer.WriteLine(FormatTask(task));
                }

                writer.WriteLine(GetFooter(tasks));
            }
        }
        
        // Форматування одного завдання в рядок
        protected abstract string FormatTask(Task task);
        
        protected virtual string GetHeader()
        {
            return "";
        }

        protected virtual string GetFooter(IEnumerable<Task> tasks)
        {
            return "";
        }
    }
}