using Microsoft.AspNetCore.Mvc;
using QuestionnaireApp.Models;

namespace QuestionnaireApp.Controllers
{
    public class MainController : Controller
    {
        // Instance of the SurveyManipulator class
        private readonly SurveyManipulator _surveyManipulator;

        // Constructor to inject an instance of SurveyManipulator into the controller
        public MainController(SurveyManipulator surveyManipulator)
        {
            _surveyManipulator = surveyManipulator;
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var allSurveys = _surveyManipulator.GetAllSurveys<Survey>("Survey");
                Console.WriteLine(allSurveys);
                return View(allSurveys);
            }
            return View();
        }
    }
}
