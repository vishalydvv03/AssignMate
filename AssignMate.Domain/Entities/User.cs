using AssignMate.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignMate.Core.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }
        
        public DateOnly DateOfBirth { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<TaskAssignment> TaskAssignments { get; set; }

    }
}
