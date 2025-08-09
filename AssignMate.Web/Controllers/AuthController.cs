using AssignMate.Core.Models;
using AssignMate.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace AssignMate.Web.Controllers
{
    [Route("")]
    public class AuthController : Controller
    {
        private readonly IUserService userService;
        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("")]
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var result = await userService.LoginUser(dto);
            if(result == false)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(dto);
            }
            return View();
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegister dto)
        {
            if (!ModelState.IsValid) 
            {
                return View(dto);
            }
            var result = await userService.RegisterUser(dto);
            if(result == false)
            {
                ModelState.AddModelError(string.Empty, "User already registered.");
                return View(dto);
            }
            TempData["SuccessMessage"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }
    }
}
