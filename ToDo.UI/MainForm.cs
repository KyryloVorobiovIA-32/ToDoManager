using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ToDo.Models;
using ToDo.Repositories;
using ToDo.Enums;
using ToDo.Strategies;
using ToDo.Services;
using ToDo.Commands;
using ToDo.Exporters;
using Task = ToDo.Models.Task; 

namespace ToDo.UI
{
    public partial class MainForm : Form
    {
        private readonly TaskService _taskService; 
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IEnumerable<ITaskSortStrategy> _sortStrategies;
        
        // Експортери (ЛР 7)
        private readonly CsvTaskExporter _csvExporter;
        private readonly HtmlTaskExporter _htmlExporter;
        
        public MainForm(
            TaskService taskService,
            IRepository<Project> projectRepository,
            IRepository<User> userRepository,
            IEnumerable<ITaskSortStrategy> sortStrategies,
            CsvTaskExporter csvExporter,
            HtmlTaskExporter htmlExporter)
        {
            InitializeComponent();
            
            _taskService = taskService;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _sortStrategies = sortStrategies;
            
            _csvExporter = csvExporter;
            _htmlExporter = htmlExporter;

            _taskService.TasksChanged += OnTasksChangedHandler;

            InitializeSortComboBox();
            LoadTasks();
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
        }
        
        private void LoadTasks()
        {
            tasksListBox.Items.Clear(); 
            
            if (cmbSortStrategy.SelectedItem is not ITaskSortStrategy selectedStrategy)
                return;

            var tasks = _taskService.GetAllTasks();
            var sortedTasks = selectedStrategy.Sort(tasks);
            
            foreach (var task in sortedTasks)
            {
                tasksListBox.Items.Add($"[{task.Status}] {task.Title} (Пріоритет: {task.Priority})");
            }
        }
        
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            var defaultUser = _userRepository.GetAll().FirstOrDefault();
            if (defaultUser == null)
            {
                MessageBox.Show("Критична помилка: Не знайдено користувача.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var defaultProject = _projectRepository.GetAll().FirstOrDefault(p => p.UserId == defaultUser.UserId);
            if (defaultProject == null)
            {
                defaultProject = new Project 
                { 
                    Title = "Загальний Проєкт", 
                    UserId = defaultUser.UserId,
                    CreationDate = DateTime.Now
                }; 
                _projectRepository.Add(defaultProject);
            }

            using (var addTaskForm = new AddTaskForm(defaultProject.ProjectId))
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
                            ProjectId = defaultProject.ProjectId,
                            Status = StatusEnum.New,
                            DueDate = DateTime.Now.AddDays(1),
                            
                            // ЛР 8: Зберігаємо оцінку (бали) з форми
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

        // Обробник кнопки "Експорт" (ЛР 7)
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

                        TaskExporter exporter;
                        
                        if (filePath.EndsWith(".csv"))
                        {
                            exporter = _csvExporter;
                        }
                        else
                        {
                            exporter = _htmlExporter;
                        }

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

        // ЛР 8: Метод підрахунку балів (Composite Pattern)
        private void btnCalcStats_Click(object sender, EventArgs e)
        {
            // Отримуємо поточного користувача
            var defaultUser = _userRepository.GetAll().FirstOrDefault();
            if (defaultUser == null) 
            {
                MessageBox.Show("Користувач не знайдений.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var project = _projectRepository.GetAll().FirstOrDefault(p => p.UserId == defaultUser.UserId);
            
            if (project != null)
            {
                // ТУТ ПРАЦЮЄ COMPOSITE:
                // Ми викликаємо метод у Проєкту, а він сам опитує свої Завдання
                double totalPoints = project.GetEstimatedPoints();
                
                // Оновлюємо лейбл та показуємо повідомлення
                lblStats.Text = $"Загальна оцінка: {totalPoints}";
                MessageBox.Show($"Загальна складність проєкту \"{project.Title}\": {totalPoints} балів", 
                                "Статистика Проєкту", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Проєкт не знайдено.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}