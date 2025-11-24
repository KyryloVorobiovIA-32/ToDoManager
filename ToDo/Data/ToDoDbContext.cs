using Microsoft.EntityFrameworkCore;
using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.Data;

public class ToDoDbContext : DbContext
{
    // *** ДОБАВЛЕН КОНСТРУКТОР ***
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Tag> Tags { get; set; }
    
    // *** МЕТОД OnConfiguring УДАЛЕН (чтобы не мешал настройкам в Program.cs) ***
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasOne(p => p.User)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.UserId);
        
        modelBuilder.Entity<Task>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId);
        
        modelBuilder.Entity<Task>()
            .HasMany(t => t.Tags)
            .WithMany(t => t.Tasks);
    }
}