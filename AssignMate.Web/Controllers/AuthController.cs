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
            var token = await userService.LoginUser(dto);
            if(string.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(dto);
            }
            Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
            return RedirectToAction("Index", "Home");
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

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken");
            return RedirectToAction("Login", "Auth");
        }

    }
}
