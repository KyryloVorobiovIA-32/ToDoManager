using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ToDo.Components;
using ToDo.Enums;
using ToDo.States;

namespace ToDo.Models
{
    public class Task : IComponent
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double EstimatedPoints { get; set; }
        public DateTime DueDate { get; set; }
        public PriorityEnum Priority { get; set; }

        // ЛОГИКА СОСТОЯНИЙ
        
        // Это хранится в БД
        public StatusEnum Status { get; set; }

        // Это не хранится в БД
        [NotMapped]
        [JsonIgnore]
        private TaskState _state;

        [NotMapped]
        [JsonIgnore]
        public TaskState State
        {
            get
            {
                // если state нет, создаем его на основе Enum из бдшки
                if (_state == null)
                {
                    _state = GetStateFromEnum(Status);
                    _state.SetContext(this);
                }
                return _state;
            }
            set
            {
                _state = value;
                _state.SetContext(this);
                // При изменении объекта состояния, меняем и Enum для бдшк
                Status = GetEnumFromState(_state);
            }
        }
        
        public void TransitionTo(TaskState newState)
        {
            State = newState;
        }

        // Вспомогательные методы конвертации
        private TaskState GetStateFromEnum(StatusEnum status)
        {
            return status switch
            {
                StatusEnum.New => new NewState(),
                StatusEnum.InProgress => new InProgressState(),
                StatusEnum.Done => new DoneState(),
                _ => new NewState()
            };
        }

        private StatusEnum GetEnumFromState(TaskState state)
        {
            if (state is NewState) return StatusEnum.New;
            if (state is InProgressState) return StatusEnum.InProgress;
            if (state is DoneState) return StatusEnum.Done;
            return StatusEnum.New;
        }

        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public double GetEstimatedPoints()
        {
            return EstimatedPoints;
        }
    }
}