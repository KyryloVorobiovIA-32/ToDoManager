using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDo.Data;
using ToDo.Models;
using ToDo.Repositories;
using ToDo.Strategies;
using ToDo.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDo.Exporters;
using Task = ToDo.Models.Task;

namespace ToDo.UI
{
    static class Program
    {
        public static IServiceProvider? Services { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            Services = host.Services;

            // Виконуємо міграцію та сидінг даних
            using (var scope = Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
                try
                {
                    dbContext.Database.Migrate();
                    EnsureDataExists(dbContext);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Критична помилка ініціалізації бази даних: {ex.Message}",
                                    "Помилка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Application.Run(Services.GetRequiredService<MainForm>());
        }

        // Налаштування "Хоста" та всіх наших сервісів
        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Реєструємо DbContext
                    services.AddDbContext<ToDoDbContext>();

                    // Реєструємо Репозиторії
                    // Тепер Tasks йдуть через Інтернет (API)
                    services.AddTransient<IRepository<Task>, ToDo.UI.ApiRepositories.ApiTaskRepository>();
                    services.AddTransient<IRepository<Project>, GenericRepository<Project>>();
                    services.AddTransient<IRepository<User>, GenericRepository<User>>();
                    
                    // Реєструємо Стратегії
                    services.AddTransient<ITaskSortStrategy, SortByPriorityStrategy>();
                    services.AddTransient<ITaskSortStrategy, SortByDateStrategy>();

                    // Реєстрація Service
                    services.AddSingleton<TaskService>(); 
                    
                    // Реєстрація Експортерів
                    services.AddTransient<CsvTaskExporter>();
                    services.AddTransient<HtmlTaskExporter>();
                    
                    // Реєструємо Форми
                    services.AddTransient<MainForm>();
                });
        
        private static void EnsureDataExists(ToDoDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Username = "default_user",
                    PasswordHash = "hashedpassword"
                });
                context.SaveChanges(); 
            }

            var defaultUser = context.Users.First();
            if (!context.Projects.Any())
            {
                context.Projects.Add(new Project
                {
                    Title = "Загальний Проєкт",
                    CreationDate = DateTime.Now,
                    UserId = defaultUser.UserId
                });
                context.SaveChanges();
            }
        }
    }
}