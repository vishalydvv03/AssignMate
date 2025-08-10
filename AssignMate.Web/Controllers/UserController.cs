using AssignMate.Core.Models;
using AssignMate.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace AssignMate.Web.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await userService.GetUserById(id);
            if(user!=null)
            {
                return View(user);
            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await userService.GetUserById(id);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Update/{id:int}")]
        public async Task<IActionResult> UpdateUser(int Id, ReadUser user)
        {
            var result = await userService.UpdateUser(Id, user);
            if (result==false)
            {
                return View("EditUser", user);
            }
            return RedirectToAction("GetUser", "User", new {id=Id});
        }

        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await userService.DeleteUser(id);
            if (result == false)
            {
                return View("Index","Home");
            }
            return RedirectToAction("Logout","Auth");
        }
    }
}
