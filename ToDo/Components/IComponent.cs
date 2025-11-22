namespace ToDo.Components
{
    public interface IComponent
    {
        string Title { get; }
        
        // Головний метод патерна: отримати оцінку (для завдання - власна, для проєкту - сума)
        double GetEstimatedPoints();
    }
}