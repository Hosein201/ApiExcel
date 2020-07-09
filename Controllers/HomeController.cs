using Microsoft.AspNetCore.Mvc;

namespace ApiExcel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
