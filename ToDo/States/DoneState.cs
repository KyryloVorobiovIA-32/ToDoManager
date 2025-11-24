using ToDo.Enums;

namespace ToDo.States
{
    public class DoneState : TaskState
    {
        public override string ColorHex => "#008000"; // Зеленый
        public override bool CanEdit => false; // Запрет на редактирование!

        public override void Process()
        {
            Context.TransitionTo(new InProgressState());
        }

        public override void Complete()
        {
            // Уже готово
        }
    }
}