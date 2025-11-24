using ToDo.Enums;

namespace ToDo.States
{
    public class NewState : TaskState
    {
        public override string ColorHex => "#0000FF"; // Синий
        public override bool CanEdit => true;

        public override void Process()
        {
            Context.TransitionTo(new InProgressState());
        }

        public override void Complete()
        {
            Context.TransitionTo(new DoneState());
        }
    }
}