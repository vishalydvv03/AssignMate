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
            return View();
        }
    }
}
