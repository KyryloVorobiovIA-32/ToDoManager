using ToDo.Data;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;

var builder = WebApplication.CreateBuilder(args);

// Реєстрація сервісів
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Ігноруємо цикли (User -> Project -> User...) при перетворенні в JSON
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToDoDbContext>(options => options.UseSqlite("Data Source=todo.db", 
        b => b.MigrationsAssembly("ToDo"))); // <--- ВАЖНО: Указываем, где лежат миграции

var app = builder.Build();

// Блок ініціалізації бази даних на сервері
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ToDoDbContext>();
        
        context.Database.Migrate();
        
        // Створюємо базові дані (User/Project), інакше при створенні Task виникне помилка ключа
        if (!context.Users.Any())
        {
            var defaultUser = new User
            {
                Username = "server_user",
                PasswordHash = "hashedpassword"
            };
            context.Users.Add(defaultUser);
            context.SaveChanges();

            // Створюємо проєкт для цього юзера
            context.Projects.Add(new Project
            {
                Title = "Server Project",
                CreationDate = DateTime.Now,
                UserId = defaultUser.UserId
            });
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();