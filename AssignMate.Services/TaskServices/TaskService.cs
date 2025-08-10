using AssignMate.Core.Entities;
using AssignMate.Core.Models;
using AssignMate.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignMate.Services.TaskServices
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext context;
        public TaskService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddTask(TaskCreate dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                CreatedByUserId = 4
            };

            if (dto.AssignedUserIds != null && dto.AssignedUserIds.Any())
            {
                task.TaskAssignments = dto.AssignedUserIds.Select(userId => new TaskAssignment
                {
                    UserId = userId
                }).ToList();
            }

            await context.Tasks.AddAsync(task);

            var saved = await context.SaveChangesAsync();

            return saved > 0;
        }
    }
}
