using AssignMate.Core.Entities;
using AssignMate.Core.Models;
using AssignMate.Data;
using AssignMate.Services.TaskServices;
using AssignMate.Services.UserServices;
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


    }
}
