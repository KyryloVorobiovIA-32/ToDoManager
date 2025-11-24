using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ToDo.Models;
using ToDo.Repositories;
using ToDo.Enums;
using ToDo.Strategies;
using ToDo.Services;
using ToDo.Commands;
using ToDo.Exporters;
using ToDo.UI.Services; // Додано для AuthService
using Task = ToDo.Models.Task; 

namespace ToDo.UI
{
    public partial class MainForm : Form
    {
        private readonly TaskService _taskService; 
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IEnumerable<ITaskSortStrategy> _sortStrategies;
        
        // Сервіс Авторизації (Курсова)
        private readonly AuthService _authService;

        // Експортери
        private readonly CsvTaskExporter _csvExporter;
        private readonly HtmlTaskExporter _htmlExporter;
        
        public MainForm(
            TaskService taskService,
            IRepository<Project> projectRepository,
            IRepository<User> userRepository,
            IEnumerable<ITaskSortStrategy> sortStrategies,
            CsvTaskExporter csvExporter,
            HtmlTaskExporter htmlExporter,
            AuthService authService) // Отримуємо AuthService через DI
        {
            InitializeComponent();
            
            _taskService = taskService;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _sortStrategies = sortStrategies;
            _csvExporter = csvExporter;
            _htmlExporter = htmlExporter;
            _authService = authService; // Зберігаємо сервіс авторизації

            _taskService.TasksChanged += OnTasksChangedHandler;

            InitializeSortComboBox();
            LoadTasks();
            
            // Відображаємо ім'я користувача у заголовку (приємна дрібниця)
            if (_authService.CurrentUser != null)
            {
                this.Text = $"Менеджер Завдань (ToDo) - Користувач: {_authService.CurrentUser.Username}";
            }
        }
        
        private void OnTasksChangedHandler()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(LoadTasks));
            }
            else
            {
                LoadTasks();
            }
        }

        private void InitializeSortComboBox()
        {
            foreach (var strategy in _sortStrategies)
            {
                cmbSortStrategy.Items.Add(strategy);
            }
            cmbSortStrategy.SelectedIndex = 0;
            cmbSortStrategy.SelectedIndexChanged += (sender, e) => LoadTasks();
            cmbSortStrategy.SelectedIndexChanged += (sender, e) => tasksListBox.Invalidate(); 
        }
        
        private void LoadTasks()
        {
            tasksListBox.Items.Clear(); 
            
            if (cmbSortStrategy.SelectedItem is not ITaskSortStrategy selectedStrategy)
                return;

            // Можна додати фільтрацію завдань тільки для поточного користувача,
            // якщо _taskService повертає всі. Але поки залишимо як є.
            var tasks = _taskService.GetAllTasks();
            var sortedTasks = selectedStrategy.Sort(tasks);
            
            foreach (var task in sortedTasks)
            {
                tasksListBox.Items.Add($"[{task.Status}] {task.Title} (Пріоритет: {task.Priority}, Балів: {task.EstimatedPoints})");
            }
        }
        
        private void tasksListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string text = tasksListBox.Items[e.Index].ToString()!;
            
            if (cmbSortStrategy.SelectedItem is not ITaskSortStrategy selectedStrategy) return;
            
            var tasks = _taskService.GetAllTasks();
            var sortedTasks = selectedStrategy.Sort(tasks).ToList();

            if (e.Index >= sortedTasks.Count) return;
            
            var task = sortedTasks[e.Index];
            
            Color itemColor = Color.Black;
            try {
                itemColor = ColorTranslator.FromHtml(task.State.ColorHex);
            } catch { }

            e.DrawBackground();

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            using (Brush brush = isSelected ? new SolidBrush(Color.White) : new SolidBrush(itemColor))
            {
                e.Graphics.DrawString(text, e.Font!, brush, e.Bounds);
            }
            
            e.DrawFocusRectangle();
        }
        
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            // *** ВИКОРИСТАННЯ AUTH SERVICE ***
            var currentUser = _authService.CurrentUser;
            
            if (currentUser == null)
            {
                MessageBox.Show("Помилка авторизації. Перезапустіть програму.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ми використовуємо ім'я змінної userProject
            var userProject = _projectRepository.GetAll().FirstOrDefault(p => p.UserId == currentUser.UserId);
            
            if (userProject == null)
            {
                userProject = new Project 
                { 
                    Title = $"Проєкт користувача {currentUser.Username}", 
                    UserId = currentUser.UserId,
                    CreationDate = DateTime.Now
                }; 
                _projectRepository.Add(userProject);
            }

            using (var addTaskForm = new AddTaskForm(userProject.ProjectId))
            {
                if (addTaskForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var newTask = new Task
                        {
                            Title = addTaskForm.TaskTitle!, 
                            Description = string.Empty,
                            Priority = addTaskForm.TaskPriority,
                            
                            // *** ВИПРАВЛЕННЯ ТУТ ***
                            // Було: defaultProject.ProjectId (помилка, бо такої змінної вже немає)
                            // Стало: userProject.ProjectId (правильно)
                            ProjectId = userProject.ProjectId, 
                            
                            Status = StatusEnum.New,
                            DueDate = DateTime.Now.AddDays(1),
                            EstimatedPoints = addTaskForm.TaskEstimatedPoints
                        };
                        
                        ICommand command = new CreateTaskCommand(_taskService, newTask);
                        command.Execute();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка збереження завдання: {ex.Message}\n\n" + 
                                        (ex.InnerException != null ? ex.InnerException.Message : ""), 
                                        "Помилка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV файл (*.csv)|*.csv|HTML сторінка (*.html)|*.html";
                saveFileDialog.Title = "Експорт завдань";
                saveFileDialog.FileName = "tasks_export";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var tasks = _taskService.GetAllTasks();
                        string filePath = saveFileDialog.FileName;

                        TaskExporter exporter = filePath.EndsWith(".csv") 
                            ? (TaskExporter)_csvExporter 
                            : (TaskExporter)_htmlExporter;

                        exporter.Export(filePath, tasks);

                        MessageBox.Show("Дані успішно експортовано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка експорту: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCalcStats_Click(object sender, EventArgs e)
        {
            // *** ВИКОРИСТАННЯ AUTH SERVICE ***
            var currentUser = _authService.CurrentUser;
            if (currentUser == null) return;
            
            var project = _projectRepository.GetAll().FirstOrDefault(p => p.UserId == currentUser.UserId);
            
            if (project != null)
            {
                double totalPoints = project.GetEstimatedPoints();
                lblStats.Text = $"Загальна оцінка: {totalPoints}";
                MessageBox.Show($"Загальна складність проєкту \"{project.Title}\": {totalPoints} балів", 
                                "Статистика Проєкту", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Проєкт не знайдено.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Task? GetSelectedTask()
        {
            if (tasksListBox.SelectedIndex == -1) return null;
            if (cmbSortStrategy.SelectedItem is not ITaskSortStrategy selectedStrategy) return null;

            var tasks = _taskService.GetAllTasks();
            var sortedTasks = selectedStrategy.Sort(tasks).ToList();
            
            if (tasksListBox.SelectedIndex < sortedTasks.Count)
            {
                return sortedTasks[tasksListBox.SelectedIndex];
            }
            return null;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var task = GetSelectedTask();
            if (task == null) 
            {
                MessageBox.Show("Виберіть завдання зі списку!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            task.State.Process();
            _taskService.UpdateTask(task);
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            var task = GetSelectedTask();
            if (task == null) 
            {
                MessageBox.Show("Виберіть завдання зі списку!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            task.State.Complete();
            _taskService.UpdateTask(task);
        }
    }
}