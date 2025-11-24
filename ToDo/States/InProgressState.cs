using ToDo.Enums;

namespace ToDo.States
{
    public class InProgressState : TaskState
    {
        public override string ColorHex => "#FFA500"; // Оранжевый
        public override bool CanEdit => true;

        public override void Process()
        {
            // Уже в работе
        }

        public override void Complete()
        {
            Context.TransitionTo(new DoneState());
        }
    }
}