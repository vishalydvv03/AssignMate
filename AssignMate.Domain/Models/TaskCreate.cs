using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignMate.Core.Models
{
    public class TaskCreate
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public List<int> AssignedUserIds { get; set; } = new List<int>();
    }
}
