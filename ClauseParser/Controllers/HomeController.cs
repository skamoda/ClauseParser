using Microsoft.AspNetCore.Mvc;

namespace ClauseParser.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Parse(string text)
        {
            ViewBag.text = text;

            return View("Index");
        }
    }
}
