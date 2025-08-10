using AssignMate.Core.Entities;
using AssignMate.Core.Models;
using AssignMate.Data;
using AssignMate.Services.TaskServices;
using AssignMate.Services.UserServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssignMate.Web.Controllers
{
    [Route("Tasks")]
    public class TaskController : Controller
    {
        private readonly IUserService userService;
        private readonly ITaskService taskService;
        public TaskController(IUserService userService, ITaskService taskService)
        {
            this.userService = userService;
            this.taskService = taskService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Create()
        {
            var users = await userService.GetAllStudentsList();

            ViewBag.Users = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            return View();
            
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(TaskCreate dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await taskService.AddTask(dto);
            if(result == false)
            {
                ModelState.AddModelError(string.Empty, "Task Cannot Be Assigned. Please Try Again !");
                return View(dto);
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await taskService.GetAllTasks();
            return View(tasks);

        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await taskService.GetTaskById(id);
            if (task == null)
            {
                return RedirectToAction("GetAllTasks", "Task");
            }
            return View(task);

        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> EditTask(int id)
        {
            var task = await taskService.GetTaskByIdForEdit(id);
            var users = await userService.GetAllStudentsList();
            ViewBag.Users = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();
            return View(task);

        }

        [HttpPost("Edit/{id:int}")]
        public async Task<IActionResult> UpdateTask(TaskEdit dto)
        {
            var result = await taskService.UpdateTask(dto);
            if(result == false)
            {
                return View("EditTask");
            }

            return RedirectToAction("GetTaskById", "Task", new { id = dto.Id });
        }

        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> DeleteTask(int Id)
        {
            var result = await taskService.DeleteTask(Id);
            if (result == false)
            {
                return RedirectToAction("GetTaskById", "Task", new { id = Id });
            }

            return RedirectToAction("GetAllTasks", "Task");
        }

    }
}
