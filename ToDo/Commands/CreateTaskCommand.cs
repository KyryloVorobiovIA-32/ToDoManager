using ToDo.Services;
using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.Commands
{
    public class CreateTaskCommand : ICommand
    {
        private readonly TaskService _receiver; // Одержувач (хто виконує)
        private readonly Task _task; // Параметри (що створити)
        
        public CreateTaskCommand(TaskService receiver, Task task)
        {
            _receiver = receiver;
            _task = task;
        }

        // Реалізація інтерфейсу Command
        public void Execute()
        {
            _receiver.CreateTask(_task);
        }
    }
}