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
        Task<IEnumerable<ReadTask>> GetAllTasks();
        Task<ReadTask?> GetTaskById(int id);
        Task<TaskEdit?> GetTaskByIdForEdit(int id);
        Task<bool> UpdateTask(TaskEdit dto);
        Task<bool> DeleteTask(int id);
    }
}
