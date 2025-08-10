using Microsoft.AspNetCore.Mvc;

namespace AssignMate.Web.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [HttpGet("Index")]
        [Route("")]
        public IActionResult Index()
        {
            if (User.IsInRole("Teacher"))
            {
                return View("TeacherDashboard");
            }
            return RedirectToAction("Login","Auth");  
        }
    }
}
