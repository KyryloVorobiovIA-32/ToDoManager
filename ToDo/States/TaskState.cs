using System.Drawing;

using ToDo.Models;
using Task = ToDo.Models.Task;

namespace ToDo.States
{
    public abstract class TaskState
    {
        protected Task Context;

        public void SetContext(Task task)
        {
            Context = task;
        }

        // Свойства, которые зависят от состояния
        public abstract string ColorHex { get; }
        public abstract bool CanEdit { get; }

        // Методы переходов (Transitions)
        public abstract void Process();
        public abstract void Complete();
    }
}