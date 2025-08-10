using AssignMate.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignMate.Core.Models
{
    public class ReadTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AssignedDate { get; set; } 
        public DateTime DueDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public List<string> AssignedTo { get; set; } = new List<string>();

    }
}
