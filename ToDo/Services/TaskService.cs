using System;
using System.Collections.Generic;
using ToDo.Models;
using ToDo.Repositories;
using Task = ToDo.Models.Task;

namespace ToDo.Services
{
    public class TaskService
    {
        private readonly IRepository<Task> _taskRepository;
        public event Action? TasksChanged;

        public TaskService(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public IEnumerable<Task> GetAllTasks()
        {
            return _taskRepository.GetAll();
        }
        public void CreateTask(Task task)
        {
            _taskRepository.Add(task);
            // Сповіщаємо всіх підписників, що дані змінилися
            OnTasksChanged();
        }

        public void UpdateTask(Task task)
        {
            _taskRepository.Update(task);
            OnTasksChanged(); // Сповіщаємо
        }

        public void DeleteTask(int taskId)
        {
            _taskRepository.Delete(taskId);
            OnTasksChanged(); // Сповіщаємо
        }
        protected virtual void OnTasksChanged()
        {
            // Викликаємо подію, якщо на неї є хоча б один підписник
            TasksChanged?.Invoke();
        }
    }
}