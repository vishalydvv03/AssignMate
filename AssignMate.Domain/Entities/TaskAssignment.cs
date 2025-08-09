using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignMate.Core.Entities
{
    public class TaskAssignment
    {
        public int TaskId { get; set; }
        public TaskItem Task { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
