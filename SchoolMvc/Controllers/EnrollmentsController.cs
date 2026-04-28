using Microsoft.AspNetCore.Mvc;

namespace SchoolMvc.Controllers
{
    public class EnrollmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
