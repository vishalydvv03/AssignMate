using AssignMate.Core.Entities;
using AssignMate.Core.Models;
using AssignMate.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AssignMate.Services.TaskServices
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor httpContext;
        public TaskService(AppDbContext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext;
        }
        public async Task<bool> AddTask(TaskCreate dto)
        {
            var user = httpContext.HttpContext?.User;
            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var task = new TaskItem
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    DueDate = dto.DueDate,
                    CreatedByUserId = Convert.ToInt32(userId),
                };

                if (dto.AssignedUserIds != null && dto.AssignedUserIds.Any())
                {
                    task.TaskAssignments = dto.AssignedUserIds.Select(userId => new TaskAssignment
                    {
                        UserId = Convert.ToInt32(userId)
                    }).ToList();
                }

                await context.Tasks.AddAsync(task);

                await context.SaveChangesAsync();
                return true;
            }
               
            return false;
        }

        public async Task<IEnumerable<ReadTask>> GetAllTasks()
        {
            var tasks = await context.Tasks
            .Include(t => t.TaskAssignments)
            .ThenInclude(ta => ta.User)
            .Select(t => new ReadTask
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                AssignedDate = t.AssignedDate,     
                DueDate = t.DueDate,
                AssignedTo = t.TaskAssignments
                               .Select(ta => ta.User.Name)
                               .ToList()
            }).ToListAsync();

            return tasks;
        }

        public async Task<ReadTask?> GetTaskById(int id)
        {
            var task = await context.Tasks
            .Include(t => t.TaskAssignments)
            .ThenInclude(ta => ta.User)
            .Where(t=>t.Id ==id)
            .Select(t => new ReadTask
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                AssignedDate = t.AssignedDate,
                DueDate = t.DueDate,
                AssignedTo = t.TaskAssignments
                               .Select(ta => ta.User.Name)
                               .ToList()
            }).FirstOrDefaultAsync();

            return task;
        }
        public async Task<TaskEdit?> GetTaskByIdForEdit(int id)
        {
            var task = await context.Tasks
                .Include(t => t.TaskAssignments)
                .Where(t => t.Id == id)
                .Select(t => new TaskEdit
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    AssignedUserIds = t.TaskAssignments.Select(a => a.UserId).ToList()
                })
                .FirstOrDefaultAsync();

            return task;
        }
        public async Task<bool> UpdateTask(TaskEdit dto)
        {
            var task = await context.Tasks
            .Include(t => t.TaskAssignments) 
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (task != null)
            {
                task.Title = dto.Title;
                task.Description = dto.Description;
                task.DueDate = dto.DueDate;

                context.TaskAssignments.RemoveRange(task.TaskAssignments);

                if (dto.AssignedUserIds != null && dto.AssignedUserIds.Any())
                {
                    foreach (var userId in dto.AssignedUserIds)
                    {
                        task.TaskAssignments.Add(new TaskAssignment
                        {
                            TaskId = task.Id,
                            UserId = userId
                        });
                    }
                }

                await context.SaveChangesAsync();
                return true;
            }
                return false;
            
        }
        public async Task<bool> DeleteTask(int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task != null)
            {
                context.Tasks.Remove(task);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
