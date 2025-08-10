using AssignMate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignMate.Services.TaskServices
{
    public interface ITaskService
    {
        Task<bool> AddTask(TaskCreate dto);
    }
}
