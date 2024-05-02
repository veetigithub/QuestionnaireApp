using Microsoft.AspNetCore.Mvc;

namespace QuestionnaireApp.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
