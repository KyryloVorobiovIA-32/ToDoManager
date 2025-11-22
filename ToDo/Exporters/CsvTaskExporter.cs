using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.Exporters
{
    public class CsvTaskExporter : TaskExporter
    {
        protected override string GetHeader()
        {
            return "ID,Title,Priority,Status,DueDate";
        }

        protected override string FormatTask(Task task)
        {
            // Форматуємо рядок через кому
            return $"{task.TaskId},{Escape(task.Title)},{task.Priority},{task.Status},{task.DueDate:yyyy-MM-dd}";
        }

        // Допоміжний метод для екранування ком у тексті
        private string Escape(string input)
        {
            if (input.Contains(","))
                return $"\"{input}\"";
            return input;
        }
    }
}