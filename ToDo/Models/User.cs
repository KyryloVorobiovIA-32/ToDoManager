using System.Collections.Generic;

namespace ToDo.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}