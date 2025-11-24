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
using ToDo.UI.Services;
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

            // *** ВАЖНОЕ ИЗМЕНЕНИЕ ***
            // Мы УДАЛИЛИ блок "using (var scope... dbContext.Database.Migrate())"
            // Клиент (UI) больше НЕ пытается создавать базу данных. 
            // Это делает только Сервер (API).

            // 1. Отримуємо форму логіна (через DI)
            var loginForm = Services.GetRequiredService<LoginForm>();
            
            // 2. Показуємо форму логіна
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // 3. Якщо користувач успішно увійшов, запускаємо головну форму
                Application.Run(Services.GetRequiredService<MainForm>());
            }
            else
            {
                Application.Exit();
            }
        }

        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Налаштовуємо підключення до тієї ж БД для локальних операцій (User/Project)
                    services.AddDbContext<ToDoDbContext>(options => 
                        options.UseSqlite("Data Source=todo.db"));

                    // Реєструємо Репозиторії (ВСЕ через API!)
                    services.AddTransient<IRepository<Task>, ToDo.UI.ApiRepositories.ApiTaskRepository>();
                    services.AddTransient<IRepository<Project>, ToDo.UI.ApiRepositories.ApiProjectRepository>(); // Новый
                    services.AddTransient<IRepository<User>, ToDo.UI.ApiRepositories.ApiUserRepository>();       // Новый
                    
                    // Реєструємо Стратегії
                    services.AddTransient<ITaskSortStrategy, SortByPriorityStrategy>();
                    services.AddTransient<ITaskSortStrategy, SortByDateStrategy>();

                    // Реєстрація Service
                    services.AddSingleton<TaskService>(); 
                    services.AddSingleton<AuthService>(); 
                    
                    // Реєстрація Експортерів
                    services.AddTransient<CsvTaskExporter>();
                    services.AddTransient<HtmlTaskExporter>();
                    
                    // Реєструємо Форми
                    services.AddTransient<MainForm>();
                    services.AddTransient<LoginForm>();
                });
    }
}